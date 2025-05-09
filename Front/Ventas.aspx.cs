﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;
using System.Linq;

namespace Front
{
    public partial class Ventas : System.Web.UI.Page
    {

        protected Label lblMensaje;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cliente"] == null || !IDPerfilValido())
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarVentas();

            }
        }


        private void CargarVentas()
        {
            VentaNegocio negocio = new VentaNegocio();
            gvVentasPendientes.DataSource = negocio.ListarVentasPendientes();
            gvVentasPendientes.DataBind();

            gvVentas.DataSource = negocio.ListarVentas();
            gvVentas.DataBind();



        }

        private bool IDPerfilValido()
        {
            Cliente cliente = (Cliente)Session["cliente"];
            return cliente.idPerfil == 2 || cliente.idPerfil == 3 || cliente.idPerfil == 4;
        }

        protected void btnAprobar_Click(object sender, EventArgs e)
        {


            int idVenta = Convert.ToInt32(((Button)sender).CommandArgument);
            VentaNegocio negocio = new VentaNegocio();
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            List<Producto> carrito = negocio.ObtenerCarritoPorVenta(idVenta);


            if (carrito == null || carrito.Count == 0)
            {
                MostrarMensaje("El carrito esta vacío.", true);
                return;
            }


            negocio.AprobarVenta(idVenta);

            dominio.Factura factura = new dominio.Factura();

            factura.IdVenta = idVenta;
            factura.TotalFactura = carrito.Sum(x => x.precioVenta * x.Cantidad);
            factura.SubTotalFactura = carrito.Sum(x => x.precioVenta * x.Cantidad);


            FacturaNegocio facturaNegocio = new FacturaNegocio();
            facturaNegocio.GenerarFactura(factura);



            // es el envio del mail

            Venta venta = negocio.ObtenerVentaPorId(idVenta);

            Cliente clientequecompro = clienteNegocio.ObtenerCliente(venta.Cliente.Id);

            EmailService emailService = new EmailService();
            emailService.EnviarCorreoConfirmacion(clientequecompro.Email, "Estado De tu Compra", "Tu Compra ya a sido Aprobada ");



            CargarVentas();
        }



        protected void btnRechazar_Click(object sender, EventArgs e)
        {

            // volver a insertar todos los productos en la base de datos porque se rechazó
            VentaNegocio negocio = new VentaNegocio();
            ClienteNegocio clienteNegocio = new ClienteNegocio();

            int idVenta = Convert.ToInt32(((Button)sender).CommandArgument);
            negocio.RechazarVenta(idVenta);
            List<Producto> carrito = negocio.ObtenerCarritoPorVenta(idVenta);


            Venta venta = negocio.ObtenerVentaPorId(idVenta);

            Cliente clientequecompro = clienteNegocio.ObtenerCliente(venta.Cliente.Id);







            // aca se tiene que volver a insertar el stock que se descontó cuando el cliente confimo la compra

            foreach (Producto item in carrito)
            {
                ProductoNegocio productoNegocio = new ProductoNegocio();
                productoNegocio.VolverAgregarStock(item.id, item.Cantidad);
            }



            // es el envio del mail
            EmailService emailService = new EmailService();
            emailService.EnviarCorreoConfirmacion(clientequecompro.Email, "Estado De tu Compra", "Tu Compra a sido Rechazada ");

            CargarVentas();
        }



        private void MostrarMensaje(string mensaje, bool esError = true)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "alert alert-danger" : "alert alert-success";
            lblMensaje.Visible = true;





        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {


            string query = txtBuscar.Text.Trim();
       

            if (string.IsNullOrEmpty(query))
            {
                Response.Redirect(Request.RawUrl);
           

            }
            else
            {
                VentaNegocio negocio = new VentaNegocio();
                gvVentas.DataSource = negocio.BuscarVentas(query);
                gvVentas.DataBind();
            }
               
        }


    }
}

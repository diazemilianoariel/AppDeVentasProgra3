using System;
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
            List<Producto> carrito = negocio.ObtenerCarritoPorVenta(idVenta);


            if (carrito ==  null || carrito.Count == 0)
            {
                MostrarMensaje("El carrito esta vacío.", true);
                return;
            }


            negocio.AprobarVenta(idVenta);




    


            dominio.Factura factura = new dominio.Factura()
            {
                IdVenta = idVenta,
                TotalFactura = carrito.Sum(x => x.precio * x.Cantidad),
                SubTotalFactura = carrito.Sum(x => x.precio * x.Cantidad),
                


            };
           


            FacturaNegocio facturaNegocio = new FacturaNegocio();
            facturaNegocio.GenerarFactura(factura);

            

            Response.Redirect("Factura.aspx");

            CargarVentas();
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            List<Producto> listaproducto = (List<Producto>)Session["Carrito"];

            // volver a insertar todos los productos en la base de datos porque se rechazó

            int idVenta = Convert.ToInt32(((Button)sender).CommandArgument);
            VentaNegocio negocio = new VentaNegocio();
            negocio.RechazarVenta(idVenta);

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
            VentaNegocio negocio = new VentaNegocio();
         

            gvVentas.DataSource = negocio.BuscarVentas(query);
            gvVentas.DataBind();
        }


    }
}

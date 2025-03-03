using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front
{
	public partial class Factura : System.Web.UI.Page
	{
     

        protected void Page_Load(object sender, EventArgs e)
		{

            if (!IsPostBack)
            {
                CargarFacturas();
            }



        }

        private void CargarFacturas()
        {
            Cliente cliente;
            cliente = (Cliente)Session["cliente"];

            if(cliente == null)
            {

                Response.Redirect("Login.aspx");


            }
            else
            {
                litCliente.Text = cliente.Nombre + " " + cliente.Apellido;
                litDireccion.Text = cliente.Direccion;
                litTelefono.Text = cliente.Telefono;
                litEmail.Text = cliente.Email;

            }



            decimal TotalGeneral = Convert.ToDecimal(Session["TotalFactura"]) ;


            int IdVenta = Convert.ToInt32(Session["IdVenta"]);
            Venta venta = new Venta();
            VentaNegocio ventanegocio = new VentaNegocio();
            List<Producto> productos = ventanegocio.ListarProductosPorVenta(IdVenta);
            venta = ventanegocio.ObtenerVentaPorId(IdVenta);

      
         
            litTotalFactura.Text = TotalGeneral.ToString();


            rptProductos.DataSource = productos;
            rptProductos.DataBind();


        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("MisCompras.aspx");
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            // aca  en realidad deberia mandarse por mail si es que necesito mandar la factura nuevamente al cliente
            // pero como no tengo un servidor de correo configurado, lo que hago es abrir una nueva ventana con la factura

        }

















    }
}
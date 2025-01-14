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
    public partial class CompraParcial : System.Web.UI.Page
    {
        public List<Producto> ListaArticulos = new List<Producto>();


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CargarCarrito();
            }

        }

        private void CargarCarrito()
        {
            List<Producto> carrito = ObtenerCarrito();
            rptCarrito.DataSource = carrito;
            rptCarrito.DataBind();
            ActualizarTotalGeneral();
        }

        private List<Producto> ObtenerCarrito()
        {
            return (List<Producto>)Session["Carrito"] ?? new List<Producto>();
        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            int idProducto = Convert.ToInt32(((Button)sender).CommandArgument);
            List<Producto> carrito = ObtenerCarrito();
            Producto productoEnCarrito = carrito.Find(p => p.id == idProducto);
            if (productoEnCarrito != null)
            {
                carrito.Remove(productoEnCarrito);
                Session["Carrito"] = carrito;
                CargarCarrito();
            }
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCantidad = (TextBox)sender;
            RepeaterItem item = (RepeaterItem)txtCantidad.NamingContainer;
            int idProducto = Convert.ToInt32(((Button)item.FindControl("btnQuitar")).CommandArgument);
            List<Producto> carrito = ObtenerCarrito();
            Producto productoEnCarrito = carrito.Find(p => p.id == idProducto);
            if (productoEnCarrito != null)
            {
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                productoEnCarrito.Cantidad = cantidad; 
                Session["Carrito"] = carrito;
                CargarCarrito();
            }
        }

        private void ActualizarTotalGeneral()
        {
            List<Producto> carrito = ObtenerCarrito();
            decimal totalGeneral = 0;
            foreach (Producto producto in carrito)
            {
                totalGeneral += producto.SubTotal;
            }
            lblTotalGeneral.Text = totalGeneral.ToString("F2");
        }

        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            // Implementar la lógica para confirmar la compra
        }


        protected void btnVolverHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }




    }
}
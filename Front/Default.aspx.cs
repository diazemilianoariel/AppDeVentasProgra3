using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Front
{
    public partial class Default : System.Web.UI.Page
    {
        private DefaultNegocio defaultNegocio = new DefaultNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
               
            }
        }

        private void CargarProductos()
        {
            try
            {

                string busqueda = txtBuscar.Text.Trim();
                List<Producto> listaProductos;

                if (string.IsNullOrEmpty(busqueda))
                {
                    listaProductos = defaultNegocio.ListarProductos();
                }
                else
                {
                    listaProductos = defaultNegocio.BuscarProductos(busqueda);
                }

                rptProductos.DataSource = listaProductos;
                rptProductos.DataBind();

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar productos: " + ex.Message, true);
            }
        }

     
        private List<Producto> ObtenerCarrito()
        {
            return (List<Producto>)Session["Carrito"] ?? new List<Producto>();
        }



        private void MostrarMensaje(string mensaje, bool esError = true)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "alert alert-danger" : "alert alert-success";
            lblMensaje.Visible = true;
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarProductos();
        }


        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            CargarProductos();
            txtBuscar.Text = "";
        }


        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            int idProducto = Convert.ToInt32(((Button)sender).CommandArgument);
            Response.Redirect($"Productos/DetalleProducto.aspx?id={idProducto}");
        }

    }
}

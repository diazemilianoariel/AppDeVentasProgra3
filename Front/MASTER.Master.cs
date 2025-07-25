using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Front
{
    public partial class MASTER : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              //  CargarCategoriasDropdown();
                CategoriaNegocio negocio = new CategoriaNegocio();
                rptCategorias.DataSource = negocio.ListarCategorias();
                rptCategorias.DataBind();
            }

            // Esta lógica ahora se ejecuta en cada carga de página (PostBack o no)
            // para que el contador esté siempre sincronizado.

            // Obtenemos la lista de productos de la sesión.
            var carrito = Session["Carrito"] as List<Producto>;
            int totalProductos = 0;

            if (carrito != null)
            {
                // Sumamos la propiedad "Cantidad" de cada producto en la lista.
                // Asegúrate de que tu clase Producto tenga una propiedad 'Cantidad' para el carrito.
                // Si no la tiene y solo guardas un producto por línea, puedes usar 'carrito.Count'.
                totalProductos = carrito.Sum(p => p.Cantidad);
            }

            ActualizarCarrito.InnerText = totalProductos.ToString();
            // Actualizamos el contador en el HTML.
            ActualizarCarrito.InnerText = totalProductos.ToString();

         

        }



        private void CargarCategoriasDropdown()
        {
            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                rptCategorias.DataSource = negocio.ListarCategorias();
                rptCategorias.DataBind();
            }
            catch (Exception ex)
            {
                // Manejar el error si no se pueden cargar las categorías.
            }
        }

        public void ActualizarContadorCarrito(int totalProductos = 0)
        {
            ActualizarCarrito.InnerText = totalProductos.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "ActualizarContadorCarrito", $"actualizarContadorCarrito({totalProductos});", true);
        }


        

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("usuario");
            Response.Redirect("Login.aspx");
        }
    }
}
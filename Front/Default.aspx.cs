using dominio;
using negocio;
using System;
using System.Collections.Generic;

namespace Front
{
    public partial class Default : System.Web.UI.Page
    {
        // Usamos la clase de negocio principal y definitiva para productos.
        private readonly ProductoNegocio productoNegocio = new ProductoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            
           
                CargarProductos();
            
        }

        private void CargarProductos()
        {
            try
            {
                string busqueda = txtBuscar.Text.Trim();
                string idCategoria = Request.QueryString["cat"];

                // Llamamos al método "Listar" unificado que ya sabemos que funciona.
                List<Producto> listaProductos = productoNegocio.Listar(busqueda, idCategoria);

                rptProductos.DataSource = listaProductos;
                rptProductos.DataBind();
            }
            catch (Exception ex)
            {
                Session["error"] = ex;
                Response.Redirect("Error.aspx");
            }
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Cuando el usuario busca, simplemente recargamos los productos con el nuevo filtro.
            CargarProductos();
        }
    }
}
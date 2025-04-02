using dominio;
using negocio;

using System;
using System.Web.UI.WebControls;

namespace Front
{
    public partial class producto : System.Web.UI.Page
    {
        ProductoNegocio productonegocio = new ProductoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["cliente"] == null || !IDPerfilValido())
            {
                Response.Redirect("Login.aspx");
                return;
            }




            if (!IsPostBack)
            {

                CargarGrilla();
            }


        }



        private bool IDPerfilValido()
        {
            Cliente cliente = (Cliente)Session["cliente"];

            return cliente.idPerfil == 2 || cliente.idPerfil == 4 || cliente.idPerfil == 3;
        }






        private void CargarGrilla()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            GridViewProductos.DataSource = negocio.ListarProductos();
            GridViewProductos.DataBind();
        }



        protected void GridViewProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modificar")
            {
                int productId = Convert.ToInt32(e.CommandArgument);

                //redireccionr a otra pagina
                Response.Redirect("Productos/ProductoModificar.aspx?id=" + productId);

            }
            else
            {
                if (e.CommandName == "Detalle")
                {
                    int productId = Convert.ToInt32(e.CommandArgument);

                    //redireccionr a otra pagina
                    Response.Redirect("Productos/ProductoDetalle.aspx?id=" + productId);
                }

            }

            if (e.CommandName == "Eliminar")
            {
                int productId = Convert.ToInt32(e.CommandArgument);

                //redireccionr a otra pagina
                Response.Redirect("Productos/ProductoEliminar.aspx?id=" + productId);

            }




        }
    }
}
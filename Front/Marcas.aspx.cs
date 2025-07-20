using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;


namespace Front
{
    public partial class Marcas : System.Web.UI.Page
    {
        //protected TextBox TextBoxMarca;
        protected TextBox TextBoxIdMarca;
        protected TextBox TextBoxNuevaMarca;
        protected Panel editSection;
      //  protected GridView GridViewMarcas;

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null || !EsAdmin(usuario))
            {
                Response.Redirect("Login.aspx");
                return;
            }


            if (!IsPostBack)
            {
                CargarMarcas();
            }
        }


        private bool EsAdmin(Usuario usuario)
        {
            //  solo los Administradores pueden gestionar Marcas.
            return usuario != null && usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }

        private void CargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            GridViewMarca.DataSource = negocio.ListarMarcas();
            GridViewMarca.DataBind();
        }

        protected void GridViewMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modificar")
            {
                int marcaId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("MarcasABM/MarcaModificar.aspx?id=" + marcaId);
            }
            else if (e.CommandName == "Eliminar")
            {
                int marcaId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("MarcasABM/MarcaEliminar.aspx?id="+  marcaId);

            }
        }


    }
}

using dominio;

using negocio;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;




namespace Front
{
    public partial class Perfiles : System.Web.UI.Page
    {
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
                CargarPerfiles();
            }

        }

        private void CargarPerfiles()
        {
            PerfilesNegocio perfilesNegocio = new PerfilesNegocio();
            string filtro = txtFiltro.Text; // Lee el valor del nuevo TextBox de búsqueda
            GridViewPerfiles.DataSource = perfilesNegocio.ListarPerfiles(filtro);
            GridViewPerfiles.DataBind();
        }

        private bool EsAdmin(Usuario usuario)
        {
            // Según el plan, solo los Administradores pueden gestionar Perfiles.
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }



        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            GridViewPerfiles.PageIndex = 0;
            CargarPerfiles();
        }


        protected void GridViewPerfiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPerfiles.PageIndex = e.NewPageIndex;
            CargarPerfiles();
        }

        protected void GridViewPerfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int perfilId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Modificar")
            {
                Response.Redirect("PerfilesABM/PerfilModificar.aspx?id=" + perfilId);
            }
            else if (e.CommandName == "Eliminar")
            {
                Response.Redirect("PerfilesABM/PerfilEliminar.aspx?id=" + perfilId);
            }
        }


    }
}
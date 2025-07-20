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
            GridViewPerfiles.DataSource = perfilesNegocio.ListarPerfiles();
            GridViewPerfiles.DataBind();
        }

        private bool EsAdmin(Usuario usuario)
        {
            // Según el plan, solo los Administradores pueden gestionar Perfiles.
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }

        protected void MostrarMensaje(string mensaje, bool esError = true)
        {
            

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
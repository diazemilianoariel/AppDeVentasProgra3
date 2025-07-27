using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace Front
{
    public partial class Tipos : System.Web.UI.Page
    {
        protected TextBox TextBoxIdTipo;
        protected TextBox TextBoxNuevoTipo;
        protected Panel editSection;

        protected void Page_Load(object sender, EventArgs e)
        {

            //obtiene  usuario de nueva variable de sesión.
            Usuario usuario = Session["usuario"] as Usuario;

            //  valida el permiso ANTES de hacer cualquier otra cosa.
            if (usuario == null || !EsAdmin(usuario))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarTipos();
            }
        }

        private bool EsAdmin(Usuario usuario)
        {
            // solo los Administradores pueden gestionar Tipos.
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }


        private void CargarTipos()
        {
            TipoNegocio negocio = new TipoNegocio();
            string filtro = txtFiltro.Text;
            GridViewTipos.DataSource = negocio.ListarTipos(filtro);
            GridViewTipos.DataBind();
        }



        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            GridViewTipos.PageIndex = 0;
            CargarTipos();
        }

        protected void GridViewTipos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewTipos.PageIndex = e.NewPageIndex;
            CargarTipos();
        }


        protected void GridViewTipos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int tipoId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Modificar")
            {
                Response.Redirect("TiposABM/TipoModificar.aspx?id=" + tipoId);
            }
            else if (e.CommandName == "Eliminar")
            {
                Response.Redirect("TiposABM/TipoEliminar.aspx?id=" + tipoId);
            }
        }

       
    }
}

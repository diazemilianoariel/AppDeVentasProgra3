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
            string filtro = txtFiltro.Text;
            GridViewMarca.DataSource = negocio.ListarMarcas(filtro);
            GridViewMarca.DataBind();
        }


        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            GridViewMarca.PageIndex = 0;
            CargarMarcas();
        }

        protected void GridViewMarcas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewMarca.PageIndex = e.NewPageIndex;
            CargarMarcas();
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

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front
{
    public partial class Categorias : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null || !EsPerfilValido(usuario))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarCategorias();
            }
        }

        private bool EsPerfilValido(Usuario usuario)
        {
            if (usuario.Perfil != null)
            {
                return usuario.Perfil.Id == (int)TipoPerfil.Administrador ||
                       usuario.Perfil.Id == (int)TipoPerfil.soporte;
                       
            }
            return false;
        }


        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            string filtro = txtFiltro.Text;
            GridViewCategorias.DataSource = negocio.ListarCategorias(filtro);
           GridViewCategorias.DataBind();
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            GridViewCategorias.PageIndex = 0;
            CargarCategorias();
        }

        protected void GridViewCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewCategorias.PageIndex = e.NewPageIndex;
            CargarCategorias();
        }





        protected void GridViewCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modificar")
            {
                int categoriaId = Convert.ToInt32(e.CommandArgument);

                Response.Redirect("CategoriasABM/CategoriaModificar.aspx?id=" + categoriaId);

            }
            else if (e.CommandName == "Eliminar")
            {
                int categoriaId = Convert.ToInt32(e.CommandArgument);

                Response.Redirect("CategoriasABM/CategoriaEliminar.aspx?id=" + categoriaId);


            }
        }


   


    }
}

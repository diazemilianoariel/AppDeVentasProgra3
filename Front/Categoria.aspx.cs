using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio.Logica;

namespace Front
{
    public partial class Categoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
            }
        }

        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            GridViewCategorias.DataSource = negocio.ListarCategorias();
            GridViewCategorias.DataBind();
        }

        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria categoria = new Categoria();
            categoria. = txtNombreCategoria.Text;
            negocio.AgregarCategoria(categoria);
            CargarCategorias();

        }

        protected void GridViewCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }
    }
}

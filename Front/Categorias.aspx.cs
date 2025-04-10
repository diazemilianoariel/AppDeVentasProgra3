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
            if (Session["cliente"] == null || !EsAdministradorOSoporte((Cliente)Session["cliente"]))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarCategorias();
            }
        }

        private bool EsAdministradorOSoporte(Cliente cliente)
        {
            return cliente.nombrePerfil == "Administrador" || cliente.nombrePerfil == "Soporte" || cliente.nombrePerfil == "Vendedor";
        }


        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            GridViewCategorias.DataSource = negocio.ListarCategorias();
           GridViewCategorias.DataBind();
        }

        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
        //    CategoriaNegocio negocio = new CategoriaNegocio();
        //    Categoria categoria = new Categoria();

        //    if (TextBoxCategoria != null && !string.IsNullOrEmpty(TextBoxCategoria.Text))
        //    {
        //        categoria.nombre = TextBoxCategoria.Text;
        //        if (!negocio.buscarCategoriaNombre(categoria.nombre)){


        //            categoria.nombre = TextBoxCategoria.Text;
        //            negocio.AgregarCategoria(categoria);

        //        }
        //        else
        //        {
        //            negocio.altaLogica(categoria.nombre);
        //        }
        //            CargarCategorias();
        //        TextBoxCategoria.Text = string.Empty; // Limpiar el TextBox después de agregar
        //    }
        //    else
        //    {
               
        //            // Mostrar ventana emergente
        //            string script = "alert('por favor proporcione un nuevo nombre de categoria.');";
        //            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
        //            return;
                
        //    }
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

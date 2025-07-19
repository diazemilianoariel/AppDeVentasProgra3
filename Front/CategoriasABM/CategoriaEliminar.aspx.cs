using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front.CategoriasABM
{
    public partial class CategoriaEliminar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (Session["usuario"] == null || !IDPerfilValido())
            {
                Response.Redirect("../Login.aspx");
                return;
            }



            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int categoriaId = Convert.ToInt32(Request.QueryString["id"]);
                    CargarDatosCategoria(categoriaId);
                }
                else
                {
                    // Manejar el caso donde no se proporciona un ID.
                    LabelError.Text = "No se especificó ninguna categoría para eliminar.";
                    LabelError.Visible = true;
                    btnConfirmar.Visible = false;

                }
            }




        



        }
        private bool IDPerfilValido()
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario != null && usuario.Perfil != null)
            {
                return usuario.Perfil.Id == (int)TipoPerfil.Administrador ||
                       usuario.Perfil.Id == (int)TipoPerfil.soporte ;
            }
            return false;

        }

        private void CargarDatosCategoria(int categoriaId)
        {
            // Implementa la lógica para obtener los datos de la categoría de la base de datos
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            var categoria = categoriaNegocio.BuscarCategoria(categoriaId);
            if (categoria != null)
            {
                LabelNombreCategoria.Text = categoria.nombre;
                LabelEstadoCategoria.Text = categoria.estado ? "Activo" : "Inactivo";


            }
            else
            {
                // Manejar el caso en que no se encuentra la categoría
                // Puedes redirigir a otra página o mostrar un mensaje de error

                LabelError.Text = "Categoría no encontrada.";
                LabelError.Visible = true;
                btnConfirmar.Visible = false;
            }
        }


        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                int categoriaId = Convert.ToInt32(Request.QueryString["id"]);
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                categoriaNegocio.bajaLogica(categoriaId);
                Response.Redirect("../Categorias.aspx");
            }
            catch (Exception ex)
            {
                LabelError.Text = "Ocurrió un error al eliminar la categoría.";
                LabelError.Visible = true;
              
               
            }
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Categorias.aspx");
        }





    }
}
using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static negocio.CategoriaNegocio;

namespace Front.CategoriasABM
{
    public partial class CategoriaAgregar : System.Web.UI.Page
    {


        CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            // Verificar si el usuario tiene permisos para acceder a esta página
            if (usuario == null || !EsPerfilValido(usuario))
            {
                Response.Redirect("Login.aspx");
                return;
            }



            if (!IsPostBack)
            {


              
            }

        }


        private bool EsPerfilValido(Usuario usuario)
        {
            if (usuario.Perfil != null)
            {
                return usuario.Perfil.Id == (int)TipoPerfil.Administrador ||
                       usuario.Perfil.Id == (int)TipoPerfil.soporte ;
            }
            return false;
        }



        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TextBoxNombre.Text))
                {
                    LabelError.Text = "El campo 'Nombre' es obligatorio.";
                    LabelError.Visible = true;
                    return;
                }

                var categoria = new Categoria
                {
                    nombre = TextBoxNombre.Text,
                    estado = CheckBoxEstado.Checked
                };

                categoriaNegocio.AgregarCategoria(categoria);
                Response.Redirect("../Categorias.aspx");
            }
            catch (CategoriaInactivaException ex)
            {
                // CASO ESPECIAL: La categoría existe pero está inactiva.
                // La reactivamos y redirigimos.
                categoriaNegocio.ReactivarCategoria(ex.CategoriaExistente.Id);
                Response.Redirect("../Categorias.aspx");
            }
            catch (Exception ex)
            {
                // CASO GENERAL: Otro error (ej: ya existe y está activa).
                LabelError.Text = "Error: " + ex.Message;
                LabelError.Visible = true;
            }
        }



        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            // Redirige a la página de lista de productos
            Response.Redirect("../Categorias.aspx");
        }





    }
}
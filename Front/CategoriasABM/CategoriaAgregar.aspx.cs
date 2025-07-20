using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

            LabelErrorCategoriaExistente.Text = "";
            LabelError.Text = "";

            string NombreNuevo = TextBoxNombre.Text;
            List<Categoria> listaDeCategorias = new List<Categoria>();
            listaDeCategorias = categoriaNegocio.ListarCategorias();


            // Verificar si el nombre ya existe
            foreach (var categoria in listaDeCategorias)
            {
                if (categoria.nombre.Equals(NombreNuevo, StringComparison.OrdinalIgnoreCase))
                {
                    LabelErrorCategoriaExistente.Text = "El nombre de la categoría ya existe.";
                    LabelErrorCategoriaExistente.Visible = true;
                    return;
                }
            }



            try
            {
                // Validar campos requeridos
                if (string.IsNullOrWhiteSpace(TextBoxNombre.Text))
                {
                    // Mostrar un mensaje de error al usuario
                    LabelError.Text = "El campo 'Nombre' es obligatorio.";
                    LabelError.Visible = true;
                    return;
                }

                

                var categoria = new Categoria
                {
                    nombre = TextBoxNombre.Text,
                    estado = CheckBoxEstado.Checked
                };

                // Implementa la lógica para agregar la categoría en la base de datos
                categoriaNegocio.AgregarCategoria(categoria);

                // Redirige a la página de lista de categorías
                Response.Redirect("../Categorias.aspx");
            }
            catch (Exception ex)
            {
                // Manejar cualquier error inesperado
                LabelError.Text = "Ocurrió un error al guardar la categoría. Intente nuevamente.";
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
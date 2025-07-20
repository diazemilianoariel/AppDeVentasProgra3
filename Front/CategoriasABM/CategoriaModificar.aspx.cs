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
    public partial class CategoriaModificar : System.Web.UI.Page
    {


        CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {

            Usuario usuario = Session["cliente"] as Usuario;
            // Verificar si el usuario tiene permisos para acceder a esta página
            if (Session["usuario"] == null || !IDPerfilValido())
            {
                Response.Redirect("Login.aspx");
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
                    LabelError.Text = "No se especificó ninguna categoría para modificar.";
                    LabelError.Visible = true;
                    ButtonGuardar.Visible = false;
                }
            }


        
        }


        private bool IDPerfilValido()
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario != null && usuario.Perfil != null)
            {
                return usuario.Perfil.Id == (int)TipoPerfil.Administrador ||
                       usuario.Perfil.Id == (int)TipoPerfil.soporte;
            }
            return false;

        }




        private void CargarDatosCategoria(int categoriaId)
        {
            
            Categoria categoria = categoriaNegocio.BuscarCategoria(categoriaId);
            if (categoria != null)
            {
                LabelId.Text = categoria.Id.ToString();
                TextBoxNombre.Text = categoria.nombre;
                CheckBoxEstado.Checked = categoria.estado;

              
                HiddenFieldNombreOriginal.Value = categoria.nombre;

            }
            else
            {
                LabelError.Text = "No se encontró la categoría especificada.";
                LabelError.Visible = true;
                ButtonGuardar.Visible = false;
            }

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

                // Si el nombre cambió, verificamos que no exista ya.
                if (!TextBoxNombre.Text.Equals(HiddenFieldNombreOriginal.Value, StringComparison.OrdinalIgnoreCase))
                {
                    List<Categoria> listaDeCategorias = categoriaNegocio.ListarCategorias();
                    if (listaDeCategorias.Any(cat => cat.nombre.Equals(TextBoxNombre.Text, StringComparison.OrdinalIgnoreCase)))
                    {
                        LabelErrorCategoriaExistente.Text = "El nombre de la categoría ya existe.";
                        LabelErrorCategoriaExistente.Visible = true;
                        return;
                    }
                }

                var categoria = new Categoria
                {
                    Id = Convert.ToInt32(LabelId.Text),
                    nombre = TextBoxNombre.Text,
                    estado = CheckBoxEstado.Checked
                };

                categoriaNegocio.ActualizarCategoria(categoria);
                Response.Redirect("../Categorias.aspx");
            }
            catch (Exception ex)
            {
                LabelError.Text = "Ocurrió un error al guardar la categoría.";
                LabelError.Visible = true;
                
                // mensaje en la consola de desarrollo.
                Console.WriteLine(ex.Message);
            }
        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {

            // Redirige a la página de lista de productos
            Response.Redirect("../Categorias.aspx");
        }



    }
}
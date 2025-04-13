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
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {



                // Verificar si el usuario tiene permisos para acceder a esta página
                if (Session["cliente"] == null || !EsAdministradorOSoporte((Cliente)Session["cliente"]))
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
            }

        }


        private bool EsAdministradorOSoporte(Cliente cliente)
        {
            return cliente.nombrePerfil == "Administrador" || cliente.nombrePerfil == "Soporte" || cliente.nombrePerfil == "Vendedor";
        }



        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
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

                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

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
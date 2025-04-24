using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace Front.MarcasABM
{
    public partial class MarcaAgregar : System.Web.UI.Page
    {



        MarcaNegocio marcaNegocio = new MarcaNegocio();
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
            LabelErrorMarcaExistente.Text = "";
            LabelError.Text = "";


            string NombreNuevo = TextBoxNombre.Text;
            List<Marca> listaDeMarca = new List<Marca>();
            listaDeMarca = marcaNegocio.ListarMarcas();


            // Verificar si el nombre ya existe
            foreach (var categoria in listaDeMarca)
            {
                if (categoria.nombre.Equals(NombreNuevo, StringComparison.OrdinalIgnoreCase))
                {
                    LabelErrorMarcaExistente.Text = "El nombre de la Marca ya existe.";
                    LabelErrorMarcaExistente.Visible = true;
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



                var marca = new Marca
                {
                    nombre = TextBoxNombre.Text,
                    estado = CheckBoxEstado.Checked
                };

                // Implementa la lógica para agregar la marca en la base de datos
                marcaNegocio.AgregarMarca(marca);

                // Redirige a la página de lista de marcas
                Response.Redirect("../Marcas.aspx");
            }
            catch (Exception)
            {
                // Manejar cualquier error inesperado
                LabelError.Text = "Ocurrió un error al guardar la marca. Intente nuevamente.";
                LabelError.Visible = true;
            }

        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            // Redirige a la página de lista de productos
            Response.Redirect("../Marcas.aspx");


        }
    }
}
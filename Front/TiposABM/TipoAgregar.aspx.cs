using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace Front.TiposABM
{
    public partial class TipoAgregar : System.Web.UI.Page
    {

        TipoNegocio tipoNegocio = new TipoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {




                // Verificar si el usuario tiene permisos para acceder a esta página
                if (Session["cliente"] == null || !EsAdministradorOSoporte((Usuario)Session["cliente"]))
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
            }


        }

        private bool EsAdministradorOSoporte(Usuario cliente)
        {
            return cliente.nombrePerfil == "Administrador" || cliente.nombrePerfil == "Soporte" || cliente.nombrePerfil == "Vendedor";
        }


        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {

            // Verificar si el usuario tiene permisos para acceder a esta página
            LabelErrorTipoExistente.Text = "";
            LabelError.Text = "";

            string NombreNuevo = TextBoxNombre.Text;
            List<Tipos> listaDeTipos = new List<Tipos>();
            listaDeTipos = tipoNegocio.ListarTipos();


            // Verificar si el nombre ya existe
            foreach (var tipo in listaDeTipos)
            {
                if (tipo.nombre.Equals(NombreNuevo, StringComparison.OrdinalIgnoreCase))
                {
                    LabelErrorTipoExistente.Text = "El nombre de la categoría ya existe.";
                    LabelErrorTipoExistente.Visible = true;
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

                TipoNegocio tipoNegocio = new TipoNegocio();

                var Tipo = new Tipos
                {
                    nombre = TextBoxNombre.Text,
                    estado = CheckBoxEstado.Checked
                };

                // Implementa la lógica para agregar el tipo en la base de datos
                tipoNegocio.AgregarTipo(Tipo);

                // Redirige a la página de lista de tipos
                Response.Redirect("../Tipos.aspx");
            }
            catch (Exception ex)
            {
                // Manejar cualquier error inesperado
                LabelError.Text = "Ocurrió un error al guardar el tipo. Intente nuevamente.";
                LabelError.Visible = true;
            }
        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            // Redirige a la página de lista de productos
            Response.Redirect("../Tipos.aspx");
        }


    }
}
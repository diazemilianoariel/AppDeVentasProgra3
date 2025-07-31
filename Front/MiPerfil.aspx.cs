using System;
using System.Web.UI;
using dominio;
using negocio;

namespace Front
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Siempre validamos que el usuario esté logueado
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                CargarDatosUsuario();
            }
        }

        private void CargarDatosUsuario()
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario usuario = (Usuario)Session["usuario"];

                // Buscamos los datos más frescos desde la BD por si hubo cambios
                Usuario datosActuales = negocio.ObtenerUsuarioPorId(usuario.Id);

                if (datosActuales != null)
                {
                    // Llenamos los campos del formulario con los datos del usuario
                    txtNombre.Text = datosActuales.Nombre;
                    txtApellido.Text = datosActuales.Apellido;
                    txtDni.Text = datosActuales.Dni;
                    txtTelefono.Text = datosActuales.Telefono;
                    txtDireccion.Text = datosActuales.Direccion;
                    txtEmail.Text = datosActuales.Email;
                }
            }
            catch (Exception ex)
            {
                // Manejar error si no se pueden cargar los datos
                // (Opcional: Podrías tener un Label de error en la página)
                Session["error"] = ex;
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Page.IsValid revisa todos los validadores del .aspx
                if (!Page.IsValid)
                    return;

                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario usuarioActual = (Usuario)Session["usuario"];

                // Actualizamos el objeto con los datos del formulario
                usuarioActual.Nombre = txtNombre.Text;
                usuarioActual.Apellido = txtApellido.Text;
                usuarioActual.Dni = txtDni.Text;
                usuarioActual.Telefono = txtTelefono.Text;
                usuarioActual.Direccion = txtDireccion.Text;

                // Lógica para actualizar la clave solo si se escribió algo
                if (!string.IsNullOrWhiteSpace(txtClaveNueva.Text))
                {
                    usuarioActual.clave = txtClaveNueva.Text;
                }

                // Llamamos al método de negocio que ya existe y funciona
                negocio.ModificarUsuario(usuarioActual);

                // Actualizamos el objeto en la sesión para que el "Bienvenido, ..." se refresque
                Session["usuario"] = usuarioActual;

               
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
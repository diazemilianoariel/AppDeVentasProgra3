using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Front
{
    public partial class RegistroUsuariosNuevos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                Response.Redirect("Default.aspx", false);
            }

            if (!IsPostBack)
            {
                lblMensaje.Visible = false;
            }

        }



        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Page.IsValid verifica todos los validadores del .aspx
                if (!Page.IsValid)
                    return;

                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                Usuario nuevoUsuario = new Usuario
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Dni = txtDni.Text,
                    Direccion = txtDireccion.Text,
                    Telefono = txtTelefono.Text,
                    Email = txtEmail.Text,
                    clave = txtClave.Text,
                    estado = true // Por defecto, los nuevos usuarios están activos.
                };

                // CORRECCIÓN: Se asigna el perfil de Cliente usando la nueva estructura.
                nuevoUsuario.Perfil = new Perfil();
                nuevoUsuario.Perfil.Id = (int)TipoPerfil.Cliente; // Siempre se registran como Clientes.

                // CORRECCIÓN: Se llama al método con el nombre correcto.
                usuarioNegocio.AgregarUsuario(nuevoUsuario);

                // Opcional: Loguear al usuario automáticamente después del registro.
                Session.Add("usuario", nuevoUsuario);

                // Redirigir a la página principal después de un registro exitoso.
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al registrar el usuario: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }

        }


        protected void btnVolverLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }



     
    }
}
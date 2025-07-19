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
            if (!IsPostBack)
            {
                lblMensaje.Visible = false;
            }

        }



        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) ||
                string.IsNullOrEmpty(txtDni.Text) || string.IsNullOrEmpty(txtDireccion.Text) ||
                string.IsNullOrEmpty(txtTelefono.Text) || string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtClave.Text))
            {
                lblMensaje.Text = "Por favor, complete todos los campos.";
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
                return;
            }



            Usuario nuevoCliente = new Usuario
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Dni = txtDni.Text,
                Direccion = txtDireccion.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                clave = txtClave.Text,
                idPerfil = 1, // Perfil de usuario siempre será cliente
                estado = true // Estado activo
            };
            UsuarioNegocio clienteNegocio = new UsuarioNegocio();
            try
            {
                clienteNegocio.AgregarCliente(nuevoCliente);
                lblMensaje.Text = "Usuario registrado con éxito.";
                lblMensaje.CssClass = "alert alert-success";
                lblMensaje.Visible = true;
                LimpiarCampos();
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



        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDni.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtClave.Text = string.Empty;
        }
    }
}
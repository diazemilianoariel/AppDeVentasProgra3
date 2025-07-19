using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMensaje.Visible = false;
            }
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                lblMensaje.Text = "Correo electrónico y contraseña son requeridos.";
                lblMensaje.Visible = true;
                return;
            }

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
           
            Usuario usuario = new Usuario
            {
                Email = txtEmail.Text,
                clave = txtPassword.Text
            };

            if (usuarioNegocio.Loguear(usuario))
            {
              
                Usuario usuarioCompleto = usuarioNegocio.ObtenerUsuarioPorEmail(usuario.Email);

                if (usuarioCompleto != null)
                {
                  
                    Session.Add("usuario", usuarioCompleto);

                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                
                    lblMensaje.Text = "Error al obtener los datos del usuario.";
                    lblMensaje.Visible = true;
                }
            }
            else
            {
                lblMensaje.Text = "Usuario o contraseña incorrectos.";
                lblMensaje.Visible = true;
            }
        }
    }
}

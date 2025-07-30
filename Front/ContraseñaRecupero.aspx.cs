using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using System.Linq.Expressions;

namespace Front
{
    public partial class ContraseñaRecupero : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMensaje.Text = "";

            }
           

        }


        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                string email = txtEmail.Text;

                // Buscamos la clave en texto plano (como lo tenías).
                string clave = negocio.RecuperarContraseña(email);

                if (!string.IsNullOrEmpty(clave))
                {
                    // Usamos nuestro EmailService centralizado.
                    EmailService emailService = new EmailService();
                    string asunto = "Recuperación de Contraseña";
                    string cuerpo = "Hola, recibimos una solicitud para recuperar tu contraseña. Tu contraseña es: <strong>" + clave + "</strong>";

                    emailService.EnviarCorreoConfirmacion(email, asunto, cuerpo);

                    lblMensaje.Text = "Se ha enviado un correo con tu contraseña.";
                    lblMensaje.CssClass = "alert alert-success";
                    lblMensaje.Visible = true;
                }
                else
                {
                    lblMensaje.Text = "No se encontró una cuenta con ese correo electrónico.";
                    lblMensaje.CssClass = "alert alert-danger";
                    lblMensaje.Visible = true;
                }
            }
            catch (Exception)
            {
                lblMensaje.Text = "Ocurrió un error. Por favor, intentá de nuevo más tarde.";
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }

       
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    
    
    }
}
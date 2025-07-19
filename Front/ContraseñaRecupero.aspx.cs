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
            UsuarioNegocio clienteNegocio = new UsuarioNegocio();
            string email = txtEmail.Text;
            string clave = clienteNegocio.RecuperarContraseña(email);
            if (clave != null)
            {
                EnviarCorreo(email, clave);
                lblMensaje.Text = "Se ha enviado un correo con su contraseña";
            }
            else
            {
                lblMensaje.Text = "No se ha encontrado el email";
            }



        }

        private void EnviarCorreo(string email, string clave)
        {
            string remitente = System.Configuration.ConfigurationManager.AppSettings["Email"];
            string contraseña = System.Configuration.ConfigurationManager.AppSettings["EmailPassword"];



            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress(remitente);//mail del que envia
            mail.Subject = "Recuperacion de contraseña";
            mail.Body = "Su contraseña es: " + clave;
            mail.IsBodyHtml = true;


            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.live.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(remitente, contraseña);

            try
            {
                smtp.Send(mail);
            }
            catch(Exception ex)
            {
                lblMensaje.Text = "Error al enviar el correo: " + ex.Message;
            }

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}
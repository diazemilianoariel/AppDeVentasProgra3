using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace negocio
{
    public class EmailService
    {


        private SmtpClient smtp;
        private string emailFrom;
        private string password;



        public EmailService()
        {
            // Leemos las credenciales desde el Web.config
            // Si no las tenés ahí, podés ponerlas directamente como strings.
            emailFrom = "arielemilianodiaz@gmail.com"; // TU CORREO DE GMAIL
            password = "zeuw efeg tgqq voen"; // TU CONTRASEÑA DE APLICACIÓN

            smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(emailFrom, password);
        }


        public void EnviarCorreoConfirmacion(string destinatario, string asunto, string cuerpo)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(destinatario);
                mail.Subject = asunto;
                mail.Body = cuerpo;
                mail.IsBodyHtml = true;

                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                // Es importante relanzar la excepción para que la página que llama
                // sepa que hubo un error y pueda mostrar un mensaje al usuario.
                throw ex;
            }
        }

        




    }
}
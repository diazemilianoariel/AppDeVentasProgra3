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
        public void EnviarCorreoConfirmacion(string destinatario, string asunto, string cuerpo)
        {

            try
            {
                string email = "arielemilianodiaz@gmail.com"; // Tu correo de Gmail
                string password = "pfcm bhcd kakp wcor"; // La contraseña de aplicación de Google

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(email);
                    mail.To.Add(destinatario);
                    mail.Subject = asunto;
                    mail.Body = cuerpo;
                    mail.IsBodyHtml = true; // Cambia a false si solo quieres texto plano

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(email, password);
                        smtp.EnableSsl = true;

                        smtp.Send(mail);
                        Console.WriteLine("Correo enviado correctamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar correo: " + ex.Message);
            }



        }




    }
}
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
                lblMensaje.Text = "correo electrónico y contraseña son requeridos.";
                lblMensaje.Visible = true;
                return;
            }


            ClienteNegocio Clientenegocio = new ClienteNegocio();
            Cliente cliente = new Cliente
            {


                Email = txtEmail.Text,
                clave = txtPassword.Text

            };

            if (Clientenegocio.Loguear(cliente))
            {
                Cliente clienteCompleto = Clientenegocio.ObtenerClientePorEmail(cliente.Email);

                if (clienteCompleto != null)
                {

                    Session["cliente"] = clienteCompleto;
                    Session["Perfil"] = clienteCompleto.idPerfil;
                    // deberia loguearse en la pagina que se esta actualmente 
                    Response.Redirect("Default.aspx");

                }
                else
                {
                    lblMensaje.Text = "Error al obtener los datos del cliente.";
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
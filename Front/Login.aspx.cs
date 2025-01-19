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
                string mensaje = Request.QueryString["mensaje"];
                if (!string.IsNullOrEmpty(mensaje))
                {
                    lblMensaje.Text = mensaje;
                    lblMensaje.Visible = true;
                }
                else
                {
                    lblMensaje.Text = "inicie sesion o registrese para poder operar.";
                    lblMensaje.Visible = true;
                }

            }



        }
        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            ClienteNegocio negocio = new ClienteNegocio();
            Cliente cliente = new Cliente
            {

                
                Email = txtEmail.Text,
                clave = txtPassword.Text

            };

            if (negocio.Loguear(cliente))
            {
                Session["cliente"] = cliente;
                // deberia loguearse en la pagina que se esta actualmente 
                Response.Redirect("Default.aspx");

            }
            else
            {
                Response.Redirect("Login.aspx");

            }




           

        }

     
    }
}
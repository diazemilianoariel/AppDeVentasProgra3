using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Front
{
    public partial class MASTER : System.Web.UI.MasterPage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {



            //Cliente cliente = new Cliente();
            // cliente = (Cliente)Session["cliente"];


            if (!IsPostBack)
            {
                ActualizarContadorCarrito();



            }

            Cliente cliente = (Cliente)Session["cliente"];
            if (cliente != null)
            {
                lblNombre.Text = "Bienvenido " +cliente.Nombre  ;
            }



        }

        public void ActualizarContadorCarrito(int totalProductos = 0)
        {
            ActualizarCarrito.InnerText = totalProductos.ToString();
            ScriptManager.RegisterStartupScript(this,GetType(), "ActualizarContadorCarrito", $"actualizarContadorCarrito({totalProductos});", true);
        }


        public string CartCountClientID
        {
            get { return ActualizarCarrito.ClientID; }
        }


        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            // Eliminar la sesión del usuario
            Session["cliente"] = null;
            Session.Abandon();

            // Redirigir a la página de inicio de sesión
            Response.Redirect("Login.aspx");
        }
    }
}
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
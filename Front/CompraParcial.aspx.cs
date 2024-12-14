using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Front
{
    public partial class CompraParcial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // recuperar session 
            int idProducto = Convert.ToInt32(Session["idProducto"]);

            lblIdProducto.Text = idProducto.ToString();

        }
    }
}
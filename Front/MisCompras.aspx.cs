using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;


namespace Front
{
    public partial class MisCompras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["cliente"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            if (!IsPostBack)
            {
                CargarCompras();
            }

        }


        private void CargarCompras()
        {
            
            Cliente cliente = (Cliente)Session["cliente"];
            negocio.FacturaNegocio negocio = new negocio.FacturaNegocio();
            gvMisCompras.DataSource = negocio.ListarFacturas(cliente.Id);
            gvMisCompras.DataBind();


        }
    }
}
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



        protected void gvMisCompras_RowCommand(object sender, GridViewCommandEventArgs e)
        {





            if (e.CommandName == "VerFactura")
            {

                string[] argumentos = e.CommandArgument.ToString().Split('|');
                decimal valorDecimal = Convert.ToDecimal(argumentos[1]);


                int IdVenta = Convert.ToInt32(argumentos[0]);
                Session["IdVenta"] = IdVenta;


                Session["TotalFactura"] = valorDecimal;

                Response.Redirect("Factura.aspx");
            }


        }
    }
}
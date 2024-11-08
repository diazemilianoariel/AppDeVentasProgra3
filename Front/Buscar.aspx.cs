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
    public partial class Buscar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string query = Request.QueryString["query"];
                if (!string.IsNullOrEmpty(query))
                {

                    // Llamar al método BuscarProducto, pasándole el query como string
                    ProductoNegocio negocio = new ProductoNegocio();
                 
                }
                else
                {
                    CargarGrilla();
                   
                }
            }
        }


        private void CargarGrilla()
        {
            ProductoNegocio negocio = new ProductoNegocio();
           
        }

        protected void GridViewProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        //ButtonBuscar_Click
        protected void ButtonBuscar_Click(object sender, EventArgs e)
        {
            string query = TextBoxBuscar.Text;
            Response.Redirect("Buscar.aspx?query=" + query);
        }

        //ButtonLimpiar_Click
        protected void ButtonLimpiar_Click(object sender, EventArgs e)
        {
            TextBoxBuscar.Text = "";
            Response.Redirect("Buscar.aspx");
        }





    }
}
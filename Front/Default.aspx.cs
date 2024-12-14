using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Front
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            List<Producto> listaProductos = productoNegocio.ListarProductos();
            Session["listaProductos"] = listaProductos; // Guardo la lista de productos en la sesion para poder accederla desde el front

            if (!IsPostBack)
            {
                CargarProductos();
            }



        }

        private void CargarProductos()
        {
            List<Producto> listaProductos = (List<Producto>)Session["listaProductos"]; // Recupero la lista de productos de la sesion
            rptProductos.DataSource = listaProductos;
            rptProductos.DataBind();
        }



        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            int idProducto = Convert.ToInt32((sender as LinkButton).CommandArgument);
            Session.Add("idProducto", idProducto);

            Response.Redirect("CompraParcial.aspx");
        }





    }
}

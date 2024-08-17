using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Front
{
    public partial class producto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // crear la definicion de boton1_click
        protected void btnAgregar(object sender, EventArgs e)
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            productoNegocio.AgregarProducto();
        }
    }
}
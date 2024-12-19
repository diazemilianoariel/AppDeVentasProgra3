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
    public partial class CompraParcial : System.Web.UI.Page
    {
        public List<Producto> ListaArticulos = new List<Producto>();


        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
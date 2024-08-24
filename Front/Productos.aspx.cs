using dominio;
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
        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            Producto producto = new Producto();

            producto.nombre = TextBoxNombre.Text;
            producto.descripcion = TextBoxDescripcion.Text;
            producto.imagen = TextBoxImagen.Text;
            producto.precio = Convert.ToDecimal(TextBoxPrecio.Text);

            // instanciar la clase ProductoNegocio
            ProductoNegocio negocio = new ProductoNegocio();
            // llamar al metodo AgregarProducto
            negocio.AgregarProducto(producto);


        }
    }
}
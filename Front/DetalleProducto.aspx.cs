using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Front
{
    public partial class DetalleProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtén el ID del producto de la query string
                string productoId = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(productoId))
                {
                    // Llama a un método para cargar los detalles del producto basado en el ID
                    CargarDetallesProducto(productoId);
                }
            }

        }

        private void CargarDetallesProducto(string productoId)
        {
            // Aquí debes agregar la lógica para obtener los detalles del producto desde la base de datos
            // Simulación de datos obtenidos
            LabelNombreProducto.Text = "Nombre del Producto";
            LabelDescripcionProducto.Text = "Descripción detallada del producto.";
            LabelPrecioProducto.Text = "$100.00";
            ImageProducto.ImageUrl = "URL de la imagen del producto";
        }
    }
}
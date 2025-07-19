using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front.Productos
{
	public partial class ProductoEliminar : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{


            if (!IsPostBack)
            {
                int productId = Convert.ToInt32(Request.QueryString["id"]);
                CargarDatosProducto(productId);
            }

            if (Session["cliente"] == null || !IDPerfilValido())
            {
                Response.Redirect("Login.aspx");
                return;
            }




        }


        private void CargarDatosProducto(int productId)
        {
            // Implementa la lógica para obtener los datos del producto de la base de datos
            ProductoNegocio productoNegocio = new ProductoNegocio();
            var producto = productoNegocio.ObtenerProducto(productId);
            if (producto != null)
            {
                LabelNombreProducto.Text = producto.nombre;
                LabelDescripcionProducto.Text = producto.descripcion;
                LabelPrecioProducto.Text = producto.precio.ToString();
                LabelStockProducto.Text = producto.stock.ToString();
                LabelMarcaProducto.Text = producto.Marca.nombre;
                ImageProducto.ImageUrl = producto.Imagen;
                LabelTipoProducto.Text = producto.Tipo.nombre;
                LabelCategoriaProducto.Text = producto.Categoria.nombre;
                LabelProveedorProducto.Text = producto.proveedor.Nombre;
                LabelEstadoProducto.Text = producto.estado.ToString();




            }
            else
            {
                // Manejar el caso en que no se encuentra el producto
                LabelError.Text = "Producto no encontrado.";
                LabelError.Visible = true;
            }
            

        }

     

        public bool IDPerfilValido()
        {

            // Verifica si el cliente tiene un perfil válido

            Usuario cliente = (Usuario)Session["cliente"];
            return cliente.idPerfil == 2 || cliente.idPerfil == 4 || cliente.idPerfil == 3;
        }



        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            // Lógica para eliminar el producto

            int productId = Convert.ToInt32(Request.QueryString["id"]);
            ProductoNegocio productoNegocio = new ProductoNegocio();
            productoNegocio.EliminarProducto(productId);
            // Redirigir a la página de productos después de eliminar
            Response.Redirect("../Productos.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Productos.aspx");
        }

    }
}
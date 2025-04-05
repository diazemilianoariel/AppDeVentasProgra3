using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace Front
{
    public partial class DetalleProducto : System.Web.UI.Page
    {
        private string mensaje = "Ocurrió un error al cargar los detalles del producto. Por favor, inténtelo de nuevo.";

        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["cliente"] == null || !IDPerfilValido())
            {
                Response.Redirect("Login.aspx");
                return;
            }




            if (!IsPostBack)
            {



                // Obtén el ID del producto de la query string
                string productoId = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(productoId))
                {
                    // Llama a un método para cargar los detalles del producto basado en el ID
                    CargarDetallesProducto(productoId);
                }
                else
                {
                    MostrarError("Debe seleccionar un producto.");
                }


            }




        }

        public bool IDPerfilValido()
        {
            Cliente cliente = (Cliente)Session["cliente"];
            return cliente.idPerfil == 2;
        }

        private void MostrarError(string v)
        {
            LabelError.Text = mensaje;
            LabelError.Visible = true;
            btnVolver.Visible = true;
        }

        private void CargarDetallesProducto(string productoId)
        {
            // Aquí debes agregar la lógica para obtener los detalles del producto desde la base de datos
            // Simulación de datos obtenidos.

            // Asignar los valores a los controles de la página

            try
            {
                ProductoNegocio negocio = new ProductoNegocio();
                Producto producto = negocio.ObtenerProducto(Convert.ToInt32(productoId));


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
            catch (Exception)
            {
                MostrarError("Ocurrió un error al cargar los detalles del producto. Por favor, inténtelo de nuevo.");



            }



        }



        protected void btnVolver_Click(object sender, EventArgs e)
        {
            // Redirigir a la página anterior o a la lista de productos
            Response.Redirect("../Productos.aspx");
        }


    }
}
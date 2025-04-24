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

            return  cliente.nombrePerfil == "Administrador" || cliente.nombrePerfil == "Soporte" || cliente.nombrePerfil == "Vendedor";
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





        private void ActualizarContadorCarrito()
        {
            List<Producto> carrito = (List<Producto>)Session["Carrito"];
            int totalProductos = carrito != null ? carrito.Sum(p => p.Cantidad) : 0;
            var masterPage = (MASTER)this.Master;
            masterPage.ActualizarContadorCarrito(totalProductos);
        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {

            DefaultNegocio defaultNegocio = new DefaultNegocio();

            try
            {
               
                int idProducto = Convert.ToInt32(Request.QueryString["id"]);

                Producto producto = defaultNegocio.ObtenerProducto(idProducto);

                if (producto != null)
                {
                    List<Producto> carrito = (List<Producto>)Session["Carrito"] ?? new List<Producto>();
               //     RepeaterItem item = (RepeaterItem)((Button)sender).NamingContainer;
                 //   TextBox txtCantidad = (TextBox)item.FindControl("txtCantidad");
                    int cantidad = 1;


                    if (producto.stock >= cantidad)
                    {
                        defaultNegocio.AgregarProductosAlCarrito(carrito, producto, cantidad);


                        Session["Carrito"] = carrito;
                        //MostrarMensaje("Producto agregado al carrito.", false);
                        ActualizarContadorCarrito();


                        ScriptManager.RegisterStartupScript(this, GetType(), "ScrollToMessage", "scrollToMessage();", true);

                        ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", "setTimeout(function() { window.location.href = 'Default.aspx'; }, 2000);", true);

                    }
                    else
                    {
                       // MostrarMensaje("No hay suficiente stock disponible para agregar la cantidad solicitada.", true);

                    }
                }
            }

            catch (Exception )
            {
              //  MostrarMensaje("Error al agregar producto al carrito: " + ex.Message, true);
            }
        }



        //protected void btnQuitarCarrito_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int idProducto = Convert.ToInt32(((Button)sender).CommandArgument);
        //        List<Producto> carrito = ObtenerCarrito();
        //        if (carrito != null)
        //        {
        //            defaultNegocio.QuitarProductoDelCarrito(carrito, idProducto);
        //            Session["Carrito"] = carrito;
        //            MostrarMensaje("Producto eliminado del carrito.", false);
        //            ActualizarContadorCarrito();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MostrarMensaje("Error al quitar producto del carrito: " + ex.Message, true);
        //    }
        //}


    }
}
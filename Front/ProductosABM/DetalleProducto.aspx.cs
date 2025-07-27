using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace Front.ProductosABM
{
    public partial class DetalleProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // CORRECCIÓN 1: La validación ahora es más simple.
            // Si no hay un usuario en la sesión, no puede ver la página.
            if (Session["usuario"] == null)
            {
                // La ruta es correcta porque sube un nivel desde la carpeta /Productos.
                Response.Redirect("../Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                string productoId = Request.QueryString["Id"];
                if (!string.IsNullOrEmpty(productoId))
                {
                    CargarDetallesProducto(productoId);
                }
                else
                {
                    // Si no hay ID, redirigimos al catálogo.
                    Response.Redirect("../Default.aspx");
                }
            }
        }

        private void CargarDetallesProducto(string productoId)
        {
            try
            {
                ProductoNegocio negocio = new ProductoNegocio();
                Producto producto = negocio.ObtenerProductoPublico(Convert.ToInt32(productoId));

                if (producto != null)
                {
                    LabelNombreProducto.Text = producto.nombre;
                    LabelDescripcionProducto.Text = producto.descripcion;
                    LabelPrecioProducto.Text = producto.precioVenta.ToString("F2"); // Formato de moneda
                    LabelStockProducto.Text = producto.stock.ToString();
                    LabelMarcaProducto.Text = producto.Marca.nombre;
                    ImageProducto.ImageUrl = producto.Imagen;
                    LabelTipoProducto.Text = producto.Tipo.nombre;
                    LabelCategoriaProducto.Text = producto.Categoria.nombre;

                    if (producto.Proveedores != null && producto.Proveedores.Any())
                    {
                        // Unimos los nombres de todos los proveedores en un solo string, separados por ", "
                        LabelProveedorProducto.Text = string.Join(", ", producto.Proveedores.Select(p => p.Nombre));
                    }
                    else
                    {
                        // Si no hay proveedores, mostramos un texto por defecto.
                        LabelProveedorProducto.Text = "No especificado";
                    }



                    LabelEstadoProducto.Text = producto.estado ? "Activo" : "Inactivo";
                }
                else
                {
                    MostrarError("El producto que buscas no existe.");
                }
            }
            catch (Exception)
            {
                MostrarError("Ocurrió un error al cargar los detalles del producto.");
            }
        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                int idProducto = Convert.ToInt32(Request.QueryString["id"]);

                // Usamos la capa de negocio correcta y el método que sabe de ofertas
                ProductoNegocio negocio = new ProductoNegocio();
                Producto productoAAgregar = negocio.ObtenerProductoPublico(idProducto);

                if (productoAAgregar != null)
                {
                    List<Producto> carrito = (List<Producto>)Session["Carrito"] ?? new List<Producto>();
                    int cantidadDeseada = Convert.ToInt32(txtCantidad.Text);

                    if (productoAAgregar.stock >= cantidadDeseada)
                    {
                        // Buscamos si el producto ya existe en el carrito
                        Producto productoEnCarrito = carrito.FirstOrDefault(p => p.id == idProducto);

                        if (productoEnCarrito != null)
                        {
                            // Si ya está, solo actualizamos la cantidad
                            productoEnCarrito.Cantidad += cantidadDeseada;
                        }
                        else
                        {
                            // Si es nuevo, le asignamos la cantidad y lo agregamos
                            productoAAgregar.Cantidad = cantidadDeseada;
                            carrito.Add(productoAAgregar);
                        }

                        Session["Carrito"] = carrito;

                        // Redirigimos a la página del carrito para que el usuario vea su producto
                        Response.Redirect("~/Carrito.aspx", false);
                    }
                    else
                    {
                        MostrarError("No hay suficiente stock disponible.");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError("Ocurrió un error al agregar el producto al carrito.");
                // Opcional: registrar ex.ToString() en un log de errores.
            }
        }

        protected void btnDisminuir_Click(object sender, EventArgs e)
        {
            int cantidad = Convert.ToInt32(txtCantidad.Text);
            if (cantidad > 1)
            {
                txtCantidad.Text = (cantidad - 1).ToString();
            }
        }

        protected void btnAumentar_Click(object sender, EventArgs e)
        {
            int stockDisponible = Convert.ToInt32(LabelStockProducto.Text);
            int cantidad = Convert.ToInt32(txtCantidad.Text);
            if (cantidad < stockDisponible)
            {
                txtCantidad.Text = (cantidad + 1).ToString();
            }
        }

        private void MostrarError(string mensaje)
        {
            LabelError.Text = mensaje;
            LabelError.Visible = true;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }
    }
}

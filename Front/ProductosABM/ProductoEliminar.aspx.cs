using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front.ProductosABM
{
	public partial class ProductoEliminar : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null || !EsAdmin(usuario))
            {
               
                Response.Redirect("../Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int productId = Convert.ToInt32(Request.QueryString["id"]);
                    CargarDatosProducto(productId);
                }
                else
                {
                    Response.Redirect("../Productos.aspx");
                }
            }

           




        }

        private bool EsAdmin(Usuario usuario)
        {
            // Según el plan, solo los Administradores pueden eliminar productos.
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }


        private void CargarDatosProducto(int productId)
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            var producto = productoNegocio.ObtenerProducto(productId);
            if (producto != null)
            {
                LabelNombreProducto.Text = producto.nombre;
                LabelDescripcionProducto.Text = producto.descripcion;
                LabelPrecioProducto.Text = producto.precio.ToString("C"); // Formato de moneda
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
                LabelError.Text = "Producto no encontrado.";
                LabelError.Visible = true;
                btnConfirmar.Visible = false;
            }


        }

     
 



        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                int productId = Convert.ToInt32(Request.QueryString["id"]);
                ProductoNegocio productoNegocio = new ProductoNegocio();
                productoNegocio.bajaLogicaProducto(productId); 
                Response.Redirect("../Productos.aspx");
            }
            catch (Exception ex)
            {
                LabelError.Text = "Ocurrió un error al eliminar el producto." + ex.Message;
                LabelError.Visible = true;
                

            }
        }   

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Productos.aspx");
        }

    }
}
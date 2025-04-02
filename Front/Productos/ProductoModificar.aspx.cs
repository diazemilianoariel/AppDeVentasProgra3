using dominio;
using negocio;
using System;

namespace Front.Productos
{
    public partial class ProductoModificar : System.Web.UI.Page
    {
        ProductoNegocio productoNegocio = new ProductoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LLenarDropDownLists();
                int productId = Convert.ToInt32(Request.QueryString["id"]);
                CargarDatosProducto(productId);
            }
        }


        private void LLenarDropDownLists()
        {
            // Cargar Marcas
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            DropDownListMarca.DataSource = marcaNegocio.ListarMarcas();
            DropDownListMarca.DataTextField = "nombre";
            DropDownListMarca.DataValueField = "id";
            DropDownListMarca.DataBind();

            // Cargar Tipos
            TipoNegocio tipoNegocio = new TipoNegocio();
            DropDownListTipo.DataSource = tipoNegocio.ListarTipos();
            DropDownListTipo.DataTextField = "nombre";
            DropDownListTipo.DataValueField = "id";
            DropDownListTipo.DataBind();

            // Cargar Categorías
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            DropDownListCategoria.DataSource = categoriaNegocio.ListarCategorias();
            DropDownListCategoria.DataTextField = "nombre";
            DropDownListCategoria.DataValueField = "id";
            DropDownListCategoria.DataBind();

            // Cargar Proveedores
            ProveedoresNegocio proveedorNegocio = new ProveedoresNegocio();
            DropDownListProveedor.DataSource = proveedorNegocio.ListarProveedores();
            DropDownListProveedor.DataTextField = "nombre";
            DropDownListProveedor.DataValueField = "id";
            DropDownListProveedor.DataBind();
        }

        private void CargarDatosProducto(int productId)
        {
            // Implementa la lógica para obtener los datos del producto de la base de datos
            var producto = productoNegocio.ObtenerProducto(productId);
            if (producto != null)
            {
                LabelId.Text = producto.id.ToString();
                TextBoxNombre.Text = producto.nombre;
                TextBoxDescripcion.Text = producto.descripcion;
                TextBoxPrecio.Text = producto.precio.ToString();
                TextBoxGanancia.Text = producto.margenGanancia.ToString();
                TextBoxStock.Text = producto.stock.ToString();
                TextBoxImagen.Text = producto.Imagen;
                ImagenProducto.Src = producto.Imagen;
                DropDownListMarca.SelectedValue = producto.Marca.id.ToString();
                DropDownListTipo.SelectedValue = producto.Tipo.id.ToString();
                DropDownListCategoria.SelectedValue = producto.Categoria.id.ToString();
                DropDownListProveedor.SelectedValue = producto.proveedor.id.ToString();
                CheckBoxEstado.Checked = producto.estado;
            }
        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            int productId = Convert.ToInt32(LabelId.Text);
            var producto = new Producto
            {
                id = productId,
                nombre = TextBoxNombre.Text,
                descripcion = TextBoxDescripcion.Text,
                precio = Convert.ToDecimal(TextBoxPrecio.Text),
                margenGanancia = Convert.ToDecimal(TextBoxGanancia.Text),
                stock = Convert.ToInt32(TextBoxStock.Text),
                Imagen = TextBoxImagen.Text,
                Marca = new Marca { id = Convert.ToInt32(DropDownListMarca.SelectedValue) },
                Tipo = new Tipos { id = Convert.ToInt32(DropDownListTipo.SelectedValue) },
                Categoria = new Categoria { id = Convert.ToInt32(DropDownListCategoria.SelectedValue) },
                proveedor = new Proveedor { id = Convert.ToInt32(DropDownListProveedor.SelectedValue) },
                estado = CheckBoxEstado.Checked
            };



            // Implementa la lógica para actualizar el producto en la base de datos
            productoNegocio.ModificarProducto(producto);

            // Redirige a la página de lista de productos
            Response.Redirect("../Productos.aspx");
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            // Redirige a la página de lista de productos sin guardar cambios
            Response.Redirect("../Productos.aspx");
        }
    }
}
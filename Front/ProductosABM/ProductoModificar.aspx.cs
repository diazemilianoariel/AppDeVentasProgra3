using dominio;
using negocio;
using System;

namespace Front.ProductosABM
{
    public partial class ProductoModificar : System.Web.UI.Page
    {
        ProductoNegocio productoNegocio = new ProductoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null || !EsPerfilValido(usuario))
            {
                Response.Redirect("../Login.aspx");
                return;
            }

            if (!IsPostBack)
            {

                LLenarDropDownLists();
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




        private bool EsPerfilValido(Usuario usuario)
        {

            return usuario.Perfil != null && (usuario.Perfil.Id == (int)TipoPerfil.Administrador || usuario.Perfil.Id == (int)TipoPerfil.Vendedor);
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
                DropDownListMarca.SelectedValue = producto.Marca.Id.ToString();
                DropDownListTipo.SelectedValue = producto.Tipo.Id.ToString();
                DropDownListCategoria.SelectedValue = producto.Categoria.Id.ToString();
                DropDownListProveedor.SelectedValue = producto.proveedor.Id.ToString();
                CheckBoxEstado.Checked = producto.estado;
            }
        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            try
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
                    Marca = new Marca { Id = Convert.ToInt32(DropDownListMarca.SelectedValue) },
                    Tipo = new dominio.Tipos { Id = Convert.ToInt32(DropDownListTipo.SelectedValue) },
                    Categoria = new Categoria { Id = Convert.ToInt32(DropDownListCategoria.SelectedValue) },
                    proveedor = new Proveedor { Id = Convert.ToInt32(DropDownListProveedor.SelectedValue) },
                    estado = CheckBoxEstado.Checked
                };

                productoNegocio.ModificarProducto(producto);
                Response.Redirect("../Productos.aspx");
            }
            catch (Exception ex)
            {
                // Aquí podrías añadir un Label de error y mostrarlo.
                 LabelError.Text = "Error al guardar el producto: " + ex.Message;
                 LabelError.Visible = true;
            }
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("../Productos.aspx");
        }
    }
}
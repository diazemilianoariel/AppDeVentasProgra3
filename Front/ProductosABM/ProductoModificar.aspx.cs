using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web.UI.WebControls;

namespace Front.ProductosABM
{
    public partial class ProductoModificar : System.Web.UI.Page
    {
        // Es buena práctica declarar las clases de negocio a nivel de clase si se usan en múltiples métodos.
        private readonly ProductoNegocio productoNegocio = new ProductoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null || !EsPerfilValido(usuario))
            {
                Response.Redirect("../Login.aspx");
                return;
            }


            if (Session["MensajeReactivacion"] != null)
            {
                LabelError.Text = Session["MensajeReactivacion"].ToString();
                LabelError.CssClass = "alert alert-info"; // Un color más amigable
                LabelError.Visible = true;
                Session["MensajeReactivacion"] = null; // Lo borramos para que no aparezca de nuevo
            }



            if (!IsPostBack)
            {
               
                LlenarControles();

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
            // La lógica para validar el perfil del usuario está bien.
            return usuario.Perfil != null && (usuario.Perfil.Id == (int)TipoPerfil.Administrador || usuario.Perfil.Id == (int)TipoPerfil.Vendedor);
        }

        private void LlenarControles()
        {
            // Cargar Marcas, Tipos y Categorías (sin cambios)
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            DropDownListMarca.DataSource = marcaNegocio.ListarMarcas();
            DropDownListMarca.DataTextField = "nombre";
            DropDownListMarca.DataValueField = "id";
            DropDownListMarca.DataBind();

            TipoNegocio tipoNegocio = new TipoNegocio();
            DropDownListTipo.DataSource = tipoNegocio.ListarTipos();
            DropDownListTipo.DataTextField = "nombre";
            DropDownListTipo.DataValueField = "id";
            DropDownListTipo.DataBind();

            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            DropDownListCategoria.DataSource = categoriaNegocio.ListarCategorias();
            DropDownListCategoria.DataTextField = "nombre";
            DropDownListCategoria.DataValueField = "id";
            DropDownListCategoria.DataBind();

            // CORRECCIÓN: Cargar Proveedores en el CheckBoxList
            ProveedoresNegocio proveedorNegocio = new ProveedoresNegocio();
            cblProveedores.DataSource = proveedorNegocio.ListarProveedores();
            cblProveedores.DataTextField = "nombre";
            cblProveedores.DataValueField = "id";
            cblProveedores.DataBind();
        }

        private void CargarDatosProducto(int productId)
        {
            var producto = productoNegocio.ObtenerProductoParaAdmin(productId);
            if (producto != null)
            {
                LabelId.Text = producto.id.ToString();
                TextBoxNombre.Text = producto.nombre;
                TextBoxDescripcion.Text = producto.descripcion;
                TextBoxPrecio.Text = producto.precio.ToString("F2"); // Usar F2 para asegurar formato decimal
                TextBoxGanancia.Text = producto.margenGanancia.ToString("F2");
                TextBoxStock.Text = producto.stock.ToString();
                TextBoxImagen.Text = producto.Imagen;
                ImagenProducto.Src = producto.Imagen; // Asumiendo que es un <img id="ImagenProducto" runat="server">

                // Seleccionar los valores en los DropDownLists
                DropDownListMarca.SelectedValue = producto.Marca.Id.ToString();
                DropDownListTipo.SelectedValue = producto.Tipo.Id.ToString();
                DropDownListCategoria.SelectedValue = producto.Categoria.Id.ToString();
                CheckBoxEstado.Checked = producto.estado;

                // CORRECCIÓN: Seleccionar los proveedores en el CheckBoxList
                foreach (ListItem item in cblProveedores.Items)
                {
                    // Usamos LINQ (.Any) para ver si la lista de proveedores del producto contiene el ID del item actual.
                    if (producto.Proveedores.Any(p => p.Id == int.Parse(item.Value)))
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Construimos el objeto producto para enviar a la capa de negocio
                var producto = new Producto
                {
                    id = Convert.ToInt32(LabelId.Text),
                    nombre = TextBoxNombre.Text,
                    descripcion = TextBoxDescripcion.Text,
                    precio = Convert.ToDecimal(TextBoxPrecio.Text),
                    margenGanancia = Convert.ToDecimal(TextBoxGanancia.Text),
                    stock = Convert.ToInt32(TextBoxStock.Text),
                    Imagen = TextBoxImagen.Text,
                    Marca = new Marca { Id = Convert.ToInt32(DropDownListMarca.SelectedValue) },
                    
                    Tipo = new dominio.Tipos { Id = Convert.ToInt32(DropDownListTipo.SelectedValue) },
                    Categoria = new Categoria { Id = Convert.ToInt32(DropDownListCategoria.SelectedValue) },
                    estado = CheckBoxEstado.Checked,
                    Proveedores = new List<Proveedor>() // CORRECCIÓN: Inicializamos la lista de proveedores
                };

                // CORRECCIÓN: Llenamos la lista de proveedores a partir de los checkboxes seleccionados
                foreach (ListItem item in cblProveedores.Items)
                {
                    if (item.Selected)
                    {
                        producto.Proveedores.Add(new Proveedor { Id = int.Parse(item.Value) });
                    }
                }

                // Llamamos al método de negocio que ya está preparado para la transacción
                productoNegocio.ModificarProducto(producto);
                Response.Redirect("../Productos.aspx");
            }
            catch (Exception ex)
            {
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
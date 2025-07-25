using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;


using System.Data;



namespace Front.ProductosABM
{
    public partial class ProductoAgregar : System.Web.UI.Page
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
                // Solo si el usuario tiene permisos, cargamos los datos iniciales.
                CargarDatosIniciales();
            }
        }

        private bool EsAdmin(Usuario usuario)
        {
            
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ProductoNegocio productoNegocio = new ProductoNegocio();
                var producto = new Producto
                {
                    nombre = TextBoxNombre.Text,
                    descripcion = TextBoxDescripcion.Text,
                    precio = Convert.ToDecimal(TextBoxPrecio.Text),
                    margenGanancia = Convert.ToDecimal(TextBoxGanancia.Text),
                    stock = Convert.ToInt32(TextBoxStock.Text),
                    Imagen = TextBoxImagen.Text,
                    Marca = new Marca { Id = Convert.ToInt32(DropDownListMarca.SelectedValue) },
                    Tipo = new dominio.Tipos { Id = Convert.ToInt32(DropDownListTipo.SelectedValue) },
                    Categoria = new Categoria { Id = Convert.ToInt32(DropDownListCategoria.SelectedValue) },

                    Proveedores = new List<Proveedor>(),

                    estado = CheckBoxEstado.Checked
                };



                foreach (ListItem item in cblProveedores.Items)
                {
                    if (item.Selected)
                    {
                        producto.Proveedores.Add(new Proveedor { Id = Convert.ToInt32(item.Value) });
                    }
                }


                productoNegocio.AgregarProducto(producto);
                Response.Redirect("../Productos.aspx");
            }
            catch (FormatException)
            {
                LabelError.Text = "Error en el formato de los datos ingresados. Por favor, revise los campos numéricos.";
                LabelError.Visible = true;
            }
            catch (Exception ex)
            {
                LabelError.Text = "Ocurrió un error inesperado al guardar el producto.";
                LabelError.Visible = true;

                

            }

        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("../Productos.aspx");
        }

        private void CargarDatosIniciales()
        {

            try
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
                cblProveedores.DataSource = proveedorNegocio.ListarProveedores();
                cblProveedores.DataTextField = "nombre";
                cblProveedores.DataValueField = "id";
                cblProveedores.DataBind();

            }
            catch(Exception ex)
            {
                LabelError.Text = "Ocurrió un error al cargar los datos iniciales." +  ex.Message;
                LabelError.Visible = true;
                return; 
            }

        }

       
    }

}



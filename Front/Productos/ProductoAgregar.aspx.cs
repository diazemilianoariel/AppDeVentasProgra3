using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;


using System.Data;



namespace Front.Productos
{
    public partial class ProductoAgregar : System.Web.UI.Page
    {



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar datos iniciales, como llenar los DropDownList
                CargarDatosIniciales();


                // Verificar si el usuario tiene permisos para acceder a esta página
                if (Session["cliente"] == null || !EsAdministradorOSoporte((Usuario)Session["cliente"]))
                {
                    Response.Redirect("Login.aspx");
                    return;
                }


            }
        }

        private bool EsAdministradorOSoporte(Usuario cliente)
        {
            return cliente.nombrePerfil == "Administrador" || cliente.nombrePerfil == "Soporte" || cliente.nombrePerfil == "Vendedor";
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
                    Marca = new Marca { id = Convert.ToInt32(DropDownListMarca.SelectedValue) },
                    Tipo = new Tipos { id = Convert.ToInt32(DropDownListTipo.SelectedValue) },
                    Categoria = new Categoria { id = Convert.ToInt32(DropDownListCategoria.SelectedValue) },
                    proveedor = new Proveedor { id = Convert.ToInt32(DropDownListProveedor.SelectedValue) },
                    estado = CheckBoxEstado.Checked
                };

                // Implementa la lógica para agregar el producto en la base de datos
                productoNegocio.AgregarProducto(producto);

                // Redirige a la página de lista de productos
                Response.Redirect("../Productos.aspx");
            }
            catch (FormatException)
            {
                // Manejar errores de formato
                LabelError.Text = "Error en el formato de los datos ingresados. Por favor, revise los campos.";
                LabelError.Visible = true;
            }
          
        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            // Redirige a la página de lista de productos sin guardar cambios
            Response.Redirect("../Productos.aspx");
        }

        private void CargarDatosIniciales()
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

       
    }

}



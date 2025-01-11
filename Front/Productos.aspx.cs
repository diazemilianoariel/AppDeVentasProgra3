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
    public partial class producto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["cliente"] == null || !EsAdministradorOSoporte((Cliente)Session["cliente"]))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarDropDownLists();
                CargarGrilla();
            }


        }

        private bool EsAdministradorOSoporte(Cliente cliente)
        {
            return cliente.nombrePerfil == "Administrador" || cliente.nombrePerfil == "Soporte" || cliente.nombrePerfil == "Vendedor";
        }


        private void CargarDropDownLists()
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


        private void CargarGrilla()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            GridViewProductos.DataSource = negocio.ListarProductos();
            GridViewProductos.DataBind();
        }



        protected void GridViewProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int index = Convert.ToInt32(e.CommandArgument);

               
                    GridViewRow row = GridViewProductos.Rows[index];
                    if (row != null && row.Cells.Count > 0) // Asegurarse de que la fila y la celda existen
                    {
                        int id = Convert.ToInt32(row.Cells[0].Text);

                        ProductoNegocio negocio = new ProductoNegocio();
                        Producto producto = negocio.ObtenerProducto(id);

                        // Asignar los valores a los TextBoxes
                        TextBoxId.Text = producto.id.ToString();
                        TextBoxNombre.Text = producto.nombre;
                        TextBoxDescripcion.Text = producto.descripcion;
                        TextBoxImagen.Text = producto.Imagen;
                        TextBoxPrecio.Text = producto.precio.ToString();
                        TextBoxGanancia.Text = producto.margenGanancia.ToString();
                        TextBoxStock.Text = producto.stock.ToString();

                        ListItem itemMarca = DropDownListMarca.Items.FindByValue(producto.marca);
                        if (itemMarca != null)
                        {
                            DropDownListMarca.ClearSelection();
                            itemMarca.Selected = true;

                        }


                        CheckBoxEstado.Checked = producto.estado;



                    }
                
               
            }
            else
            {
                if (e.CommandName == "VerDetalle")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    // Verificar si el índice está dentro del rango
                    if (index >= 0 && index < GridViewProductos.Rows.Count)
                    {
                        GridViewRow row = GridViewProductos.Rows[index];
                        if (row != null && row.Cells.Count > 0) // Asegurarse de que la fila y la celda existen
                        {
                            int id = Convert.ToInt32(row.Cells[0].Text);

                            // Redirigir a la página de detalle, pasando el ID como parámetro
                            Response.Redirect("DetalleProducto.aspx?id=" + id);
                        }
                        else
                        {
                            // mensaje de error
                            Response.Write("¡Ups! Parece que seleccionaste una fila inexistente.");
                        }
                    }
                    else
                    {
                        // mensaje de error
                        Response.Write("¡Ups! Parece que seleccionaste una fila inexistente.");
                    }
                }

            }
        }


        protected void GridViewProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow fila = GridViewProductos.SelectedRow;
            if (fila != null && fila.Cells.Count > 1) // Asegurarse de que la fila y la celda existen
            {
                int id = Convert.ToInt32(fila.Cells[1].Text); // Asegúrate de que esta celda contiene el ID correcto y es convertible a int

                ProductoNegocio negocio = new ProductoNegocio();
                Producto producto = negocio.ObtenerProducto(id);

                // Asignar los valores a los TextBoxes
                TextBoxId.Text = producto.id.ToString();
                TextBoxNombre.Text = producto.nombre;
                TextBoxDescripcion.Text = producto.descripcion;
                TextBoxImagen.Text = producto.Imagen;
                TextBoxPrecio.Text = producto.precio.ToString();
                TextBoxGanancia.Text = producto.margenGanancia.ToString();
                TextBoxStock.Text = producto.stock.ToString();
                ListItem itemMarca = DropDownListMarca.Items.FindByValue(producto.marca);
                ListItem itemTipo = DropDownListTipo.Items.FindByValue(producto.tipo);
                ListItem itemCategoria = DropDownListCategoria.Items.FindByValue(producto.categoria);
                CheckBoxEstado.Checked = producto.estado;

            }
            else
            {
                // Manejar el caso donde la fila o la celda no existen
                // Por ejemplo, mostrar un mensaje de error

                // Limpiar los TextBoxes
                TextBoxId.Text = "";
                TextBoxNombre.Text = "";
                TextBoxDescripcion.Text = "";
                TextBoxImagen.Text = "";
                TextBoxPrecio.Text = "";
                TextBoxGanancia.Text = "";
                TextBoxStock.Text = "";
                DropDownListMarca.SelectedIndex = 0;
                DropDownListTipo.SelectedIndex = 0;
                DropDownListCategoria.SelectedIndex = 0;
                DropDownListProveedor.SelectedIndex = 0;
                CheckBoxEstado.Checked = true;



            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Producto producto = new Producto
            {
                nombre = TextBoxNombre.Text,
                descripcion = TextBoxDescripcion.Text,
                precio = Convert.ToDecimal(TextBoxPrecio.Text),
                margenGanancia = Convert.ToDecimal(TextBoxGanancia.Text),
                Imagen = TextBoxImagen.Text,
                stock = Convert.ToInt32(TextBoxStock.Text),
                marca = DropDownListMarca.SelectedValue,
                tipo = DropDownListTipo.SelectedValue,
                categoria = DropDownListCategoria.SelectedValue,
                proveedor = DropDownListProveedor.SelectedValue,
                estado = CheckBoxEstado.Checked
            };

            ProductoNegocio negocio = new ProductoNegocio();
            negocio.AgregarProducto(producto);

            CargarGrilla();
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            TextBoxNombre.Text = string.Empty;
            TextBoxDescripcion.Text = string.Empty;
            TextBoxPrecio.Text = string.Empty;
            TextBoxGanancia.Text = string.Empty;
            TextBoxImagen.Text = string.Empty;
            TextBoxStock.Text = string.Empty;
            DropDownListMarca.SelectedIndex = 0;
            DropDownListTipo.SelectedIndex = 0;
            DropDownListCategoria.SelectedIndex = 0;
            DropDownListProveedor.SelectedIndex = 0;
            CheckBoxEstado.Checked = true;
        }




        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Producto producto = new Producto();

            producto.id = Convert.ToInt32(TextBoxId.Text);
            producto.nombre = TextBoxNombre.Text;
            producto.descripcion = TextBoxDescripcion.Text;
            producto.precio = Convert.ToDecimal(TextBoxPrecio.Text);
            producto.margenGanancia = Convert.ToDecimal(TextBoxGanancia.Text);
            producto.Imagen = TextBoxImagen.Text;
            producto.stock = Convert.ToInt32(TextBoxStock.Text);
            

            ProductoNegocio negocio = new ProductoNegocio();
            negocio.ModificarProducto(producto);

            CargarGrilla();
        }



        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            negocio.EliminarProducto(Convert.ToInt32(TextBoxId.Text));
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            TextBoxId.Text = "";
            TextBoxNombre.Text = "";
            TextBoxDescripcion.Text = "";
            TextBoxPrecio.Text = "";
            TextBoxGanancia.Text = "";
            TextBoxImagen.Text = "";
            TextBoxStock.Text = "";
          
        }

        protected void BtnVerDetalle_Click(object sender, EventArgs e)
        {
            // Obtener el id del producto seleccionado
            GridViewRow fila = GridViewProductos.SelectedRow;
            if (fila != null)
            {
                int id = Convert.ToInt32(fila.Cells[1].Text); // Asegúrate de que esta celda contiene el ID correcto y es convertible a int
                // Redirigir a la página de detalle, pasando el ID como parámetro
                Response.Redirect("DetalleProducto.aspx?id=" + id);
            }
            else
            {
                // Manejar el caso cuando no hay fila seleccionada
                // Por ejemplo, mostrar un mensaje de error al usuario
                Response.Write("Por favor, seleccione un producto.");
            }
        }




        protected void TextBoxGanancia_TextChanged(object sender, EventArgs e)
        {
            // Obtener el precio del producto
            decimal precio;
            if (decimal.TryParse(TextBoxPrecio.Text, out precio))
            {
                // Obtener el margen de ganancia ingresado por el usuario
                decimal margenGanancia;
                if (decimal.TryParse(TextBoxGanancia.Text, out margenGanancia))
                {
                    // Calcular el precio con el margen de ganancia aplicado
                    decimal precioConGanancia = precio + (precio * (margenGanancia / 100));
                    // Mostrar el precio con ganancia en el TextBoxPrecio
                    TextBoxPrecio.Text = precioConGanancia.ToString("F2");
                }
            }
        }



    }
}
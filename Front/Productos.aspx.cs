using dominio;
using negocio;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Front
{
    public partial class producto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CargarGrilla();
            }


        }


        private void CargarGrilla()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            GridViewProductos.DataSource = negocio.ListarProductos();
            GridViewProductos.DataBind();
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
                TextBoxImagen.Text = producto.imagen;
                TextBoxPrecio.Text = producto.precio.ToString();
                TextBoxStock.Text = producto.stock.ToString();
                TextBoxMarca.Text = producto.marca.ToString();
                TextBoxTipo.Text = producto.tipo.ToString();
                TextBoxCategoria.Text = producto.categoria.ToString();
                TextBoxProveedor.Text = producto.proveedor.ToString();
                TextBoxEstado.Text = producto.estado.ToString();
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
                TextBoxStock.Text = "";
                TextBoxMarca.Text = "";
                TextBoxTipo.Text = "";
                TextBoxCategoria.Text = "";
                TextBoxProveedor.Text = "";
                TextBoxEstado.Text = "";

            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            ProductoNegocio negocio = new ProductoNegocio();
            Producto producto = new Producto();

            producto.nombre = TextBoxNombre.Text;
            producto.descripcion = TextBoxDescripcion.Text;
            producto.imagen = TextBoxImagen.Text;
            producto.precio = Convert.ToDecimal(TextBoxPrecio.Text);
            producto.stock = Convert.ToInt32(TextBoxStock.Text);
            producto.marca = TextBoxMarca.Text;
            producto.tipo = TextBoxTipo.Text;
            producto.categoria = TextBoxCategoria.Text;
            producto.proveedor = TextBoxProveedor.Text;
            producto.estado = TextBoxEstado.Text;
          
            negocio.AgregarProducto(producto);


        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Producto producto = new Producto();

            producto.id = Convert.ToInt32(TextBoxId.Text);
            producto.nombre = TextBoxNombre.Text;
            producto.descripcion = TextBoxDescripcion.Text;
            producto.imagen = TextBoxImagen.Text;
            producto.precio = Convert.ToDecimal(TextBoxPrecio.Text);

            ProductoNegocio negocio = new ProductoNegocio();
            negocio.ModificarProducto(producto);
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
            TextBoxImagen.Text = "";
            TextBoxPrecio.Text = "";
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


        protected void GridViewProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                // Verificar si el índice está dentro del rango
                if (index >= 0 && index < GridViewProductos.Rows.Count)
                {
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
                        TextBoxImagen.Text = producto.imagen;
                        TextBoxPrecio.Text = producto.precio.ToString();
                        TextBoxStock.Text = producto.stock.ToString();
                        TextBoxMarca.Text = producto.marca.ToString();
                        TextBoxTipo.Text = producto.tipo.ToString();
                        TextBoxCategoria.Text = producto.categoria.ToString();
                        TextBoxProveedor.Text = producto.proveedor.ToString();
                        TextBoxEstado.Text = producto.estado.ToString();
                    }
                }
                else
                {
                    // Manejar el caso donde el índice está fuera del rango

                    // Limpiar los TextBoxes
                    TextBoxId.Text = "";
                    TextBoxNombre.Text = "";
                    TextBoxDescripcion.Text = "";
                    TextBoxImagen.Text = "";
                    TextBoxPrecio.Text = "";
                    TextBoxStock.Text = "";
                    TextBoxMarca.Text = "";
                    TextBoxTipo.Text = "";
                    TextBoxCategoria.Text = "";
                    TextBoxProveedor.Text = "";
                    TextBoxEstado.Text = "";
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
                    }
                }

            }
        }





    }
}
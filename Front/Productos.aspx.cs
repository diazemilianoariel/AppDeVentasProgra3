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
        private ProductoNegocio negocio = new ProductoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cliente"] == null || !IDPerfilValido())
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

        private bool IDPerfilValido()
        {
            Cliente cliente = (Cliente)Session["cliente"];
            return cliente.idPerfil == 2 || cliente.idPerfil == 4 || cliente.idPerfil == 3;
        }


        private void CargarDropDownLists()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            TipoNegocio tipoNegocio = new TipoNegocio();
            ProveedoresNegocio proveedorNegocio = new ProveedoresNegocio();


            ddlCategoriaProducto.DataSource = categoriaNegocio.ListarCategorias();
            ddlCategoriaProducto.DataTextField = "nombre";
            ddlCategoriaProducto.DataValueField = "id";
            ddlCategoriaProducto.DataBind();

            ddlMarcaProducto.DataSource = marcaNegocio.ListarMarcas();
            ddlMarcaProducto.DataTextField = "nombre";
            ddlMarcaProducto.DataValueField = "id";
            ddlMarcaProducto.DataBind();

            ddlTipoProducto.DataSource = tipoNegocio.ListarTipos();
            ddlTipoProducto.DataTextField = "nombre";
            ddlTipoProducto.DataValueField = "id";
            ddlTipoProducto.DataBind();

            ddlProveedoresProducto.DataSource = proveedorNegocio.ListarProveedores();
            ddlProveedoresProducto.DataTextField = "nombre";
            ddlProveedoresProducto.DataValueField = "id";
            ddlProveedoresProducto.DataBind();




        }

        private void CargarGrilla()
        {
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

                }
            }
            else if (e.CommandName == "VerDetalle")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                if (index >= 0 && index < GridViewProductos.Rows.Count)
                {
                    GridViewRow row = GridViewProductos.Rows[index];
                    if (row != null && row.Cells.Count > 0) // Asegurarse de que la fila y la celda existen
                    {
                        int id = Convert.ToInt32(row.Cells[0].Text);
                        Response.Redirect("DetalleProducto.aspx?id=" + id);
                    }
                    else
                    {
                        Response.Write("¡Ups! Parece que seleccionaste una fila inexistente.");
                    }
                }
                else
                {
                    Response.Write("¡Ups! Parece que seleccionaste una fila inexistente.");
                }
            }
        }

        protected void GridViewProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow fila = GridViewProductos.SelectedRow;
            if (fila != null && fila.Cells.Count > 1) // Asegurarse de que la fila y la celda existen
            {
                int id = Convert.ToInt32(fila.Cells[1].Text); // Asegúrate de que esta celda contiene el ID correcto y es convertible a int

                Producto producto = negocio.ObtenerProducto(id);


            }
            else
            {
                // Manejar el caso donde la fila o la celda no existen
                // Por ejemplo, mostrar un mensaje de error

                // Limpiar los TextBoxes
                LimpiarCampos();
            }
        }

        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            Producto producto = new Producto
            {
                nombre = txtNombreProducto.Text,
                descripcion = txtDescripcionProducto.Text,
                precio = decimal.Parse(txtPrecioProducto.Text),
                margenGanancia = decimal.Parse(txtmagenGanancia.Text),
                Imagen = txtImagenProducto.Text,
                stock = int.Parse(txtStockProducto.Text),
                IdCategoria = int.Parse(ddlCategoriaProducto.SelectedValue),
                idMarca = int.Parse(ddlMarcaProducto.SelectedValue),
                idTipo = int.Parse(ddlTipoProducto.SelectedValue),
                IdProveedor = int.Parse(ddlProveedoresProducto.SelectedValue),
                estado = chkEstadoProducto.Checked
            };

            negocio.AgregarProducto(producto);
            CargarGrilla();
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtNombreProducto.Text = string.Empty;
            txtDescripcionProducto.Text = string.Empty;
            txtPrecioProducto.Text = string.Empty;
            txtmagenGanancia.Text = string.Empty;
            txtImagenProducto.Text = string.Empty;
            txtStockProducto.Text = string.Empty;
            ddlCategoriaProducto.SelectedIndex = 0;
            ddlMarcaProducto.SelectedIndex = 0;
            ddlTipoProducto.SelectedIndex = 0;
            ddlProveedoresProducto.SelectedIndex = 0;

            chkEstadoProducto.Checked = false;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {


        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {



            CargarGrilla();
            LimpiarCampos();
        }

        //evento cerrar del modal 
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }


        protected void BtnVerDetalle_Click(object sender, EventArgs e)
        {
            GridViewRow fila = GridViewProductos.SelectedRow;
            if (fila != null)
            {
                int id = Convert.ToInt32(fila.Cells[1].Text); // Asegúrate de que esta celda contiene el ID correcto y es convertible a int
                Response.Redirect("DetalleProducto.aspx?id=" + id);
            }
            else
            {
                Response.Write("Por favor, seleccione un producto.");
            }
        }

        protected void btnCancelar_Click1(object sender, EventArgs e)
        {

        }
    }

}
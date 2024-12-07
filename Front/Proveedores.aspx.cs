
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
    public partial class Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                


                CargarGrilla();

            }
        }

        private void CargarGrilla()
        {
            ProveedoresNegocio negocio = new ProveedoresNegocio();
            GridViewProveedores.DataSource = negocio.ListarProveedores();
            GridViewProveedores.DataBind();


        }

        protected void GridViewProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow fila = GridViewProveedores.SelectedRow;
            if (fila != null && fila.Cells.Count > 1) // Asegurarse de que la fila y la celda existen
            {
                int id = Convert.ToInt32(fila.Cells[1].Text); // Asegúrate de que esta celda contiene el ID correcto y es convertible a int

                ProveedoresNegocio negocio = new ProveedoresNegocio();
                Proveedor proveedor = negocio.ObtenerProveedor(id);

                // Asignar los valores a los TextBoxes
                TextBoxNombre.Text = proveedor.Nombre;
                TextBoxDireccion.Text = proveedor.Direccion;
                TextBoxTelefono.Text = proveedor.Telefono;
                TextBoxEmail.Text = proveedor.Email;
            }
            else
            {
                // Manejar el caso donde la fila o la celda no existen
                // Por ejemplo, mostrar un mensaje de error

                // Limpiar los TextBoxes
                TextBoxId.Text = "";
                TextBoxNombre.Text = "";
                TextBoxDireccion.Text = "";
                TextBoxTelefono.Text = "";
                TextBoxEmail.Text = "";
            }
        }


        protected void GridViewProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                // Verificar si el índice está dentro del rango
                if (index >= 0 && index < GridViewProveedores.Rows.Count)
                {
                    GridViewRow row = GridViewProveedores.Rows[index];
                    if (row != null && row.Cells.Count > 0) // Asegurarse de que la fila y la celda existen
                    {
                        int idProveedor = Convert.ToInt32(row.Cells[0].Text); // Suponemos que el ID está en la columna 0

                        ProveedoresNegocio negocio = new ProveedoresNegocio();
                        Proveedor proveedor = negocio.ObtenerProveedor(idProveedor);

                        // Asignar los valores a los TextBoxes
                        TextBoxId.Text = proveedor.id.ToString();
                        TextBoxNombre.Text = proveedor.Nombre;
                        TextBoxDireccion.Text = proveedor.Direccion;
                        TextBoxTelefono.Text = proveedor.Telefono;
                        TextBoxEmail.Text = proveedor.Email;
                    }
                }
                else
                {
                    // Manejar el caso donde el índice está fuera del rango
                    // Limpiar los TextBoxes
                    LimpiarCampos();
                    Response.Write("¡Ups! Parece que seleccionaste una fila inexistente.");
                }
            }
            // ... (resto del código para otros comandos)
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    ProveedoresNegocio negocio = new ProveedoresNegocio();
                    Proveedor proveedor = new Proveedor
                    {
                        Nombre = TextBoxNombre.Text,
                        Direccion = TextBoxDireccion.Text,
                        Telefono = TextBoxTelefono.Text,
                        Email = TextBoxEmail.Text
                    };

                    negocio.AgregarProveedor(proveedor);
                    CargarGrilla();
                    LimpiarCampos();
                }
                else
                {
                    // Mostrar mensaje de error
                    Response.Write("Por favor, complete todos los campos.");
                }
            }
            catch (Exception ex)
            {
                // Manejar el error
                Response.Write("Ocurrió un error: " + ex.Message);
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Proveedor proveedor = new Proveedor
            {

                Nombre = TextBoxNombre.Text,
                Direccion = TextBoxDireccion.Text,
                Telefono = TextBoxTelefono.Text,
                Email = TextBoxEmail.Text
            };

            ProveedoresNegocio negocio = new ProveedoresNegocio();
            negocio.ModificarProveedor(proveedor);
            CargarGrilla();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ProveedoresNegocio negocio = new ProveedoresNegocio();
            negocio.EliminarProveedor(Convert.ToInt32(TextBoxId.Text));
            CargarGrilla();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            TextBoxId.Text = "";
            TextBoxNombre.Text = "";
            TextBoxDireccion.Text = "";
            TextBoxTelefono.Text = "";
            TextBoxEmail.Text = "";
        }

        protected void BtnVerDetalle_Click(object sender, EventArgs e)
        {
            // Obtener el id del proveedor seleccionado
            GridViewRow fila = GridViewProveedores.SelectedRow;
            if (fila != null)
            {
                int id = Convert.ToInt32(fila.Cells[1].Text); // Asegúrate de que esta celda contiene el ID correcto y es convertible a int
                // Redirigir a la página de detalle, pasando el ID como parámetro
                Response.Redirect("DetalleProveedor.aspx?id=" + id);
            }
            else
            {
                // Manejar el caso cuando no hay fila seleccionada
                // Por ejemplo, mostrar un mensaje de error al usuario
                Response.Write("Por favor, seleccione un proveedor.");
            }
        }


        private void LimpiarCampos()
        {
            TextBoxId.Text = "";
            TextBoxNombre.Text = "";
            TextBoxDireccion.Text = "";
            TextBoxTelefono.Text = "";
            TextBoxEmail.Text = "";
        }



        private bool ValidarCampos()
        {
            return !string.IsNullOrEmpty(TextBoxNombre.Text) &&
                   !string.IsNullOrEmpty(TextBoxDireccion.Text) &&
                   !string.IsNullOrEmpty(TextBoxTelefono.Text) &&
                   !string.IsNullOrEmpty(TextBoxEmail.Text);
        }




    }
}

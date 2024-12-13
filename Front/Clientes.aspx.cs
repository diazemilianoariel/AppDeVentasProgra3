using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using dominio;
using negocio;

namespace Front
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                CargarGrillaClientes();

            }

        }


        private void CargarGrillaClientes()
        {
            ClienteNegocio negocio = new ClienteNegocio();
            GridViewClientes.DataSource = negocio.ListarClientes();
            GridViewClientes.DataBind();
        }

        protected void GridViewClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = GridViewClientes.Rows[index];

                // Asignar los valores de la fila seleccionada a los TextBoxes
                TextBoxIdCliente.Text = HttpUtility.HtmlDecode(fila.Cells[0].Text);
                TextBoxNombreCliente.Text = HttpUtility.HtmlDecode(fila.Cells[1].Text);
                TextBoxApellidoCliente.Text = HttpUtility.HtmlDecode(fila.Cells[2].Text);
                TextBoxDniCliente.Text = HttpUtility.HtmlDecode(fila.Cells[3].Text);
                TextBoxDireccionCliente.Text = HttpUtility.HtmlDecode(fila.Cells[4].Text);
                TextBoxTelefonoCliente.Text = HttpUtility.HtmlDecode(fila.Cells[5].Text);
                TextBoxEmailCliente.Text = HttpUtility.HtmlDecode(fila.Cells[6].Text);
            }
        }



        protected void GridViewClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow fila = GridViewClientes.SelectedRow;
            if (fila != null && fila.Cells.Count > 1)
            {
                int id = Convert.ToInt32(fila.Cells[1].Text);

                ClienteNegocio negocio = new ClienteNegocio();
                Cliente cliente = negocio.ObtenerCliente(id);

                // Asignar los valores a los TextBoxes
                TextBoxIdCliente.Text = cliente.Id.ToString();
                TextBoxNombreCliente.Text = cliente.Nombre;
                TextBoxApellidoCliente.Text = cliente.Apellido;
                TextBoxDniCliente.Text = cliente.Dni;
                TextBoxDireccionCliente.Text = cliente.Direccion;
                TextBoxTelefonoCliente.Text = cliente.Telefono;
                TextBoxEmailCliente.Text = cliente.Email;

            }
        }


        protected void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    ClienteNegocio negocio = new ClienteNegocio();
                    Cliente cliente = new Cliente
                    {
                        Nombre = TextBoxNombreCliente.Text,
                        Apellido = TextBoxApellidoCliente.Text,
                        Dni = TextBoxDniCliente.Text,
                        Direccion = TextBoxDireccionCliente.Text,
                        Telefono = TextBoxTelefonoCliente.Text,
                        Email = TextBoxEmailCliente.Text
                    };

                    Cliente clienteInactivo = negocio.ExisteClienteInactivo(cliente.Dni);
                    if (clienteInactivo != null)
                    {
                        // Mostrar mensaje de confirmación
                        lblConfirmacion.Text = "El cliente ya existe pero está inactivo. ¿Desea reactivarlo?";
                        lblConfirmacion.Visible = true;
                        btnConfirmarReactivacion.CommandArgument = clienteInactivo.Id.ToString();
                        btnConfirmarReactivacion.Visible = true;
                    }
                    else
                    {
                        negocio.AgregarCliente(cliente);
                        Response.Write("Cliente agregado exitosamente.");
                        CargarGrillaClientes();
                        LimpiarCampos();
                    }
                }
                else
                {
                    // Mostrar mensaje de error
                    Response.Write("Por favor, complete todos los campos.");
                }
            }
            catch (Exception ex)
            {
                Response.Write("Ocurrió un error: " + ex.Message);
            }
        }

        protected void btnConfirmarReactivacion_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(((Button)sender).CommandArgument);
            ClienteNegocio negocio = new ClienteNegocio();
            negocio.ActivarCliente(id);
            Response.Write("Cliente reactivado exitosamente.");
            CargarGrillaClientes();
            LimpiarCampos();
            lblConfirmacion.Visible = false;
            btnConfirmarReactivacion.Visible = false;
        }





        protected void btnActivarCliente_Click(object sender, EventArgs e)
        {
            if (int.TryParse(Request["__EVENTARGUMENT"], out int id))
            {
                ClienteNegocio negocio = new ClienteNegocio();
                negocio.ActivarCliente(id);
                CargarGrillaClientes();
                LimpiarCampos();
            }
            else
            {
                Response.Write("ID de cliente no válido.");
            }
        }



        protected void btnModificarCliente_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente
            {
                Id = Convert.ToInt32(TextBoxIdCliente.Text),
                Nombre = TextBoxNombreCliente.Text,
                Apellido = TextBoxApellidoCliente.Text,
                Dni = TextBoxDniCliente.Text,
                Direccion = TextBoxDireccionCliente.Text,
                Telefono = TextBoxTelefonoCliente.Text,
                Email = TextBoxEmailCliente.Text,
            };

            ClienteNegocio negocio = new ClienteNegocio();
            negocio.ModificarCliente(cliente);
            CargarGrillaClientes();
            LimpiarCampos();

        }

        protected void btnEliminarCliente_Click(object sender, EventArgs e)
        {
            ClienteNegocio negocio = new ClienteNegocio();
            negocio.EliminarCliente(Convert.ToInt32(TextBoxIdCliente.Text));
            CargarGrillaClientes();
            LimpiarCampos();

        }

        protected void btnCancelarCliente_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }


        private void LimpiarCampos()
        {

            TextBoxIdCliente.Text = "";
            TextBoxNombreCliente.Text = "";
            TextBoxApellidoCliente.Text = "";
            TextBoxDniCliente.Text = "";
            TextBoxDireccionCliente.Text = "";
            TextBoxTelefonoCliente.Text = "";
            TextBoxEmailCliente.Text = "";




        }

        private bool ValidarCampos()
        {
            return 
                   !string.IsNullOrEmpty(TextBoxNombreCliente.Text) &&
                   !string.IsNullOrEmpty(TextBoxApellidoCliente.Text) &&
                   !string.IsNullOrEmpty(TextBoxDniCliente.Text) &&
                   !string.IsNullOrEmpty(TextBoxDireccionCliente.Text) &&
                   !string.IsNullOrEmpty(TextBoxTelefonoCliente.Text) &&
                   !string.IsNullOrEmpty(TextBoxEmailCliente.Text);
        }
    }
}
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front
{
    public partial class Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["cliente"] == null || !EsVendedorAdminSoporte((Cliente)Session["cliente"]))
            //{
            //    Response.Redirect("Login.aspx");
            //    return;
            //}

            if (!IsPostBack)
            {
                CargarVentas();

            }
        }
        private void CargarVentas()
        {
            VentaNegocio negocio = new VentaNegocio();
            gvVentas.DataSource = negocio.ListarVentas();
            gvVentas.DataBind();
        }

        //private bool EsVendedorAdminSoporte(Cliente cliente)
        //{
        //    return cliente.idPerfil == 2 || cliente.idPerfil == 3 || cliente.idPerfil == 4;
        //}

        protected void btnAprobar_Click(object sender, EventArgs e)
        {
            int idVenta = Convert.ToInt32(((Button)sender).CommandArgument);
            ActualizarEstadoVenta(idVenta, 2); // 2 = Completada
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            int idVenta = Convert.ToInt32(((Button)sender).CommandArgument);
            ActualizarEstadoVenta(idVenta, 3); // 3 = Cancelada
        }


        protected void btnNotificar_Click(object sender, EventArgs e)
        {
            int idVenta = Convert.ToInt32(((Button)sender).CommandArgument);
            // Implementar la lógica para notificar al comprador
            MostrarMensaje("Notificación enviada al comprador.", false);
        }


        private void ActualizarEstadoVenta(int idVenta, int nuevoEstado)
        {
            VentaNegocio negocio = new VentaNegocio();
            negocio.ActualizarEstadoVenta(idVenta, nuevoEstado);
            CargarVentas();
        }

        protected void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar los campos
                if (string.IsNullOrEmpty(txtFecha.Text) ||
                    string.IsNullOrEmpty(txtMonto.Text) ||
                    string.IsNullOrEmpty(txtIdUsuario.Text))
                {
                    MostrarMensaje("Todos los campos son obligatorios.");
                    return;
                }

                // Crear una nueva venta
                Venta venta = new Venta
                {
                    Fecha = Convert.ToDateTime(txtFecha.Text),
                    Monto = Convert.ToDecimal(txtMonto.Text),
                    Cliente = new Cliente { Id = Convert.ToInt32(txtIdUsuario.Text) },
                    EnLocal = chkEnLocal.Checked,
                    idEstadoVenta = Convert.ToInt32(ddlEstadoVenta.SelectedValue)
                };

                // Guardar la venta en la base de datos
                VentaNegocio negocio = new VentaNegocio();
                negocio.RegistrarVenta(venta);

                // Mostrar mensaje de éxito
                MostrarMensaje("Venta registrada exitosamente.", false);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error
                MostrarMensaje("Ocurrió un error: " + ex.Message);
            }
        }

        private void MostrarMensaje(string mensaje, bool esError = true)
        {
            // Implementar la lógica para mostrar mensajes
        }

        private void LimpiarCampos()
        {
            txtIdVenta.Text = string.Empty;
            txtFecha.Text = string.Empty;
            txtMonto.Text = string.Empty;
            txtIdUsuario.Text = string.Empty;
            chkEnLocal.Checked = false;
            ddlEstadoVenta.SelectedIndex = 0;
        }

    }
}

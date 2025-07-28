using negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using dominio;

namespace Front
{
    public partial class Ventas : System.Web.UI.Page
    {
        VentaNegocio negocio = new VentaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatosCompletos();
            }
        }

        private void CargarDatosCompletos()
        {
            try
            {
                // Cargar los KPIs
                litVentasPendientes.Text = negocio.ContarVentasPorEstado(1).ToString(); // 1 = Pendiente
                litMontoPendiente.Text = negocio.CalcularMontoPendiente().ToString("C");
                litVentasAprobadas.Text = negocio.ContarVentasPorEstado(2).ToString(); // 2 = Aprobado
                litIngresosTotales.Text = negocio.CalcularIngresosTotales().ToString("C");

                // Cargar las grillas
                gvVentasPendientes.DataSource = negocio.ListarVentasPendientes();
                gvVentasPendientes.DataBind();

                CargarHistorialConFiltros();
            }
            catch (Exception ex)
            {
                pnlMensaje.Visible = true;
                pnlMensaje.CssClass = "alert alert-danger";
                lblMensaje.Text = "Error al cargar datos: " + ex.Message;
            }
        }

        private void CargarHistorialConFiltros()
        {
            DateTime? fechaDesde = null;
            if (!string.IsNullOrEmpty(txtFechaDesde.Text))
                fechaDesde = DateTime.Parse(txtFechaDesde.Text);

            DateTime? fechaHasta = null;
            if (!string.IsNullOrEmpty(txtFechaHasta.Text))
                fechaHasta = DateTime.Parse(txtFechaHasta.Text);

            gvVentasRealizadas.DataSource = negocio.ListarVentasParaReporte("", fechaDesde, fechaHasta);
            gvVentasRealizadas.DataBind();
        }

       

        protected void gvVentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idVenta = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Aprobar")
                {
                    // 1. Buscamos la venta para obtener los datos del cliente.
                    Venta ventaParaNotificar = negocio.ObtenerVentaParaNotificacion(idVenta);

                    // 2. Aprobamos la venta en la base de datos.
                    negocio.AprobarVenta(idVenta);

                    // Si encontramos la venta, enviamos el correo de confirmación.
                    if (ventaParaNotificar != null)
                    {
                        EmailService emailService = new EmailService();
                        string asunto = "¡Tu compra ha sido aprobada!";
                        string cuerpo = $"Hola {ventaParaNotificar.Cliente.Nombre}, te informamos que tu orden N°{idVenta} ha sido aprobada y está siendo preparada para el envío. ¡Gracias por tu compra!";
                        emailService.EnviarCorreoConfirmacion(ventaParaNotificar.Cliente.Email, asunto, cuerpo);
                    }

                    // Mostramos mensaje de éxito.
                    pnlMensaje.Visible = true;
                    pnlMensaje.CssClass = "alert alert-success";
                    lblMensaje.Text = "Venta #" + idVenta + " aprobada y correo de notificación enviado.";
                }
                else if (e.CommandName == "Rechazar")
                {
                    // Buscamos la venta para notificar.
                    Venta ventaParaNotificar = negocio.ObtenerVentaParaNotificacion(idVenta);

                    //  Buscamos los productos para devolver el stock.
                    List<Producto> productosDeLaVenta = negocio.ListarProductosPorVenta(idVenta);

                   
                    negocio.RechazarVentaYDevolverStock(idVenta, productosDeLaVenta);

                    // Si todo salió bien, enviamos el correo.
                    if (ventaParaNotificar != null)
                    {
                        EmailService emailService = new EmailService();
                        string asunto = "Información sobre tu compra N°" + idVenta;
                        string cuerpo = $"Hola {ventaParaNotificar.Cliente.Nombre}, lamentamos informarte que tu orden N°{idVenta} ha sido cancelada. Si tenés alguna consulta, no dudes en contactarnos.";
                        emailService.EnviarCorreoConfirmacion(ventaParaNotificar.Cliente.Email, asunto, cuerpo);
                    }

                    // Mostramos mensaje de éxito.
                    pnlMensaje.Visible = true;
                    pnlMensaje.CssClass = "alert alert-warning";
                    lblMensaje.Text = "Venta #" + idVenta + " rechazada y correo de notificación enviado.";
                }
                else if (e.CommandName == "VerResumen")
                {
                    Response.Redirect("DetalleCompra.aspx?id=" + idVenta);
                   
                    return;
                }

                // recargamos toda la info para ver los cambios.
                CargarDatosCompletos();
            }
            catch (Exception ex)
            {
                pnlMensaje.Visible = true;
                pnlMensaje.CssClass = "alert alert-danger";
                lblMensaje.Text = "Ocurrió un error en la operación: " + ex.Message;
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            gvVentasRealizadas.PageIndex = 0;
            CargarHistorialConFiltros();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFechaDesde.Text = "";
            txtFechaHasta.Text = "";
            gvVentasRealizadas.PageIndex = 0;
            CargarHistorialConFiltros();
        }

        protected void gvVentasRealizadas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVentasRealizadas.PageIndex = e.NewPageIndex;
            CargarHistorialConFiltros();
        }
    }
}
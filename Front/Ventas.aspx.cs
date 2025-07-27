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
            int idVenta = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Aprobar")
            {
                negocio.AprobarVenta(idVenta);
            }
            else if (e.CommandName == "Rechazar")
            {
                List<Producto> productosDeLaVenta = negocio.ListarProductosPorVenta(idVenta);
                negocio.RechazarVentaYDevolverStock(idVenta, productosDeLaVenta);
            }
            else if (e.CommandName== "VerResumen")
            {
                Response.Redirect("DetalleCompra.aspx?id=" + idVenta);
            }




            CargarDatosCompletos(); // Recargar todo para ver los cambios
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            gvVentasRealizadas.PageIndex = 0; // Volver a la primera página al filtrar
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
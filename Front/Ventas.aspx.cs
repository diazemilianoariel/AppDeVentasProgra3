using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;

namespace Front
{
    public partial class Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

       
        private void CargarDatos()
        {
            VentaNegocio negocio = new VentaNegocio();
            try
            {
                // Cargar los KPIs (Indicadores Clave de Desempeño)
                litVentasPendientes.Text = negocio.ContarVentasPorEstado(1).ToString(); // 1 = Pendiente
                litMontoPendiente.Text = negocio.CalcularMontoPendiente().ToString("C"); // "C" formatea como moneda
                litVentasAprobadas.Text = negocio.ContarVentasPorEstado(2).ToString(); // 2 = Aprobado
                litIngresosTotales.Text = negocio.CalcularIngresosTotales().ToString("C");

              
                string filtroPendientes = txtBuscarPendientes.Text;
                gvVentasPendientes.DataSource = negocio.ListarVentasPendientes(filtroPendientes);
                gvVentasPendientes.DataBind();

               
                string filtroHistorial = txtBuscarHistorial.Text;
                gvVentasRealizadas.DataSource = negocio.ListarVentas(filtroHistorial);
                gvVentasRealizadas.DataBind();
            }
            catch (Exception ex)
            {


                pnlMensaje.Visible = true;
                pnlMensaje.CssClass = "alert alert-danger";
                //lblMensaje.Text = "Error al cargar los datos: " + ex.Message;

            }
        }

        // EVENTOS DE LOS BOTONES DE ACCIÓN
        protected void btnAprobar_Click(object sender, EventArgs e)
        {
            try
            {
                int idVenta = Convert.ToInt32(((Button)sender).CommandArgument);
                VentaNegocio negocio = new VentaNegocio();
                negocio.AprobarVenta(idVenta);
                CargarDatos(); // Recargar todo para ver los cambios

                pnlMensaje.Visible = true;
                pnlMensaje.CssClass = "alert alert-success";
                lblMensaje.Text = "Venta #" + idVenta + " aprobada.";

            }
            catch (Exception ex)
            {
                pnlMensaje.Visible = true;
                pnlMensaje.CssClass = "alert alert-danger";
                lblMensaje.Text = "Error al aprobar la venta: " + ex.Message;
            }
        }



      

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            try
            {
                int idVenta = Convert.ToInt32(((Button)sender).CommandArgument);
                VentaNegocio negocio = new VentaNegocio();

                // 1. Buscamos la lista de productos de esa venta para saber qué stock devolver.
                //    Usamos el método que ya sabemos que funciona.
                List<Producto> productosDeLaVenta = negocio.ListarProductosPorVenta(idVenta);

                // 2. Llamamos al método transaccional que hace TODO el trabajo de forma segura.
                //    (Este es el método que te pasé en el mensaje anterior para VentaNegocio.cs)
                negocio.RechazarVentaYDevolverStock(idVenta, productosDeLaVenta);

                // 3. Recargamos la grilla para que se vea el cambio de estado.
                CargarDatos();

                pnlMensaje.Visible = true;
                pnlMensaje.CssClass = "alert alert-warning";
                lblMensaje.Text = "Venta #" + idVenta + " rechazada y stock devuelto.";
            }
            catch (Exception ex)
            {
                pnlMensaje.Visible = true;
                pnlMensaje.CssClass = "alert alert-danger";
                lblMensaje.Text = "Error al rechazar la venta: " + ex.Message;
            }
        }


        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            string idVenta = ((Button)sender).CommandArgument;
            Response.Redirect("DetalleVenta.aspx?id=" + idVenta);
        }


      
        protected void txtBuscarPendientes_TextChanged(object sender, EventArgs e)
        {
            CargarDatos();
        }

        protected void txtBuscarHistorial_TextChanged(object sender, EventArgs e)
        {
            CargarDatos();
        }

       
        protected void gvVentasPendientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVentasPendientes.PageIndex = e.NewPageIndex;
            CargarDatos();
        }

        protected void gvVentasRealizadas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVentasRealizadas.PageIndex = e.NewPageIndex;
            CargarDatos();
        }
    }
}

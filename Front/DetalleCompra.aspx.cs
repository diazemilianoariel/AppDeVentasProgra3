using System;
using System.Collections.Generic;
using negocio;
using dominio;

namespace Front
{
    public partial class DetalleCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("Login.aspx", false);
                    return;
                }

                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out int idVenta))
                {
                    CargarDetalles(idVenta);
                }
                else
                {
                    // Si no hay ID, redirigir a una página principal
                    Response.Redirect("Default.aspx", false);
                }
            }   
        }


        private void CargarDetalles(int idVenta)
        {
            try
            {
                VentaNegocio negocio = new VentaNegocio();
                // Usamos el método que ya trae toda la información de la venta.
                Venta venta = negocio.ObtenerVentaPorId(idVenta);

                if (venta != null)
                {
                    // Cargamos los datos en los controles de la página
                    litIdCompra.Text = venta.IdVenta.ToString("D8");
                    litFechaCompra.Text = venta.Fecha.ToString("dd/MM/yyyy");
                    litTotalCompra.Text = venta.Monto.ToString("C");

                    // Cargamos la lista de productos en el GridView
                    gvDetalles.DataSource = venta.Productos;
                    gvDetalles.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session["error"] = ex;
                Response.Redirect("Error.aspx", false);
            }
        }




    }
}
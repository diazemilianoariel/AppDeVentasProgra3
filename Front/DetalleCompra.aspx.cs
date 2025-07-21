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
            try
            {
                if (!IsPostBack)
                {
                    if (Session["usuario"] == null)
                    {
                        Response.Redirect("Login.aspx", false);
                        return;
                    }

                    int idVenta = 0;
                    if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out idVenta))
                    {
                        FacturaNegocio negocio = new FacturaNegocio();

                        // 1. Obtenemos los datos generales de la compra
                        CompraResumen compra = negocio.ObtenerCompraPorId(idVenta);

                        // 2. Obtenemos la lista de productos
                        List<Producto> detalleVenta = negocio.ObtenerDetalleVenta(idVenta);

                        if (compra != null)
                        {
                            // Mostramos los datos en los nuevos literales
                            litIdCompra.Text = "#" + compra.IdVenta.ToString();
                            litFechaCompra.Text = compra.Fecha.ToString("dd/MM/yyyy");
                            litTotalCompra.Text = compra.TotalFactura.ToString("c");

                            // Mostramos los productos en la tabla
                            gvDetalles.DataSource = detalleVenta;
                            gvDetalles.DataBind();
                        }
                    }
                    else
                    {
                        Response.Redirect("MisCompras.aspx", false);
                    }
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
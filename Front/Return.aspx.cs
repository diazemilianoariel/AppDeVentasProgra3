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
    public partial class Return : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar el estado del pago
            string paymentId = Request.QueryString["payment_id"];
            string status = Request.QueryString["status"];

            if (status == "approved")
            {
                // Obtener el carrito y el cliente de la sesión
                List<Producto> carrito = (List<Producto>)Session["Carrito"];
                Cliente cliente = (Cliente)Session["cliente"];

                // Insertar la venta en la base de datos
                CarritoNegocio carritoNegocio = new CarritoNegocio();
                decimal totalGeneral = carrito.Sum(p => p.SubTotal);
                bool exito = carritoNegocio.InsertarVenta(carrito, totalGeneral);

                if (exito)
                {
                    // Limpiar el carrito
                    Session["Carrito"] = new List<Producto>();

                    // Mostrar mensaje de éxito
                    lblMensaje.Text = "Venta generada de manera exitosa.";
                    lblMensaje.Visible = true;

                    // Redirigir a la página de inicio después de unos segundos
                    ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", "setTimeout(function() { window.location.href = 'Default.aspx'; }, 3000);", true);
                }
                else
                {
                    lblMensaje.Text = "Error al confirmar la compra. Por favor, inténtelo de nuevo.";
                    lblMensaje.Visible = true;
                }
            }
            else
            {
                lblMensaje.Text = "El pago no fue aprobado.";
                lblMensaje.Visible = true;
            }

        }
    }
}
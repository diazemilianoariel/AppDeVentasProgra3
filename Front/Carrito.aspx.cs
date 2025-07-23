using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front
{
    public partial class CompraParcial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }

        private void CargarCarrito()
        {
            List<Producto> carrito = ObtenerCarrito();
            if (carrito.Count > 0)
            {
                pnlCarritoConItems.Visible = true;
                pnlCarritoVacio.Visible = false;
                rptCarrito.DataSource = carrito;
                rptCarrito.DataBind();
                ActualizarTotalGeneral();
            }
            else
            {
                pnlCarritoConItems.Visible = false;
                pnlCarritoVacio.Visible = true;
            }
        }

        private List<Producto> ObtenerCarrito()
        {
            return (List<Producto>)Session["Carrito"] ?? new List<Producto>();
        }

        protected void rptCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idProducto = Convert.ToInt32(e.CommandArgument);
            List<Producto> carrito = ObtenerCarrito();
            Producto productoEnCarrito = carrito.FirstOrDefault(p => p.id == idProducto);

            if (productoEnCarrito == null) return;

            if (e.CommandName == "Quitar")
            {
                carrito.Remove(productoEnCarrito);
            }
            else if (e.CommandName == "Aumentar")
            {
                // Podríamos añadir una validación contra el stock aquí si quisiéramos.
                productoEnCarrito.Cantidad++;
            }
            else if (e.CommandName == "Disminuir")
            {
                if (productoEnCarrito.Cantidad > 1)
                {
                    productoEnCarrito.Cantidad--;
                }
            }

            Session["Carrito"] = carrito;
            CargarCarrito();
        }

        private void ActualizarTotalGeneral()
        {
            List<Producto> carrito = ObtenerCarrito();
            // CORRECCIÓN: Se usa precioVenta para el cálculo, que ya incluye el margen.
            decimal totalGeneral = carrito.Sum(p => p.SubTotal);
            litSubtotal.Text = totalGeneral.ToString("F2");
            litTotalGeneral.Text = totalGeneral.ToString("F2");
        }

        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            try
            {
                List<Producto> carrito = ObtenerCarrito();
                if (carrito.Any())
                {
                    decimal totalGeneral = carrito.Sum(p => p.SubTotal);

                    CarritoNegocio carritoNegocio = new CarritoNegocio();
                    bool exito = carritoNegocio.InsertarVenta(carrito, totalGeneral, usuario.Id);

                    if (exito)
                    {
                        Session["Carrito"] = new List<Producto>();
                        EmailService emailService = new EmailService();
                        emailService.EnviarCorreoConfirmacion(usuario.Email, "Confirmación de Compra", "Tu compra está siendo procesada.");

                        // Redirigir a una página de éxito o a "Mis Compras"
                        Response.Redirect("MisCompras.aspx", false);
                    }
                    else
                    {
                        MostrarMensaje("Error al confirmar la compra. Por favor, inténtelo de nuevo.");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrió un error: " + ex.Message);
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Visible = true;
        }

        protected void btnVolverHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}

﻿using System;
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
            // Se usa precioVenta para el cálculo, que ya incluye el margen.
            decimal totalGeneral = carrito.Sum(p => p.SubTotal);
            litSubtotal.Text = totalGeneral.ToString("F2");
            litTotalGeneral.Text = totalGeneral.ToString("F2");
        }

        // EN: Carrito.aspx.cs (o el nombre de tu página de carrito)

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

                   

                    // 1. Llamamos al nuevo método transaccional 'ProcesarVenta'.
                    int idVentaGenerada = carritoNegocio.ProcesarVenta(carrito, totalGeneral, usuario.Id);

                    // 2. Verificamos que se haya generado un ID de venta válido.
                    if (idVentaGenerada > 0)
                    {
                        // Si la venta fue exitosa, vaciamos el carrito y enviamos el email.
                        Session["Carrito"] = new List<Producto>();
                        EmailService emailService = new EmailService(); // Asumiendo que tenés esta clase
                        emailService.EnviarCorreoConfirmacion(usuario.Email, "Confirmación de Compra", "Queremos Agradecerte por confiar en nuestros productos, porfavor ten paciencia ,nuestro equipo esta procesando tu compra, pronto recibiras un nuevo correo de aprobacion exitosa.");

                        // Redirigir a una página de éxito o a "Mis Compras"
                        Response.Redirect("MisCompras.aspx", false);
                    }
                   
                    else
                    {
                        MostrarMensaje("Error al confirmar la compra. No se pudo generar la venta.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Si la transacción falló, mostramos el error que viene de la capa de negocio.
                MostrarMensaje("Ocurrió un error al procesar la venta: " + ex.Message);
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

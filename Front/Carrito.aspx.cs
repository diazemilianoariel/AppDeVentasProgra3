using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front
{
    // CORRECCIÓN 1: El nombre de la clase ahora es "CompraParcial" para coincidir con el archivo .aspx
    public partial class CompraParcial : System.Web.UI.Page
    {
        public List<Producto> ListaArticulos = new List<Producto>();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Se lee "usuario" de la sesión.
            if (Session["usuario"] == null || !IDPerfilValido())
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }

        public bool IDPerfilValido()
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (usuario != null && usuario.Perfil != null)
            {
                // CORRECCIÓN 2: Se usa "soporte" con minúscula para coincidir con tu enum.
                return usuario.Perfil.Id == (int)TipoPerfil.Cliente ||
                       usuario.Perfil.Id == (int)TipoPerfil.Administrador ||
                       usuario.Perfil.Id == (int)TipoPerfil.Vendedor ||
                       usuario.Perfil.Id == (int)TipoPerfil.soporte;
            }
            return false;
        }

        private void CargarCarrito()
        {
            List<Producto> carrito = ObtenerCarrito();
            rptCarrito.DataSource = carrito;
            rptCarrito.DataBind();
            ActualizarTotalGeneral();
        }

        private List<Producto> ObtenerCarrito()
        {
            return (List<Producto>)Session["Carrito"] ?? new List<Producto>();
        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            int idProducto = Convert.ToInt32(((Button)sender).CommandArgument);
            List<Producto> carrito = ObtenerCarrito();
            Producto productoEnCarrito = carrito.Find(p => p.id == idProducto);
            if (productoEnCarrito != null)
            {
                carrito.Remove(productoEnCarrito);
                Session["Carrito"] = carrito;
                CargarCarrito();
            }
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCantidad = (TextBox)sender;
            RepeaterItem item = (RepeaterItem)txtCantidad.NamingContainer;
            int idProducto = Convert.ToInt32(((Button)item.FindControl("btnQuitar")).CommandArgument);
            List<Producto> carrito = ObtenerCarrito();
            Producto productoEnCarrito = carrito.Find(p => p.id == idProducto);

            if (productoEnCarrito != null)
            {
                try
                {
                    int cantidad = Convert.ToInt32(txtCantidad.Text);
                    if (cantidad <= 0)
                    {
                        MostrarMensaje("La cantidad debe ser mayor a cero.");
                        txtCantidad.Text = productoEnCarrito.Cantidad.ToString();
                        return;
                    }
                    productoEnCarrito.Cantidad = cantidad;
                    Session["Carrito"] = carrito;
                    CargarCarrito();
                }
                catch (Exception)
                {
                    MostrarMensaje("La cantidad debe ser un número entero.");
                }
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Visible = true;
        }

        private void ActualizarTotalGeneral()
        {
            List<Producto> carrito = ObtenerCarrito();
            decimal totalGeneral = 0;
            foreach (Producto producto in carrito)
            {
                // Asumo que tienes una propiedad SubTotal en Producto que calcula (Precio * Cantidad)
                totalGeneral += producto.precio * producto.Cantidad;
            }
            lblTotalGeneral.Text = totalGeneral.ToString("F2");
        }

        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            int idUsuario = usuario.Id;

            try
            {
                List<Producto> carrito = ObtenerCarrito();
                if (carrito.Count > 0)
                {
                    decimal totalGeneral = carrito.Sum(p => p.precio * p.Cantidad);

                    CarritoNegocio carritoNegocio = new CarritoNegocio();
                    bool exito = carritoNegocio.InsertarVenta(carrito, totalGeneral, idUsuario);

                    if (exito)
                    {
                        Session["Carrito"] = new List<Producto>();

                        EmailService emailService = new EmailService();
                        emailService.EnviarCorreoConfirmacion(usuario.Email, "Estado De tu Compra", "Tu Compra esta en Proceso ");

                        CargarCarrito();

                        lblMensaje.Text = "Venta generada de manera exitosa.";
                        lblMensaje.Visible = true;

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
                    lblMensaje.Text = "El carrito está vacío.";
                    lblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrió un error: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }

        protected void btnVolverHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using MercadoPago.Common;
using MercadoPago.DataStructures.Preference;
using negocio;
using MercadoPago.Resources;

namespace Front
{
    public partial class CompraParcial : System.Web.UI.Page
    {
        public List<Producto> ListaArticulos = new List<Producto>();


        protected void Page_Load(object sender, EventArgs e)
        { 



            if (!IsPostBack)
            {
                CargarCarrito();
            }

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
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                productoEnCarrito.Cantidad = cantidad; 
                Session["Carrito"] = carrito;
                CargarCarrito();
            }
        }

        private void ActualizarTotalGeneral()
        {
            List<Producto> carrito = ObtenerCarrito();
            decimal totalGeneral = 0;
            foreach (Producto producto in carrito)
            {
                totalGeneral += producto.SubTotal;
            }
            lblTotalGeneral.Text = totalGeneral.ToString("F2");
        }

        //private bool EsAdministradorClienteVendedor(Cliente cliente)
        //{
        //   return cliente.idPerfil == 1 || // Cliente
        //  cliente.idPerfil == 2 || // Administrador
        //  cliente.idPerfil == 3 || // Vendedor
        //  cliente.idPerfil == 4;   // Soporte
        //}

         protected void btnConfirmarCompra_Click(object sender, EventArgs e)
         {
            Cliente cliente = (Cliente)Session["cliente"];


            if (cliente == null || cliente.idPerfil != 1)
            {
                Response.Redirect("Login.aspx?mensaje=inicie+sesion+o+registrese+para+poder+operar.");
                return;
            }



            try
             {
                 List<Producto> carrito = ObtenerCarrito();

                 if (carrito.Count > 0)
                 {
                     decimal totalGeneral = carrito.Sum(p => p.SubTotal);


                    
                     CarritoNegocio carritoNegocio = new CarritoNegocio();
                     bool exito = carritoNegocio.InsertarVenta(carrito, totalGeneral);

                     if (exito)
                     {
                         // Limpiar el carrito
                         Session["Carrito"] = new List<Producto>();
                         CargarCarrito();

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


      /*  protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            if (Session["cliente"] == null || !EsAdministradorClienteVendedor((Cliente)Session["cliente"]))
            {
                Response.Redirect("Login.aspx?mensaje=inicie+sesion+o+registrese+para+poder+operar.");
                return;
            }

            try
            {
                List<Producto> carrito = ObtenerCarrito();

                if (carrito.Count > 0)
                {
                    decimal totalGeneral = carrito.Sum(p => p.SubTotal);
                    Cliente cliente = (Cliente)Session["cliente"];

                    //  credenciales de Mercado Pago tokennn
                    MercadoPago.SDK.AccessToken = ConfigurationManager.AppSettings["MercadoPagoAccessToken"];

                    // Crear la preferencia de pago
                    Preference preference = new Preference();

                    foreach (var producto in carrito)
                    {
                        preference.Items.Add(new Item()
                        {
                            Title = producto.nombre,
                            Quantity = producto.Cantidad,
                            CurrencyId = CurrencyId.ARS,
                            UnitPrice = producto.precio
                        });
                    }

                    preference.Payer = new Payer()
                    {
                        Email = cliente.Email
                    };

                    preference.BackUrls = new BackUrls()
                    {
                        Success = "https://tusitio.com/Return.aspx",
                        Failure = "https://tusitio.com/Cancel.aspx",
                        Pending = "https://tusitio.com/Pending.aspx"
                    };

                    preference.AutoReturn = AutoReturnType.approved;

                    preference.Save();

                    // Redirigir al usuario a la URL de aprobación de Mercado Pago
                    Response.Redirect(preference.InitPoint);
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
        }*/

        protected void btnVolverHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }




    }
}
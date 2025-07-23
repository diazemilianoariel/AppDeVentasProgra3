using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front
{
    public partial class Factura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = Session["usuario"] as Usuario;
                if (usuario == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                if (Request.QueryString["id"] == null)
                {
                    Response.Redirect("MisCompras.aspx");
                    return;
                }

                int idVenta = Convert.ToInt32(Request.QueryString["id"]);
                CargarFactura(idVenta, usuario);
            }
        }

        private void CargarFactura(int idVenta, Usuario usuarioLogueado)
        {
            try
            {
                VentaNegocio ventaNegocio = new VentaNegocio();
                Venta venta = ventaNegocio.ObtenerVentaPorId(idVenta);

                if (venta == null)
                {
                    lblError.Text = "La factura solicitada no fue encontrada.";
                    lblError.Visible = true;
                    return;
                }

                // VALIDACIÓN DE SEGURIDAD CLAVE:
                // Si el usuario no es admin Y el ID del cliente de la venta no coincide con el ID del usuario logueado...
                if (usuarioLogueado.Perfil.Id != (int)TipoPerfil.Administrador && venta.Cliente.Id != usuarioLogueado.Id)
                {
                    // ...lo redirigimos porque no tiene permiso para ver esta factura.
                    Response.Redirect("MisCompras.aspx");
                    return;
                }

                // Cargar datos del cliente de la venta
                litCliente.Text = venta.Cliente.Nombre + " " + venta.Cliente.Apellido;
                litDireccion.Text = venta.Cliente.Direccion;
                litTelefono.Text = venta.Cliente.Telefono;
                litEmail.Text = venta.Cliente.Email;

                // Cargar datos de la factura
                litNumeroFactura.Text = venta.IdVenta.ToString().PadLeft(8, '0');
                litFecha.Text = venta.Fecha.ToString("dd/MM/yyyy");

                // Cargar productos
                rptProductos.DataSource = venta.Productos;
                rptProductos.DataBind();

                // Cargar total
                litTotalFactura.Text = venta.Monto.ToString("C"); // "C" formatea como moneda
            }
            catch (Exception ex)
            {
                lblError.Text = "Ocurrió un error al cargar la factura. Por favor, intente más tarde.";
                lblError.Visible = true;
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("MisCompras.aspx");
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            // Llama a una función de JavaScript para abrir el diálogo de impresión del navegador.
            ClientScript.RegisterStartupScript(this.GetType(), "print", "window.print();", true);
        }
    }
}

using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Front
{
    public partial class MisCompras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Se obtiene el usuario de la nueva variable de sesión
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null || !EsPerfilValido(usuario))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                // Se le pasa el ID del usuario logueado para cargar sus compras
                CargarCompras(usuario.Id);
            }
        }

        private bool EsPerfilValido(Usuario usuario)
        {
            // Según el plan, Clientes y Administradores pueden ver esta página.
            return usuario.Perfil != null &&
                   (usuario.Perfil.Id == (int)TipoPerfil.Cliente ||
                    usuario.Perfil.Id == (int)TipoPerfil.Administrador);
        }

        private void CargarCompras(int idUsuario)
        {
            try
            {
                // Se utiliza el método de VentaNegocio para listar las ventas del usuario.
                VentaNegocio negocio = new VentaNegocio();
                List<Venta> listaCompras = negocio.ListarVentasPorUsuario(idUsuario);

                if (listaCompras.Any())
                {
                    gvMisCompras.DataSource = listaCompras;
                    gvMisCompras.DataBind();
                }
                else
                {
                    // Si no hay compras, mostramos el panel de mensaje.
                    pnlNoHayCompras.Visible = true;
                    gvMisCompras.Visible = false;
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                lblError.Text = "Ocurrió un error al cargar tus compras.";
                lblError.Visible = true;
            }
        }

        protected void gvMisCompras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerFactura")
            {
                int idVenta = Convert.ToInt32(e.CommandArgument);
                // Redirigimos pasando el ID de la venta por la URL. Es más seguro y robusto.
                Response.Redirect("Factura.aspx?id=" + idVenta);
            }
        }
    }
}

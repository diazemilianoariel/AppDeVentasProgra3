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
                CargarCompras();
            }
        }

        private bool EsPerfilValido(Usuario usuario)
        {
            // Según el plan, Clientes y Administradores pueden ver esta página.
            return usuario.Perfil != null &&
                   (usuario.Perfil.Id == (int)TipoPerfil.Cliente ||
                    usuario.Perfil.Id == (int)TipoPerfil.Administrador);
        }

        private void CargarCompras()
        {

            try
            {
                Usuario usuario = (Usuario)Session["usuario"];
                VentaNegocio negocio = new VentaNegocio();
                string filtro = txtFiltro.Text;

                List<Venta> listaCompras = negocio.ListarVentasPorUsuario(usuario.Id, filtro);

                if (listaCompras.Any())
                {
                    gvMisCompras.DataSource = listaCompras;
                    gvMisCompras.DataBind();
                }
                else
                {
                    pnlNoHayCompras.Visible = true;
                    gvMisCompras.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Ocurrió un error al cargar tus compras.";
                lblError.Visible = true;
            }
        }



        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            gvMisCompras.PageIndex = 0;
            CargarCompras();
        }

        protected void gvMisCompras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMisCompras.PageIndex = e.NewPageIndex;
            CargarCompras();
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

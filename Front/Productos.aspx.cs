using dominio;
using negocio;
using System;
using System.Web.UI.WebControls;

namespace Front
{
    public partial class Productos : System.Web.UI.Page
    {
        public bool IsAdmin { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = Session["usuario"] as Usuario;

            if (usuario == null || !EsPerfilValido(usuario))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            IsAdmin = EsAdmin(usuario);

            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private bool EsPerfilValido(Usuario usuario)
        {
            return usuario.Perfil != null &&
                   (usuario.Perfil.Id == (int)TipoPerfil.Administrador ||
                    usuario.Perfil.Id == (int)TipoPerfil.Vendedor);
        }

        private bool EsAdmin(Usuario usuario)
        {
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }

        private void CargarGrilla()
        {
            try
            {
                ProductoNegocio negocio = new ProductoNegocio();
                GridViewProductos.DataSource = negocio.ListarProductos();
                GridViewProductos.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al cargar los productos: " + ex.Message;
                lblError.Visible = true;
            }
        }

        protected void GridViewProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int productId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Modificar")
            {
                Response.Redirect("ProductosABM/ProductoModificar.aspx?id=" + productId);
            }
            else if (e.CommandName == "Detalle")
            {
                Response.Redirect("ProductosABM/DetalleProducto.aspx?id=" + productId);
            }
            else if (e.CommandName == "Eliminar")
            {
                if (IsAdmin)
                {
                    Response.Redirect("ProductosABM/ProductoEliminar.aspx?id=" + productId);
                }
            }
        }
    }
}

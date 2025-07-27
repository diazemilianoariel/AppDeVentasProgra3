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
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = Session["usuario"] as Usuario;

            if (usuario == null || !EsAdmin(usuario))
            {
                Response.Redirect("Login.aspx");
                return;
            }


            if (!IsPostBack)
            {


                CargarGrillaUsuarios();

            }

        }

        // Método de validación corregido y específico para esta página
        private bool EsAdmin(Usuario usuario)
        {
            // Según el plan, solo los Administradores pueden gestionar usuarios.
           // return usuario != null && usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
            return usuario?.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;

        }



        private void CargarGrillaUsuarios()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            string filtro = txtFiltro.Text;
            GridViewClientes.DataSource = negocio.ListarUsuarios(filtro);
            GridViewClientes.DataBind();
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // Al buscar, reiniciamos el índice de la página a 0 (la primera página)
            GridViewClientes.PageIndex = 0;
            CargarGrillaUsuarios();
        }

        protected void GridViewClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Maneja el cambio de página
            GridViewClientes.PageIndex = e.NewPageIndex;
            CargarGrillaUsuarios();
        }



        protected void GridViewClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
                int UsuarioId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Modificar")
            {
                Response.Redirect("UsuariosABM/UsuarioModificar.aspx?id=" + UsuarioId);





            }
            if (e.CommandName == "Eliminar")
            {
                Response.Redirect("UsuariosABM/UsuarioEliminar.aspx?id=" + UsuarioId);



            }
        }


        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            // Cuando el texto cambia, reiniciamos la paginación y recargamos la grilla.
            GridViewClientes.PageIndex = 0;
            CargarGrillaUsuarios();
        }
    }
}
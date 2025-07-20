using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Front.UsuariosABM
{
    public partial class UsuarioEliminar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuarioLogueado = Session["usuario"] as Usuario;
            if (usuarioLogueado == null || !EsAdmin(usuarioLogueado))
            {
                Response.Redirect("../Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                // PASO 2: Solo si el usuario tiene permisos, cargamos los datos.
                if (Request.QueryString["id"] != null)
                {
                    int usuarioId = Convert.ToInt32(Request.QueryString["id"]);

                    // IMPORTANTE: Un administrador no debería poder eliminarse a sí mismo.
                    if (usuarioLogueado.Id == usuarioId)
                    {
                        // Redirigimos o mostramos un error.
                        Response.Redirect("../Clientes.aspx");
                        return;
                    }

                    CargarDatosUsuario(usuarioId);
                }
                else
                {
                    Response.Redirect("../Clientes.aspx");
                }
            }
        }


        private bool EsAdmin(Usuario usuario)
        {
            // Según el plan, solo los Administradores pueden gestionar Usuarios.
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }



        private void CargarDatosUsuario(int usuarioId)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                // Asumo que tienes un método para obtener un usuario por ID.
                Usuario usuarioAEliminar = negocio.ObtenerUsuarioPorId(usuarioId);

                if (usuarioAEliminar != null)
                {
                    LabelNombreUsuario.Text = usuarioAEliminar.Nombre + " " + usuarioAEliminar.Apellido;
                    LabelEmailUsuario.Text = usuarioAEliminar.Email;
                    LabelPerfilUsuario.Text = usuarioAEliminar.Perfil.Nombre;
                    LabelEstadoUsuario.Text = usuarioAEliminar.estado ? "Activo" : "Inactivo";
                }
                else
                {
                    LabelError.Text = "Usuario no encontrado.";
                    LabelError.Visible = true;
                    btnConfirmar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LabelError.Text = "Error al cargar los datos del usuario.";
                LabelError.Visible = true;
                btnConfirmar.Visible = false;
            }
        }



        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                int usuarioId = Convert.ToInt32(Request.QueryString["id"]);
                UsuarioNegocio negocio = new UsuarioNegocio();
                // Asumo que tienes un método para la baja lógica de usuarios.
                negocio.BajaLogicaUsuario(usuarioId);
                Response.Redirect("../Clientes.aspx");
            }
            catch (Exception ex)
            {
                LabelError.Text = "Ocurrió un error al eliminar el usuario.";
                LabelError.Visible = true;
            }
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Clientes.aspx");
        }


    }
}
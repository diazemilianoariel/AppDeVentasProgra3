using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front.UsuariosABM
{
    public partial class UsuarioAgregar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuarioLogueado = Session["usuario"] as Usuario;
            if (usuarioLogueado == null || !EsAdmin(usuarioLogueado))
            {
                // CORRECCIÓN: La ruta de redirección debe subir un nivel.
                Response.Redirect("../Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                // Solo si el usuario tiene permisos, cargamos los perfiles.
                CargarPerfiles();
            }

        }

        private bool EsAdmin(Usuario usuario)
        {
            // Según el plan, solo los Administradores pueden gestionar Usuarios.
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }

        private void CargarPerfiles()
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                ddlPerfil.DataSource = negocio.ListarPerfiles();
                ddlPerfil.DataValueField = "id";
                ddlPerfil.DataTextField = "nombre";
                ddlPerfil.DataBind();
            }
            catch (Exception ex)
            {
                // Manejar error al cargar perfiles
            }
        }

       

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                Usuario nuevoUsuario = new Usuario();

                nuevoUsuario.Nombre = TextBoxNombre.Text;
                nuevoUsuario.Apellido = TextBoxApellido.Text;
                nuevoUsuario.Dni = TextBoxDni.Text;
                nuevoUsuario.Direccion = TextBoxDireccion.Text;
                nuevoUsuario.Telefono = TextBoxTelefono.Text;
                nuevoUsuario.Email = TextBoxEmail.Text;
                nuevoUsuario.clave = TextBoxClave.Text;

                // CORRECCIÓN: Se asigna el objeto Perfil completo.
                nuevoUsuario.Perfil = new Perfil();
                nuevoUsuario.Perfil.Id = int.Parse(ddlPerfil.SelectedValue);

                nuevoUsuario.estado = true; // Por defecto, los nuevos usuarios están activos.

                usuarioNegocio.AgregarUsuario(nuevoUsuario);

                Response.Redirect("../Clientes.aspx");
            }
            catch (Exception ex)
            {
                
                LabelError.Text = "Error al guardar el usuario: " + ex.Message;
            }

        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Clientes.aspx");
        }

    }
}
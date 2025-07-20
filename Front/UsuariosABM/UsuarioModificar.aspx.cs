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
    public partial class UsuarioModificar : System.Web.UI.Page
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
                    CargarPerfiles();
                    int usuarioId = Convert.ToInt32(Request.QueryString["id"]);
                    CargarDatosUsuario(usuarioId);

                    // MEJORA DE SEGURIDAD: Si el admin se está editando a sí mismo...
                    if (usuarioLogueado.Id == usuarioId)
                    {
                        // ...deshabilitamos la opción de cambiar de perfil y de desactivarse.
                        ddlPerfil.Enabled = false;
                        CheckBoxEstado.Enabled = false;
                    }
                }
                else
                {
                    Response.Redirect("../Clientes.aspx");
                }
            }

        }

        // Verifica si el id de perfil es valido
        private bool EsAdmin(Usuario usuario)
        {
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }

        private void CargarDatosUsuario(int usuarioId)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = negocio.ObtenerUsuarioPorId(usuarioId);
            if (usuario != null)
            {
                LabelId.Text = usuario.Id.ToString();
                TextBoxNombre.Text = usuario.Nombre;
                TextBoxApellido.Text = usuario.Apellido;
                TextBoxDni.Text = usuario.Dni;
                TextBoxDireccion.Text = usuario.Direccion;
                TextBoxTelefono.Text = usuario.Telefono;
                TextBoxEmail.Text = usuario.Email;
                // Por seguridad, no precargamos la clave. Se deja en blanco.
                // TextBoxClave.Text = usuario.clave; 
                ddlPerfil.SelectedValue = usuario.Perfil.Id.ToString();
                CheckBoxEstado.Checked = usuario.estado;
            }
        }

        private void CargarPerfiles()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            ddlPerfil.DataSource = negocio.ListarPerfiles();
            ddlPerfil.DataValueField = "id";
            ddlPerfil.DataTextField = "nombre";
            ddlPerfil.DataBind();
        }


        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                    return;

                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                Usuario usuarioModificado = new Usuario();

                usuarioModificado.Id = Convert.ToInt32(LabelId.Text);
                usuarioModificado.Nombre = TextBoxNombre.Text;
                usuarioModificado.Apellido = TextBoxApellido.Text;
                usuarioModificado.Dni = TextBoxDni.Text;
                usuarioModificado.Direccion = TextBoxDireccion.Text;
                usuarioModificado.Telefono = TextBoxTelefono.Text;
                usuarioModificado.Email = TextBoxEmail.Text;

                // Si el campo de la clave está vacío, no la actualizamos.
                // Si tiene texto, actualizamos la clave.
                if (!string.IsNullOrWhiteSpace(TextBoxClave.Text))
                {
                    usuarioModificado.clave = TextBoxClave.Text;
                }
                else
                {
                    // Obtenemos la clave original para no guardarla vacía.
                    Usuario original = usuarioNegocio.ObtenerUsuarioPorId(usuarioModificado.Id);
                    usuarioModificado.clave = original.clave;
                }

                usuarioModificado.Perfil = new Perfil();
                usuarioModificado.Perfil.Id = int.Parse(ddlPerfil.SelectedValue);
                usuarioModificado.estado = CheckBoxEstado.Checked;

                usuarioNegocio.ModificarUsuario(usuarioModificado);
                Response.Redirect("../Clientes.aspx");
            }
            catch (Exception ex)
            {
                // Manejar error
            }
        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Clientes.aspx");
        }
    }
}
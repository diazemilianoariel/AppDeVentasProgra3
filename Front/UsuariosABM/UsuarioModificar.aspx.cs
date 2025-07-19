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
            if (Session["cliente"] == null || !IDPerfilValido())
            {
                Response.Redirect("Login.aspx");
                return;
            }
            if (!IsPostBack)
            {
                int usuarioId = Convert.ToInt32(Request.QueryString["id"]);
                CargarDatosUsuario(usuarioId);


                CargarPerfiles();

            }

        }

        // Verifica si el id de perfil es valido
        private bool IDPerfilValido()
        {
            Usuario cliente = (Usuario)Session["cliente"];
            return cliente.idPerfil == 2 || cliente.idPerfil == 4 || cliente.idPerfil == 3;
        }

        private void CargarDatosUsuario(int usuarioId)
        {
            UsuarioNegocio clienteNegocio = new UsuarioNegocio();
            Usuario usuario = new Usuario();
            usuario = clienteNegocio.ObtenerCliente(usuarioId);
            if (usuario != null)
            {
                TextBoxNombre.Text = usuario.Nombre;
                TextBoxApellido.Text = usuario.Apellido;
                TextBoxDni.Text = usuario.Dni;
                TextBoxDireccion.Text = usuario.Direccion;
                TextBoxTelefono.Text = usuario.Telefono;
                TextBoxEmail.Text = usuario.Email;
                TextBoxClave.Text = usuario.clave;
                ddlPerfilCliente.SelectedValue = usuario.idPerfil.ToString();
                CheckBoxEstado.Checked = usuario.estado;



            }
        }

        private void CargarPerfiles()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            ddlPerfilCliente.DataSource = negocio.ListarPerfiles();
            ddlPerfilCliente.DataValueField = "id";
            ddlPerfilCliente.DataTextField = "nombre";
            ddlPerfilCliente.DataBind();
        }


        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio clienteNegocio = new UsuarioNegocio();
            Usuario usuario = new Usuario();
            usuario.Nombre = TextBoxNombre.Text;
            usuario.Apellido = TextBoxApellido.Text;
            usuario.Dni = TextBoxDni.Text;
            usuario.Direccion = TextBoxDireccion.Text;
            usuario.Telefono = TextBoxTelefono.Text;
            usuario.Email = TextBoxEmail.Text;
            usuario.idPerfil = int.Parse(ddlPerfilCliente.SelectedValue);
            usuario.estado = CheckBoxEstado.Checked;
          
            clienteNegocio.ModificarCliente(usuario);
        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Clientes.aspx");
        }
    }
}
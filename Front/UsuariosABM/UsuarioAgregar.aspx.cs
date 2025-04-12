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
            if (Session["cliente"] == null || !EsAdministradorOSoporte((Cliente)Session["cliente"]))
            {
                Response.Redirect("Login.aspx");
                return;
            }
            if (!IsPostBack)
            {
                CargarPerfiles();
               
            }

        }

        private bool EsAdministradorOSoporte(Cliente cliente)
        {
            return cliente.idPerfil == 2 || cliente.idPerfil == 3 || cliente.idPerfil == 4;
        }

        private void CargarPerfiles()
        {
            ClienteNegocio negocio = new ClienteNegocio();
            ddlPerfilCliente.DataSource = negocio.ListarPerfiles();
            ddlPerfilCliente.DataValueField = "id";
            ddlPerfilCliente.DataTextField = "nombre";
            ddlPerfilCliente.DataBind();
        }

       

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
           ClienteNegocio clienteNegocio = new ClienteNegocio();
           Cliente cliente = new Cliente();

            cliente.Nombre = TextBoxNombreCliente.Text;
            cliente.Apellido = TextBoxApellidoCliente.Text;
            cliente.Dni = TextBoxDniCliente.Text;
            cliente.Direccion = TextBoxDireccionCliente.Text;
            cliente.Telefono = TextBoxTelefonoCliente.Text;
            cliente.Email = TextBoxEmailCliente.Text;
            cliente.clave = TextBoxClaveCliente.Text;
            cliente.idPerfil = int.Parse(ddlPerfilCliente.SelectedValue);
            cliente.estado = true;
            clienteNegocio.AgregarCliente(cliente);

            // Redirige a la página de lista de Usuarios
            Response.Redirect("../Clientes.aspx");



        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Clientes.aspx");
        }

    }
}
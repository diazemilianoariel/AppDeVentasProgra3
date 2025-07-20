using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front.ProveedoresABM
{
    public partial class ProveedorModificar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null || !EsAdmin(usuario))
            {

                Response.Redirect("../Login.aspx");
                return;
            }

            if (!IsPostBack)
            {

                if (Request.QueryString["id"] != null)
                {
                    int proveedorId = Convert.ToInt32(Request.QueryString["id"]);
                    CargarDatosProveedor(proveedorId);
                }
                else
                {
                    Response.Redirect("../Proveedores.aspx");
                }
            }
        }


        private bool EsAdmin(Usuario usuario)
        {

            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }

        private void CargarDatosProveedor(int proveedorId)
        {
            ProveedoresNegocio proveedorNegocio = new ProveedoresNegocio();
            Proveedor proveedor = proveedorNegocio.ObtenerProveedor(proveedorId);

            if (proveedor != null)
            {

                LabelId.Text = proveedor.Id.ToString();
                TextBoxNombre.Text = proveedor.Nombre;
                TextBoxDireccion.Text = proveedor.Direccion;
                TextBoxTelefono.Text = proveedor.Telefono;
                TextBoxEmail.Text = proveedor.Email;
                CheckBoxEstado.Checked = proveedor.estado;
            }
            else
            {

                Response.Redirect("../Proveedores.aspx");
            }
        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(TextBoxNombre.Text) || string.IsNullOrWhiteSpace(TextBoxEmail.Text))
                {
                    // La validación del aspx se encargará de esto, pero es una buena práctica tenerlo aquí también.
                    return;
                }

                var proveedor = new Proveedor
                {
                    Id = Convert.ToInt32(LabelId.Text),
                    Nombre = TextBoxNombre.Text,
                    Direccion = TextBoxDireccion.Text,
                    Telefono = TextBoxTelefono.Text,
                    Email = TextBoxEmail.Text,
                    estado = CheckBoxEstado.Checked
                };

                ProveedoresNegocio negocio = new ProveedoresNegocio();
                negocio.ModificarProveedor(proveedor);
                Response.Redirect("../Proveedores.aspx");
            }
            catch (Exception ex)
            {
                // Manejo de errores. Podrías usar un Label para mostrar el mensaje.
            }
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Proveedores.aspx");
        }
    }
}

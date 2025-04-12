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

            if(!IsPostBack)
            {
                int proveedorId = Convert.ToInt32(Request.QueryString["id"]);
                CargarDatosProveedor(proveedorId);
            }



            if (Session["cliente"] == null || !IDPerfilValido())
            {
                Response.Redirect("Login.aspx");
                return;
            }
           

        }

        // Verifica si el id de perfil es valido
        private bool IDPerfilValido()
        {
            Cliente cliente = (Cliente)Session["cliente"];
            return cliente.idPerfil == 2 || cliente.idPerfil == 4 || cliente.idPerfil == 3;
        }

        private void CargarDatosProveedor(int proveedorId)
        {

            // var es una variable dinamica
            //var proveedor = new ProveedoresNegocio().ObtenerProveedor(proveedorId);

            ProveedoresNegocio proveedorNegocio = new ProveedoresNegocio();
            Proveedor proveedor = new Proveedor();

            proveedor = proveedorNegocio.ObtenerProveedor(proveedorId);

            if (proveedor != null)
            {
                TextBoxNombre.Text = proveedor.Nombre;
                TextBoxDireccion.Text = proveedor.Direccion;
                TextBoxTelefono.Text = proveedor.Telefono;
                TextBoxEmail.Text = proveedor.Email;
                CheckBoxEstado.Checked = proveedor.estado;
            }


        }


        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            int proveedorId = Convert.ToInt32(Request.QueryString["id"]);
            var proveedor = new Proveedor
            {
                id = proveedorId,
                Nombre = TextBoxNombre.Text,
                Direccion = TextBoxDireccion.Text,
                Telefono = TextBoxTelefono.Text,
                Email = TextBoxEmail.Text,
                estado = CheckBoxEstado.Checked
            };
            // Implementa la lógica para actualizar el producto en la base de datos
            ProveedoresNegocio negocio = new ProveedoresNegocio();
            negocio.ModificarProveedor(proveedor);
            // Redirige a la página de lista de productos después de guardar
            Response.Redirect("../Proveedores.aspx");
        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            // Redirige a la página de lista de productos sin guardar cambios
            Response.Redirect("../Proveedores.aspx");
        }


    }
}
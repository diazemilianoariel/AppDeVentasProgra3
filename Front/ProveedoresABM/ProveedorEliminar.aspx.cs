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
    public partial class ProveedorEliminar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
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
            Usuario cliente = (Usuario)Session["cliente"];
            return cliente.idPerfil == 2 || cliente.idPerfil == 4 || cliente.idPerfil == 3;
        }

        private void CargarDatosProveedor(int proveedorId)
        {
            // Implementa la lógica para obtener los datos del proveedor de la base de datos
            ProveedoresNegocio proveedorNegocio = new ProveedoresNegocio();
            var proveedor = proveedorNegocio.ObtenerProveedor(proveedorId);
            if (proveedor != null)
            {
                LabelNombreProveedor.Text = proveedor.Nombre;
                LabelDireccionProveedor.Text = proveedor.Direccion;
                LabelTelefonoProveedor.Text = proveedor.Telefono;
                LabelEmailProveedor.Text = proveedor.Email;
                LabelEstadoProveedor.Text = proveedor.estado.ToString();
            }
            else
            {
                // Manejar el caso en que no se encuentra el producto
                LabelNombreProveedor.Text = "Proveedor no encontrado";
                LabelDireccionProveedor.Text = "";
                LabelTelefonoProveedor.Text = "";
                LabelEmailProveedor.Text = "";
                LabelEstadoProveedor.Text = "";


            }

        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            // Lógica para eliminar el proveedor
            int proveedorId = Convert.ToInt32(Request.QueryString["id"]);
            ProveedoresNegocio proveedorNegocio = new ProveedoresNegocio();
            proveedorNegocio.EliminarProveedor(proveedorId);
            Response.Redirect("../Proveedores.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            // Lógica para cancelar la eliminación y redirigir a la página de proveedores
            Response.Redirect("../Proveedores.aspx");
        }
    }
}
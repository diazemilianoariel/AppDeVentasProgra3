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
            var proveedor = proveedorNegocio.ObtenerProveedor(proveedorId);
            if (proveedor != null)
            {
                LabelNombreProveedor.Text = proveedor.Nombre;
                LabelDireccionProveedor.Text = proveedor.Direccion;
                LabelTelefonoProveedor.Text = proveedor.Telefono;
                LabelEmailProveedor.Text = proveedor.Email;
                //  Se muestra un texto más amigable para el estado.
                LabelEstadoProveedor.Text = proveedor.estado ? "Activo" : "Inactivo";
            }
            else
            {
                LabelError.Text = "Proveedor no encontrado.";
                LabelError.Visible = true;
                btnConfirmar.Visible = false;
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                int proveedorId = Convert.ToInt32(Request.QueryString["id"]);
                ProveedoresNegocio proveedorNegocio = new ProveedoresNegocio();
                proveedorNegocio.EliminarProveedor(proveedorId); // Asumo que es baja lógica
                Response.Redirect("../Proveedores.aspx");
            }
            catch (Exception ex)
            {
                LabelError.Text = "Ocurrió un error al eliminar el proveedor.";
                LabelError.Visible = true;

            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Proveedores.aspx");
        }
    }
}

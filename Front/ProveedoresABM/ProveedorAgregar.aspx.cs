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
    public partial class ProveedorAgregar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            // hacer la validacion 


            ProveedoresNegocio proveedorNegocio= new ProveedoresNegocio();
            var proveedor = new Proveedor
            {
                Nombre = TextBoxNombre.Text,
                Direccion = TextBoxDireccion.Text,
                Telefono = TextBoxTelefono.Text,
                Email = TextBoxEmail.Text,
                estado = CheckBoxEstado.Checked
            };

            // Implementa la lógica para agregar el producto en la base de datos
             proveedorNegocio.AgregarProveedor(proveedor);


            // Redirige a la página de lista de productos
            Response.Redirect("../Proveedores.aspx");


        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Proveedores.aspx");
        }






    }
}
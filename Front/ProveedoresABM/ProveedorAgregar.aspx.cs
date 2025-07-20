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




            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null || !EsAdmin(usuario))
            {
                
                Response.Redirect("../Login.aspx");
                return;
            }


        }

        
        private bool EsAdmin(Usuario usuario)
        {
            // Administradores pueden gestionar Proveedores.
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Se añade validación de campos obligatorios.
                if (string.IsNullOrWhiteSpace(TextBoxNombre.Text) || string.IsNullOrWhiteSpace(TextBoxEmail.Text))
                {
                    // Aquí podrías usar un Label de error para ser más específico.
                    // Por ejemplo: LabelError.Text = "El nombre y el email son obligatorios.";

                    return;
                }

                ProveedoresNegocio proveedorNegocio = new ProveedoresNegocio();
                var proveedor = new Proveedor
                {
                    Nombre = TextBoxNombre.Text,
                    Direccion = TextBoxDireccion.Text,
                    Telefono = TextBoxTelefono.Text,
                    Email = TextBoxEmail.Text,
                    estado = CheckBoxEstado.Checked
                };

                proveedorNegocio.AgregarProveedor(proveedor);
                Response.Redirect("../Proveedores.aspx");
            }
            catch (Exception ex)
            {
                // Manejo de errores
                // Podrías mostrar el error en un Label: LabelError.Text = "Ocurrió un error...";
                Console.WriteLine("Error al agregar proveedor: " + ex.Message);
            }


        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Proveedores.aspx");
        }






    }
}
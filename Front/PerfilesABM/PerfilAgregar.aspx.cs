using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front.PerfilesABM
{
    public partial class PerfilesAgregar : System.Web.UI.Page
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
                // CargarPerfiles();
            }

        }

        private bool IDPerfilValido()
        {
            Usuario cliente = (Usuario)Session["cliente"];
            return cliente.idPerfil == 2 || cliente.idPerfil == 4;
        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            PerfilesNegocio perfilesNegocio = new PerfilesNegocio();
            Perfil perfil = new Perfil();

            perfil.Nombre = TxtNombre.Text;
            perfil.Estado = CheckBoxEstado.Checked;

           

            // Implementa la lógica para agregar el producto en la base de datos
            perfilesNegocio.AgregarPerfil(perfil);

            // Redirige a la página de lista de productos
            Response.Redirect("../Perfiles.aspx");

        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Perfiles.aspx");
        }


        protected void btnConfirmarReactivacion_Click(object sender, EventArgs e)
        {
            // Aquí puedes agregar la lógica para reactivar el perfil
            // Por ejemplo, llamar a un método en la clase de negocio para reactivar el perfil
            // MostrarMensaje("Perfil reactivado correctamente.", false);
        }


    }
}
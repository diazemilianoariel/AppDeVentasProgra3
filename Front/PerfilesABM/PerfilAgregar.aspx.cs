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



            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null || !EsAdmin(usuario))
            {
                Response.Redirect("Login.aspx");
                return;
            }




            if (!IsPostBack)
            {
                
            }

        }

        private bool EsAdmin(Usuario usuario)
        {
            // Según el plan, solo los Administradores pueden gestionar Perfiles.
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
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
            


            PerfilesNegocio perfilesNegocio = new PerfilesNegocio();
            Perfil perfil = new Perfil();
            perfil.Nombre = TxtNombre.Text;
            perfil.Estado = true; 

            perfilesNegocio.AgregarPerfil(perfil);
            
            Response.Redirect("../Perfiles.aspx");

        }


    }
}
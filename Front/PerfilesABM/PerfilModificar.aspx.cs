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
    public partial class PerfilesModificar : System.Web.UI.Page
    {
        PerfilesNegocio perfilesNegocio = new PerfilesNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
           
            Usuario usuario = Session["usuario"] as Usuario;
            if(Session ["usuario"] == null && !EsAdmin(usuario))
            {
                Response.Redirect("../Login.aspx");
            }



            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int perfilId = Convert.ToInt32(Request.QueryString["id"]);
                    CargarDatosPerfil(perfilId);
                }
                else
                {
                    // Manejar el caso donde no se proporciona un ID.
                    LabelError.Text = "No se especificó ningún perfil para modificar.";
                    LabelError.Visible = true;
                    ButtonGuardar.Visible = false;
                }

            }

        }


        private bool EsAdmin(Usuario usuario)
        {
            if (usuario != null && usuario.Perfil != null)
            {
                return usuario.Perfil.Id == (int)TipoPerfil.Administrador;
            }
            return false;
        }



        private void CargarDatosPerfil(int perfilId)
        {
          
            PerfilesNegocio perfilesNegocio = new PerfilesNegocio();
            Perfil perfil = new Perfil();


            perfil = perfilesNegocio.ObtenerPerfil(perfilId);



            if (perfil != null)
            {
                TextBoxNombre.Text = perfil.Nombre;
                
                CheckBoxEstado.Checked = perfil.Estado;
            }
        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            

            int perfilId = Convert.ToInt32(Request.QueryString["id"]);
            var perfil = new Perfil
            {
                Id = perfilId,
                Nombre = TextBoxNombre.Text,
                Estado = CheckBoxEstado.Checked
            };
            // Implementa la lógica para actualizar el perfil en la base de datos
            perfilesNegocio.ModificarPerfil(perfil);
            // Redirige a la página de lista de productos después de guardar
            Response.Redirect("../Perfiles.aspx");
        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            // Redirige a la página de lista de productos sin guardar cambios
            Response.Redirect("../Perfiles.aspx");
        }


    }
}
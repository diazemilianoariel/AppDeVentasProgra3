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
    public partial class PerfilEliminar : System.Web.UI.Page
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

                if(Request.QueryString["id"] != null)
                {
                   
                    CargarPerfil();
                }
                else
                {
                    // Si no hay ID, no hay nada que eliminar.
                    Response.Redirect("../Perfiles.aspx");
                }

            }


       


        }


        private bool EsAdmin(Usuario usuario)
        {
            // Según el plan, solo los Administradores pueden gestionar Marcas.
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }

        private void CargarPerfil()
        {
            try
            {
                int perfilId = Convert.ToInt32(Request.QueryString["id"]);
                PerfilesNegocio perfilesNegocio = new PerfilesNegocio();
                Perfil perfil = perfilesNegocio.ObtenerPerfil(perfilId);

                if (perfil != null)
                {
                    LabelNombrePerfil.Text = perfil.Nombre;
                    // MEJORA: Se muestra un texto más amigable para el estado.
                    LabelEstadoPerfil.Text = perfil.Estado ? "Activo" : "Inactivo";
                }
                else
                {
                    LabelError.Text = "Perfil no encontrado.";
                    LabelError.Visible = true;
                    btnConfirmar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LabelError.Text = "Error al cargar el perfil: " + ex.Message;
                LabelError.Visible = true;
                btnConfirmar.Visible = false;
            }
        }

      


        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                int perfilId = Convert.ToInt32(Request.QueryString["id"]);
                PerfilesNegocio perfilesNegocio = new PerfilesNegocio();

                //  objeto Perfil solo con el ID, que es lo que necesita el método de baja.
                Perfil perfil = new Perfil { Id = perfilId };

                perfilesNegocio.BajaLogicaPerfiles(perfil);
                Response.Redirect("../Perfiles.aspx");
            }
            catch (Exception ex)
            {
                LabelError.Text = "Error al eliminar el perfil: " + ex.Message;
                LabelError.Visible = true;
            }
        }



        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Perfiles.aspx");
        }

    }
}
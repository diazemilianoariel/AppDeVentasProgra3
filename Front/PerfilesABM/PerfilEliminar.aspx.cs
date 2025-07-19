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
            if (!IsPostBack)
            {
                CargarPerfil();
            }


            if (Session["cliente"] == null || !IDPerfilValido())
            {
                Response.Redirect("../Login.aspx");
                return;
            }


        }

        private void CargarPerfil()
        {
            int perfilId = Convert.ToInt32(Request.QueryString["id"]);
            PerfilesNegocio perfilesNegocio = new PerfilesNegocio();
            Perfil perfil = perfilesNegocio.ObtenerPerfil(perfilId);
            if (perfil != null)
            {
                LabelNombrePerfil.Text = perfil.Nombre;
                LabelEstadoPerfil.Text = perfil.Estado.ToString();
               
            }
            else
            {
                LabelError.Text = "Perfil no encontrado.";


            }
        }

        private bool IDPerfilValido()
        {
            Usuario cliente = (Usuario)Session["cliente"];
            return cliente.idPerfil == 2 || cliente.idPerfil == 4;
        }


        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            int perfilId = Convert.ToInt32(Request.QueryString["id"]);
            PerfilesNegocio perfilesNegocio = new PerfilesNegocio();
            Perfil perfil = new Perfil();

            perfil.Id = perfilId;


            try
            {
                perfilesNegocio.BajaLogicaPerfiles(perfil);
                Response.Redirect("../Perfiles.aspx");
            }
            catch (Exception ex)
            {
                LabelError.Text = "Error al eliminar el perfil: " + ex.Message;
            }
        }



        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Perfiles.aspx");
        }

    }
}
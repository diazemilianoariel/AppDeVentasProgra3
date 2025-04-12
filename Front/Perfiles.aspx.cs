using dominio;

using negocio;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;




namespace Front
{
    public partial class Perfiles : System.Web.UI.Page
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
                CargarPerfiles();
            }

        }

        private void CargarPerfiles()
        {
            PerfilesNegocio perfilesNegocio = new PerfilesNegocio();
            GridViewPerfiles.DataSource = perfilesNegocio.ListarPerfiles();
            GridViewPerfiles.DataBind();
        }

        private bool IDPerfilValido()
        {
            Cliente cliente = (Cliente)Session["cliente"];
            return cliente.idPerfil == 2 || cliente.idPerfil == 4;
        }

        protected void MostrarMensaje(string mensaje, bool esError = true)
        {
            //LabelMensaje.Text = mensaje;
            //LabelMensaje.ForeColor = esError ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            //LabelMensaje.Visible = true;
        }

        protected void GridViewPerfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modificar")
            {
                int perfilId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("PerfilesABM/PerfilModificar.aspx?id=" + perfilId);



            }
            else
            {
                if (e.CommandName == "Eliminar")
                {

                int perfilId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("PerfilesABM/PerfilEliminar.aspx?id=" + perfilId);
                }

            }
        }


    }
}
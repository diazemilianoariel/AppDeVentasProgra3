using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace Front
{
    public partial class Tipo : System.Web.UI.Page
    {
        protected TextBox TextBoxIdTipo;
        protected TextBox TextBoxNuevoTipo;
        protected Panel editSection;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["cliente"] == null || !EsAdministradorOSoporte((Cliente)Session["cliente"]))
            {
                Response.Redirect("Login.aspx");
                return;
            }


            if (!IsPostBack)
            {
                CargarTipos();
            }
        }

        private bool EsAdministradorOSoporte(Cliente cliente)
        {
            return cliente.nombrePerfil == "Administrador" || cliente.nombrePerfil == "Soporte" || cliente.nombrePerfil == "Vendedor";
        }

        private void CargarTipos()
        {
            TipoNegocio negocio = new TipoNegocio();
            GridViewTipos.DataSource = negocio.ListarTipos();
            GridViewTipos.DataBind();
        }

       

        protected void GridViewTipos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modificar")
            {

                int tipoId= Convert.ToInt32(e.CommandArgument);
                Response.Redirect("TiposABM/TipoModificar.aspx?id=" + tipoId);



            }
            else if (e.CommandName == "Eliminar")
            {
                Response.Redirect("TiposABM/TipoEliminar.aspx?id=" + e.CommandArgument);

            }
        }

       
    }
}

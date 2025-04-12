using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using dominio;
using negocio;

namespace Front
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["cliente"] == null || !EsAdministradorOSoporte((Cliente)Session["cliente"]))
            {
                Response.Redirect("Login.aspx");
                return;
            }


            if (!IsPostBack)
            {


                CargarGrillaClientes();

            }

        }

        private bool EsAdministradorOSoporte(Cliente cliente)
        {
            return cliente.idPerfil == 2 || cliente.idPerfil == 3 || cliente.idPerfil == 4;
        }


    
        private void CargarGrillaClientes()
        {
            ClienteNegocio negocio = new ClienteNegocio();
            GridViewClientes.DataSource = negocio.ListarClientes();
            GridViewClientes.DataBind();
        }


        protected void GridViewClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
                int UsuarioId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Modificar")
            {
                Response.Redirect("UsuariosABM/UsuarioModificar.aspx?id=" + UsuarioId);





            }
            if (e.CommandName == "Eliminar")
            {
                Response.Redirect("UsuariosABM/UsuarioModificar.aspx?id=" + UsuarioId);



            }
        }
    }
}
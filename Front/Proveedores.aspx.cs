
using dominio;
using negocio;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Front
{
    public partial class Proveedores : System.Web.UI.Page
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



                CargarGrilla();

            }
        }


        // Verifica si el id de perfil es valido
        private bool IDPerfilValido()
        {
            Usuario cliente = (Usuario)Session["cliente"];

            return cliente.idPerfil == 2 || cliente.idPerfil == 4 || cliente.idPerfil == 3;
        }



        private void CargarGrilla()
        {
            ProveedoresNegocio negocio = new ProveedoresNegocio();
            GridViewProveedores.DataSource = negocio.ListarProveedores();
            GridViewProveedores.DataBind();


        }




        protected void GridViewProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int proveedorId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Modificar")
            {
                Response.Redirect("ProveedoresABM/ProveedorModificar.aspx?id=" + proveedorId);

            }
            else
            if (e.CommandName == "Eliminar")
            {

                Response.Redirect("ProveedoresABM/ProveedorEliminar.aspx?id=" + proveedorId);

            }
        }


    }
}

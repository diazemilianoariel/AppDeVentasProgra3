
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
            Usuario usuario = Session["usuario"] as Usuario;

            
            if (usuario == null || !EsAdmin(usuario))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarGrilla();
            }



        }


      
        private bool EsAdmin(Usuario usuario)
        {
            // solo los Administradores pueden gestionar Proveedores.
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
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

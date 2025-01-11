using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace Front
{
    public partial class Perfiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



            if (Session["cliente"] == null || !EsAdministradorOSoporte((Cliente)Session["cliente"]))
            {
                Response.Redirect("Login.aspx");
                return;
            }
        }

        private bool EsAdministradorOSoporte(Cliente cliente)
        {
            return cliente.idPerfil == 2 || cliente.idPerfil == 4;
        }


        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
         
        }

        // modificar 
        protected void BtnModificar_Click(object sender, EventArgs e)
        {
        }

        // eliminar

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
        }

        //cancelar
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
        }


        

    }
}
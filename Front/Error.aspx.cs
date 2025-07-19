using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio; 

namespace Front
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                Exception ex = Server.GetLastError();


                if(ex != null)
                {
                    Usuario usuario = (Usuario)Session["usuario"] as Usuario;
                    if(usuario != null && usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador)
                    {
                        // Si es Admin, mostramos el panel de error técnico.
                        pnlErrorAdmin.Visible = true;
                        litErrorDetalle.Text = ex.Message;
                    }

                    /// limpiamos el error para que no quede pegado
                    Server.ClearError();

                }
            }

        }
    }
}
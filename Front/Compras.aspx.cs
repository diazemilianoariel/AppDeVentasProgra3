using System;

using dominio;


namespace Front
{
    public partial class Compras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


             if (Session["cliente"] == null || !IDPerfilValido())
            {
                Response.Redirect("Login.aspx");
                return;
            }




          

        }



        public bool IDPerfilValido()
        {
            Cliente cliente = (Cliente)Session["cliente"];
            return cliente.idPerfil == 2;
        }

    }
}
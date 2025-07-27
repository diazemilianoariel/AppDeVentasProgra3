using System;
using System.Linq;
using negocio;

namespace Front
{
    public partial class Ofertas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    OfertaNegocio negocio = new OfertaNegocio();
                    var listaOfertas = negocio.ListarOfertasActivas();

                    if (listaOfertas.Any())
                    {
                        rptOfertas.DataSource = listaOfertas;
                        rptOfertas.DataBind();
                    }
                    else
                    {
                        pnlNoHayOfertas.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    Session["error"] = ex;
                    Response.Redirect("Error.aspx");
                }
            }
        }
    }
}
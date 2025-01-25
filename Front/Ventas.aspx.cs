using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front
{
    public partial class Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["cliente"] == null || !EsVendedorAdminSoporte((Cliente)Session["cliente"]))
            //{
            //    Response.Redirect("Login.aspx");
            //    return;
            //}

            if (!IsPostBack)
            {
                CargarVentas();

            }
        }


        private void CargarVentas()
        {
            VentaNegocio negocio = new VentaNegocio();
            gvVentasPendientes.DataSource = negocio.ListarVentasPendientes();
            gvVentasPendientes.DataBind();

            gvVentas.DataSource = negocio.ListarVentas();
            gvVentas.DataBind();



        }

        //private bool EsVendedorAdminSoporte(Cliente cliente)
        //{
        //    return cliente.idPerfil == 2 || cliente.idPerfil == 3 || cliente.idPerfil == 4;
        //}

        protected void btnAprobar_Click(object sender, EventArgs e)
        {
            int idVenta = Convert.ToInt32(((Button)sender).CommandArgument);
            VentaNegocio negocio = new VentaNegocio();
            negocio.AprobarVenta(idVenta);

            CargarVentas();
           




        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            int idVenta = Convert.ToInt32(((Button)sender).CommandArgument);
            VentaNegocio negocio = new VentaNegocio();
            negocio.RechazarVenta(idVenta);

            CargarVentas();



        }



        private void MostrarMensaje(string mensaje, bool esError = true)
        {
            // Implementar la lógica para mostrar mensajes
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string query = txtBuscar.Text.Trim();
            VentaNegocio negocio = new VentaNegocio();
         

            gvVentas.DataSource = negocio.BuscarVentas(query);
            gvVentas.DataBind();
        }


    }
}

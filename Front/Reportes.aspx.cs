using dominio;
using negocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace Front
{
    public partial class Reportes : System.Web.UI.Page
    {
        CategoriaNegocio categorianegocio = new CategoriaNegocio();
        ClienteNegocio clienteNegocio = new ClienteNegocio();
        ProductoNegocio productonegocio = new ProductoNegocio();
        VentaNegocio ventanegocio = new VentaNegocio();

        protected string CategoriasJson;
        protected string ProductosJson;

        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["cliente"] == null || !IDPerfilValido())
            {
                Response.Redirect("Login.aspx");
                return;
            }


            if (!IsPostBack)
            {
                cargarDatosPanel();
                CargarTarjetas();
                
            }







        }


        public bool IDPerfilValido()
        {
            Cliente cliente = (Cliente)Session["cliente"];
            return cliente.idPerfil == 2;
        }



        public void CargarTarjetas()
        {
            litVentasHoy.Text = ventanegocio.cantidadVentasHoy().ToString();

            litPedidosPendientes.Text = ventanegocio.cantidadVentaPendiente().ToString();

            litUsuariosRegistrados.Text = clienteNegocio.CantidadUsuarios().ToString();

            litCantidadProductos.Text = productonegocio.CantidadProductos().ToString();
        }

        public void cargarDatosPanel()
        { 
             
     

            List<Venta> ventas = ventanegocio.ListarVentas();

            rptVentas.DataSource = ventas;
            rptVentas.DataBind(); 




        }












    }
}
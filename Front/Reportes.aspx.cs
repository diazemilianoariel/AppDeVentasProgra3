using negocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace Front
{
    public partial class Reportes : System.Web.UI.Page
    {

        ClienteNegocio clienteNegocio = new ClienteNegocio();
        ProductoNegocio productonegocio = new ProductoNegocio();
        VentaNegocio ventanegocio = new VentaNegocio();





        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CargarTarjetas();
                cargarDatosPanel();
            }







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
            List<int> ventasSemana = new List<int> { 120, 150, 180, 200, 220, 250, 300 };


            hfVentasSemana.Value = JsonConvert.SerializeObject(ventasSemana);


        }




    }
}
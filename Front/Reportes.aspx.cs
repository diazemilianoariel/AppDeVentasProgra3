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


            
            // Datos forzados (se pueden reemplazar con datos de BD luego)
            List<string> categorias = new List<string>();
            List<int> cantidades = new List<int>();
            var resultado = categorianegocio.CantidadesPorCategoria();

             categorias = resultado.Item1;
             cantidades = resultado.Item2;

          
            // Convertir a JSON para usar en el script
            CategoriasJson = new JavaScriptSerializer().Serialize(categorias);
            ProductosJson = new JavaScriptSerializer().Serialize(cantidades);



            /// tabla ultima 
             Venta venta = new Venta();


            List<Venta> ventas = ventanegocio.ListarVentas();

            rptVentas.DataSource = ventas;
            rptVentas.DataBind();




        }




    }
}
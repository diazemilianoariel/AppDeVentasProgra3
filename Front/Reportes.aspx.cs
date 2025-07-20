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
        UsuarioNegocio clienteNegocio = new UsuarioNegocio();
        ProductoNegocio productonegocio = new ProductoNegocio();
        VentaNegocio ventanegocio = new VentaNegocio();

        protected string CategoriasJson;
        protected string ProductosJson;

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
                PaginaActual = 1; // Inicializa la página actual en 1
                lblPaginaActual.Text = PaginaActual.ToString(); 
                CargarDatosPanel();
                CargarTarjetas();
                
            }







        }

        private bool EsAdmin(Usuario usuario)
        {
            // Según el plan, solo los Administradores pueden ver los reportes.
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }
        private int PaginaActual
        {
            get { return (int)(ViewState["PaginaActual"] ?? 1); }
            set { ViewState["PaginaActual"] = value; }
        }


   

        private int TamañoPagina = 10;



        public void CargarTarjetas()
        {
            litVentasHoy.Text = ventanegocio.cantidadVentasHoy().ToString();

            litPedidosPendientes.Text = ventanegocio.ContarVentasPorEstado(1).ToString();

            litUsuariosRegistrados.Text = clienteNegocio.CantidadUsuarios().ToString();

            litCantidadProductos.Text = productonegocio.CantidadProductos().ToString();
        }

       



        


        private void CargarDatosPanel()
        {
            List<Venta> ventas = ventanegocio.ListarVentas(); // Método que obtiene la lista de ventas
            int totalVentas = ventas.Count;

            //paginación
            var ventasPaginadas = ventas.Skip((PaginaActual - 1) * TamañoPagina).Take(TamañoPagina).ToList();

            rptVentas.DataSource = ventasPaginadas;
            rptVentas.DataBind();

            // Actualiza el estado de los botones de paginación
            btnAnterior.Enabled = PaginaActual > 1;
            btnSiguiente.Enabled = (PaginaActual * TamañoPagina) < totalVentas;
        }


        protected void btnAnterior_Click(object sender, EventArgs e)
        {

            if (PaginaActual > 1)
            {
                PaginaActual--;
                CargarDatosPanel(); // Recarga los datos de la página actual
                lblPaginaActual.Text = PaginaActual.ToString(); // Actualiza el número de página
            }
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            PaginaActual++;
            CargarDatosPanel();
            lblPaginaActual.Text = PaginaActual.ToString();
        }














    }
}
using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Front
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Producto> ListaProductos { get; set; }

        private DefaultNegocio defaultNegocio = new DefaultNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();


                try
                {
                    if (!IsPostBack)
                    {
                        ProductoNegocio negocio = new ProductoNegocio();

                        // 1. Leemos el ID de la categoría desde la URL
                        string idCategoria = Request.QueryString["cat"];

                        // 2. Pasamos el ID al método Listar. Si no hay ID, traerá todo.
                        ListaProductos = negocio.Listar(idCategoria);

                        // Opcional: Cambiar el título de la página
                        if (!string.IsNullOrEmpty(idCategoria))
                        {
                            // Aquí podrías tener una lógica para buscar el nombre de la categoría
                            // y mostrarlo en un Label, por ejemplo: "Mostrando productos de: Niños"
                        }

                        rptProductos.DataSource = ListaProductos;
                        rptProductos.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Session["error"] = ex;
                    Response.Redirect("Error.aspx");
                }
            


            }
        }

        private void CargarProductos()
        {
            try
            {

                string busqueda = txtBuscar.Text.Trim();
                List<Producto> listaProductos;

                if (string.IsNullOrEmpty(busqueda))
                {
                    listaProductos = defaultNegocio.ListarProductos();
                }
                else
                {
                    listaProductos = defaultNegocio.BuscarProductos(busqueda);
                }

                rptProductos.DataSource = listaProductos;
                rptProductos.DataBind();

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar productos: " + ex.Message, true);
            }
        }

     
        private List<Producto> ObtenerCarrito()
        {
            return (List<Producto>)Session["Carrito"] ?? new List<Producto>();
        }



        private void MostrarMensaje(string mensaje, bool esError = true)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "alert alert-danger" : "alert alert-success";
            lblMensaje.Visible = true;
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarProductos();
        }


        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            CargarProductos();
            txtBuscar.Text = "";
        }


        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            int idProducto = Convert.ToInt32(((Button)sender).CommandArgument);
            Response.Redirect($"Productos/DetalleProducto.aspx?id={idProducto}");
        }

    }
}

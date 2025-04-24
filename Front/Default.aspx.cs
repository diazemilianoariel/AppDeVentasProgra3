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
        private DefaultNegocio defaultNegocio = new DefaultNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
                ActualizarContadorCarrito();
            }
        }








        private void ActualizarContadorCarrito()
        {
            List<Producto> carrito = (List<Producto>)Session["Carrito"];
            int totalProductos = carrito != null ? carrito.Sum(p => p.Cantidad) : 0;
            var masterPage = (MASTER)this.Master;
            masterPage.ActualizarContadorCarrito(totalProductos);
        }


        private void CargarProductos()
        {
            try
            {


                List<Producto> listaProductos = defaultNegocio.ListarProductos();
                rptProductos.DataSource = listaProductos;
                rptProductos.DataBind();

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar productos: " + ex.Message, true);
            }
        }

        //protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int idProducto = Convert.ToInt32(((Button)sender).CommandArgument);
        //        Producto producto = defaultNegocio.ObtenerProducto(idProducto);

        //        if (producto != null)
        //        {
        //            List<Producto> carrito = ObtenerCarrito();
        //            RepeaterItem item = (RepeaterItem)((Button)sender).NamingContainer;
        //            TextBox txtCantidad = (TextBox)item.FindControl("txtCantidad");
        //            int cantidad = Convert.ToInt32(txtCantidad.Text);


        //            if (producto.stock >= cantidad)
        //            {
        //                defaultNegocio.AgregarProductosAlCarrito(carrito, producto, cantidad);


        //                Session["Carrito"] = carrito;
        //                MostrarMensaje("Producto agregado al carrito.", false);
        //                ActualizarContadorCarrito();


        //                ScriptManager.RegisterStartupScript(this, GetType(), "ScrollToMessage", "scrollToMessage();", true);

        //                ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", "setTimeout(function() { window.location.href = 'Default.aspx'; }, 2000);", true);

        //            }
        //            else
        //            {
        //                MostrarMensaje("No hay suficiente stock disponible para agregar la cantidad solicitada.", true);

        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        MostrarMensaje("Error al agregar producto al carrito: " + ex.Message, true);
        //    }
        //}



        //protected void btnQuitarCarrito_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int idProducto = Convert.ToInt32(((Button)sender).CommandArgument);
        //        List<Producto> carrito = ObtenerCarrito();
        //        if (carrito != null)
        //        {
        //            defaultNegocio.QuitarProductoDelCarrito(carrito, idProducto);
        //            Session["Carrito"] = carrito;
        //            MostrarMensaje("Producto eliminado del carrito.", false);
        //            ActualizarContadorCarrito();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MostrarMensaje("Error al quitar producto del carrito: " + ex.Message, true);
        //    }
        //}

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
            try
            {
                string busqueda = txtBuscar.Text.Trim();
                List<Producto> listaFiltrada;

                if (string.IsNullOrEmpty(busqueda))
                {
                    listaFiltrada = defaultNegocio.ListarProductos();
                }
                else
                {
                    listaFiltrada = defaultNegocio.BuscarProductos(busqueda);
                }

                rptProductos.DataSource = listaFiltrada;
                rptProductos.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al buscar productos: " + ex.Message, true);
            }
        }


        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            CargarProductos();
            txtBuscar.Text = "";
        }






        //protected void btnDisminuir_Click(object sender, EventArgs e)
        //{
        //    Button btnDisminuir = (Button)sender;
        //    RepeaterItem item = (RepeaterItem)btnDisminuir.NamingContainer;
        //    TextBox txtCantidad = (TextBox)item.FindControl("txtCantidad");
        //    int cantidad = Convert.ToInt32(txtCantidad.Text);
        //    if (cantidad > 1)
        //    {
        //        txtCantidad.Text = (cantidad - 1).ToString();
        //    }
        //}

        //protected void btnAumentar_Click(object sender, EventArgs e)
        //{
        //    Button btnAumentar = (Button)sender;
        //    RepeaterItem item = (RepeaterItem)btnAumentar.NamingContainer;
        //    TextBox txtCantidad = (TextBox)item.FindControl("txtCantidad");
        //    string[] args = btnAumentar.CommandArgument.Split(',');
        //    int stockDisponible = Convert.ToInt32(args[1]);
        //    int cantidad = Convert.ToInt32(txtCantidad.Text);

        //    if (cantidad < stockDisponible)
        //    {
        //        txtCantidad.Text = (cantidad + 1).ToString();
        //    }
        //}






        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            int idProducto = Convert.ToInt32(((Button)sender).CommandArgument);
            Response.Redirect($"Productos/DetalleProducto.aspx?id={idProducto}");
        }






    }
}

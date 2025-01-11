using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Front
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        private void CargarProductos()
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            List<Producto> listaProductos = productoNegocio.ListarProductos();
            rptProductos.DataSource = listaProductos;
            rptProductos.DataBind();
        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            // Implementar la lógica para agregar productos al carrito
        }

        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            // Implementar la lógica para confirmar la compra
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            // Implementar la lógica para eliminar productos del carrito
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
           

            ProductoNegocio productoNegocio = new ProductoNegocio();
            List<Producto> listaProductos = productoNegocio.ListarProductos();
            List<Producto> listaFiltrada = new List<Producto>();

            foreach (Producto producto in listaProductos)
            {
                if (producto.nombre.ToLower().Contains(txtBuscar.Text.ToLower()))
                {
                    listaFiltrada.Add(producto);
                }
            }

            rptProductos.DataSource = listaFiltrada;

            rptProductos.DataBind();

        }
    }
}

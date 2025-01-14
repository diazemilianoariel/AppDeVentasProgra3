﻿using dominio;
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
                ProductoNegocio productoNegocio = new ProductoNegocio();
                {
                    List<Producto> listaProductos = productoNegocio.ListarProductos();
                    rptProductos.DataSource = listaProductos;
                    rptProductos.DataBind();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar productos: " + ex.Message, true);
            }
        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                int idProducto = Convert.ToInt32(((Button)sender).CommandArgument);
                ProductoNegocio productoNegocio = new ProductoNegocio();
                Producto producto = productoNegocio.ObtenerProducto(idProducto);
                {
                    if (producto != null)
                    {
                        List<Producto> carrito = ObtenerCarrito();
                        RepeaterItem item = (RepeaterItem)((Button)sender).NamingContainer;
                        TextBox txtCantidad = (TextBox)item.FindControl("txtCantidad");
                        int cantidad = Convert.ToInt32(txtCantidad.Text);

                        AgregarProductoAlCarrito(carrito, producto, cantidad);
                        Session["Carrito"] = carrito;
                        MostrarMensaje("Producto agregado al carrito.", false);
                        ActualizarContadorCarrito();

                        // Mostrar el modal
                        iframeCarrito.Attributes["src"] = "Carrito.aspx";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "document.getElementById('btnShowModal').click();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al agregar producto al carrito: " + ex.Message, true);
            }
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            mpeCarrito.Hide();
        }

        protected void btnQuitarCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                int idProducto = Convert.ToInt32(((Button)sender).CommandArgument);
                List<Producto> carrito = ObtenerCarrito();
                if (carrito != null)
                {
                    Producto productoEnCarrito = carrito.Find(p => p.id == idProducto);
                    if (productoEnCarrito != null)
                    {
                        carrito.Remove(productoEnCarrito);
                        Session["Carrito"] = carrito;
                        MostrarMensaje("Producto eliminado del carrito.", false);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al quitar producto del carrito: " + ex.Message, true);
            }
        }

        private List<Producto> ObtenerCarrito()
        {
            return (List<Producto>)Session["Carrito"] ?? new List<Producto>();
        }

        private void AgregarProductoAlCarrito(List<Producto> carrito, Producto producto, int cantidad)
        {
            Producto productoEnCarrito = carrito.Find(p => p.id == producto.id);
            if (productoEnCarrito != null)
            {
                productoEnCarrito.Cantidad += cantidad; 
            }
            else
            {
                producto.Cantidad = cantidad;
                carrito.Add(producto);
            }
        }

        private void MostrarMensaje(string mensaje, bool esError = true)
        {
            // Implementar la lógica para mostrar mensajes
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
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

        protected void btnDisminuir_Click(object sender, EventArgs e)
        {
            Button btnDisminuir = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnDisminuir.NamingContainer;
            TextBox txtCantidad = (TextBox)item.FindControl("txtCantidad");
            int cantidad = Convert.ToInt32(txtCantidad.Text);
            if (cantidad > 1)
            {
                txtCantidad.Text = (cantidad - 1).ToString();
            }
        }

        protected void btnAumentar_Click(object sender, EventArgs e)
        {
            Button btnAumentar = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnAumentar.NamingContainer;
            TextBox txtCantidad = (TextBox)item.FindControl("txtCantidad");
            int cantidad = Convert.ToInt32(txtCantidad.Text);
            txtCantidad.Text = (cantidad + 1).ToString();
        }

        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            int idProducto = Convert.ToInt32(((Button)sender).CommandArgument);
            Response.Redirect($"DetalleProducto.aspx?id={idProducto}");
        }

    
    }
}

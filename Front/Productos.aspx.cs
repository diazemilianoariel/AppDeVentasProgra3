﻿using dominio;
using negocio;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Front
{
    public partial class producto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CargarGrilla();
            }


        }


        private void CargarGrilla()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            GridViewProductos.DataSource = negocio.ListarProductos();
            GridViewProductos.DataBind();
        }

        protected void GridViewProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridViewProductos.SelectedRow;
            if (row != null && row.Cells.Count > 1) // Asegurarse de que la fila y la celda existen
            {
                int id = Convert.ToInt32(row.Cells[1].Text); // Asegúrate de que esta celda contiene el ID correcto y es convertible a int

                ProductoNegocio negocio = new ProductoNegocio();
                Producto producto = negocio.ObtenerProducto(id);

                // Asignar los valores a los TextBoxes
                TextBoxId.Text = producto.id.ToString();
                TextBoxNombre.Text = producto.nombre;
                TextBoxDescripcion.Text = producto.descripcion;
                TextBoxImagen.Text = producto.imagen;
                TextBoxPrecio.Text = producto.precio.ToString();
            }
            else
            {
                // Manejar el caso donde la fila o la celda no existen
                // Por ejemplo, mostrar un mensaje de error

                // Limpiar los TextBoxes
                TextBoxId.Text = "";
                TextBoxNombre.Text = "";
                TextBoxDescripcion.Text = "";
                TextBoxImagen.Text = "";
                TextBoxPrecio.Text = "";

            }
        }



        // crear la definicion de boton1_click
        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            Producto producto = new Producto();

            producto.nombre = TextBoxNombre.Text;
            producto.descripcion = TextBoxDescripcion.Text;
            producto.imagen = TextBoxImagen.Text;
            producto.precio = Convert.ToDecimal(TextBoxPrecio.Text);

            // instanciar la clase ProductoNegocio
            ProductoNegocio negocio = new ProductoNegocio();
            // llamar al metodo AgregarProducto
            negocio.AgregarProducto(producto);


        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Producto producto = new Producto();

            producto.id = Convert.ToInt32(TextBoxId.Text);
            producto.nombre = TextBoxNombre.Text;
            producto.descripcion = TextBoxDescripcion.Text;
            producto.imagen = TextBoxImagen.Text;
            producto.precio = Convert.ToDecimal(TextBoxPrecio.Text);

            ProductoNegocio negocio = new ProductoNegocio();
            negocio.ModificarProducto(producto);
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            negocio.EliminarProducto(Convert.ToInt32(TextBoxId.Text));
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            TextBoxId.Text = "";
            TextBoxNombre.Text = "";
            TextBoxDescripcion.Text = "";
            TextBoxImagen.Text = "";
            TextBoxPrecio.Text = "";
        }

        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            // Obtener el id del producto seleccionado
            GridViewRow row = GridViewProductos.SelectedRow;
            int id = Convert.ToInt32(row.Cells[1].Text); // Asegúrate de que esta celda contiene el ID correcto y es convertible a int

            // Redirigir a la página de detalle, pasando el ID como parámetro
            Response.Redirect("Detalle.aspx?id=" + id);
        }





        protected void GridViewProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                // Verificar si el índice está dentro del rango
                if (index >= 0 && index < GridViewProductos.Rows.Count)
                {
                    GridViewRow row = GridViewProductos.Rows[index];
                    if (row != null && row.Cells.Count > 0) // Asegurarse de que la fila y la celda existen
                    {
                        int id = Convert.ToInt32(row.Cells[0].Text);

                        ProductoNegocio negocio = new ProductoNegocio();
                        Producto producto = negocio.ObtenerProducto(id);

                        // Asignar los valores a los TextBoxes
                        TextBoxId.Text = producto.id.ToString();
                        TextBoxNombre.Text = producto.nombre;
                        TextBoxDescripcion.Text = producto.descripcion;
                        TextBoxImagen.Text = producto.imagen;
                        TextBoxPrecio.Text = producto.precio.ToString();
                    }
                }
                else
                {
                    // Manejar el caso donde el índice está fuera del rango

                    // Limpiar los TextBoxes
                    TextBoxId.Text = "";
                    TextBoxNombre.Text = "";
                    TextBoxDescripcion.Text = "";
                    TextBoxImagen.Text = "";
                    TextBoxPrecio.Text = "";


                }
            }
        }



    }
}
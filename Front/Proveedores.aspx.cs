﻿
using dominio;
using negocio;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Front
{
    public partial class Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



            if (Session["cliente"] == null || !IDPerfilValido())
            {
                Response.Redirect("Login.aspx");
                return;
            }


            if (!IsPostBack)
            {
                


                CargarGrilla();

            }
        }


        // Verifica si el id de perfil es valido
        private bool IDPerfilValido()
        {
            Cliente cliente = (Cliente)Session["cliente"];

            return cliente.idPerfil == 2 || cliente.idPerfil == 4 || cliente.idPerfil == 3;
        }



        private void CargarGrilla()
        {
            ProveedoresNegocio negocio = new ProveedoresNegocio();
            GridViewProveedores.DataSource = negocio.ListarProveedores();
            GridViewProveedores.DataBind();


        }

        protected void GridViewProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow fila = GridViewProveedores.SelectedRow;
            if (fila != null && fila.Cells.Count > 1) 
            {
                int id = Convert.ToInt32(fila.Cells[1].Text); 

                ProveedoresNegocio negocio = new ProveedoresNegocio();
                Proveedor proveedor = negocio.ObtenerProveedor(id);

                // Asignar los valores a los TextBoxes
                TextBoxNombre.Text = proveedor.Nombre;
                TextBoxDireccion.Text = proveedor.Direccion;
                TextBoxTelefono.Text = proveedor.Telefono;
                TextBoxEmail.Text = proveedor.Email;


            }
            else
            {
                LimpiarCampos();
            }
        }


        protected void GridViewProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                // Verificar si el índice está dentro del rango
                if (index >= 0 && index < GridViewProveedores.Rows.Count)
                {
                    GridViewRow row = GridViewProveedores.Rows[index];
                    if (row != null && row.Cells.Count > 0) // Asegurarse de que la fila y la celda existen
                    {
                        int idProveedor = Convert.ToInt32(row.Cells[0].Text); // Suponemos que el ID está en la columna 0

                        ProveedoresNegocio negocio = new ProveedoresNegocio();
                        Proveedor proveedor = negocio.ObtenerProveedor(idProveedor);

                        // Asignar los valores a los TextBoxes
                        TextBoxId.Text = proveedor.id.ToString();
                        TextBoxNombre.Text = proveedor.Nombre;
                        TextBoxDireccion.Text = proveedor.Direccion;
                        TextBoxTelefono.Text = proveedor.Telefono;
                        TextBoxEmail.Text = proveedor.Email;
                    }
                }
                else
                {
                    // Manejar el caso donde el índice está fuera del rango
                    // Limpiar los TextBoxes
                    LimpiarCampos();
                    Response.Write("¡Ups! Parece que seleccionaste una fila inexistente.");
                }
            }
            else
            {
                if (e.CommandName == "VerDetalle")
                {
                    int index = Convert.ToInt32(e.CommandArgument);

                    // Verificar si el índice está dentro del rango
                    if (index >= 0 && index < GridViewProveedores.Rows.Count)
                    {
                        GridViewRow row = GridViewProveedores.Rows[index];
                        if (row != null && row.Cells.Count > 0) // Asegurarse de que la fila y la celda existen
                        {
                            int idProveedor = Convert.ToInt32(row.Cells[0].Text); // Suponemos que el ID está en la columna 0

                            // Redirigir a la página de detalle, pasando el ID como parámetro
                            Response.Redirect("DetalleProveedor.aspx?id=" + idProveedor);
                        }
                    }
                    else
                    {
                        // Manejar el caso donde el índice está fuera del rango
                        // Por ejemplo, mostrar un mensaje de error al usuario
                        Response.Write("¡Ups! Parece que seleccionaste una fila inexistente.");


                    }
                }

            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    ProveedoresNegocio negocio = new ProveedoresNegocio();
                    Proveedor proveedor = new Proveedor
                    {
                        Nombre = TextBoxNombre.Text,
                        Direccion = TextBoxDireccion.Text,
                        Telefono = TextBoxTelefono.Text,
                        Email = TextBoxEmail.Text
                    };

                    Proveedor proveedorExistente = new Proveedor();
                    proveedorExistente = negocio.ObtenerProveedorPorEmail(proveedor.Email);
                    if (proveedorExistente != null)
                    {
                        if (!proveedorExistente.estado)
                        {
                            // Mostrar mensaje de confirmación para reactivar el proveedor
                            lblConfirmacionReactivacion.Visible = true;
                            btnConfirmarReactivacion.Visible = true;
                            btnConfirmarReactivacion.CommandArgument = proveedorExistente.id.ToString();
                            return;
                        }
                        else
                        {
                            Response.Write("El proveedor ya existe y está activo.");
                            return;
                        }
                    }

                    negocio.AgregarProveedor(proveedor);
                    CargarGrilla();
                    LimpiarCampos();
                }
                else
                {
                    // Mostrar ventana emergente
                    string script = "alert('por favor complete todos los campos.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                    return;

                }
            }
            catch (Exception ex)
            {
                // Manejar el error
                Response.Write("Ocurrió un error: " + ex.Message);
            }
        }

        protected void btnConfirmarReactivacion_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(((Button)sender).CommandArgument);
            ProveedoresNegocio negocio = new ProveedoresNegocio();
            negocio.ActivarProveedor(id);
            string script = "alert('Proveedor reactivado exitosamente.');";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            CargarGrilla();
            LimpiarCampos();
            lblConfirmacionReactivacion.Visible = false;
            btnConfirmarReactivacion.Visible = false;
        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(TextBoxNombre.Text))
            {
                // Mostrar ventana emergente
                string script = "alert('por favor seleccione un Proveedor para modificar.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                return;
            }


            Proveedor proveedor = new Proveedor
            {

                id = Convert.ToInt32(TextBoxId.Text),
                Nombre = TextBoxNombre.Text,
                Direccion = TextBoxDireccion.Text,
                Telefono = TextBoxTelefono.Text,
                Email = TextBoxEmail.Text
            };

            ProveedoresNegocio negocio = new ProveedoresNegocio();
            negocio.ModificarProveedor(proveedor);
            CargarGrilla();

            LimpiarCampos();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxNombre.Text))
            {
                // Mostrar ventana emergente
                string script = "alert('porfavor seleccione un elemento para eliiminar.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                return;
            }

            ProveedoresNegocio negocio = new ProveedoresNegocio();
            negocio.EliminarProveedor(Convert.ToInt32(TextBoxId.Text));
            CargarGrilla();
            LimpiarCampos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
           LimpiarCampos();
        }


        //protected void BtnVerDetalle_Click(object sender, EventArgs e)
        //{
        //    // Obtener el id del proveedor seleccionado
        //    GridViewRow fila = GridViewProveedores.SelectedRow;
        //    if (fila != null)
        //    {
        //        int id = Convert.ToInt32(fila.Cells[1].Text); // Asegúrate de que esta celda contiene el ID correcto y es convertible a int
        //        // Redirigir a la página de detalle, pasando el ID como parámetro
        //        Response.Redirect("DetalleProveedor.aspx?id=" + id);
        //    }
        //    else
        //    {
        //        // Manejar el caso cuando no hay fila seleccionada
        //        // Por ejemplo, mostrar un mensaje de error al usuario
        //        Response.Write("Por favor, seleccione un proveedor.");
        //    }
        //}

        private void LimpiarCampos()
        {
            TextBoxId.Text = "";
            TextBoxNombre.Text = "";
            TextBoxDireccion.Text = "";
            TextBoxTelefono.Text = "";
            TextBoxEmail.Text = "";
        }



        private bool ValidarCampos()
        {
            return !string.IsNullOrEmpty(TextBoxNombre.Text) &&
                   !string.IsNullOrEmpty(TextBoxDireccion.Text) &&
                   !string.IsNullOrEmpty(TextBoxTelefono.Text) &&
                   !string.IsNullOrEmpty(TextBoxEmail.Text);
        }




    }
}

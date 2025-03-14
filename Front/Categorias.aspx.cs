﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front
{
    public partial class Categorias : System.Web.UI.Page
    {
        protected TextBox TextBoxIdCategoria;
        protected TextBox TextBoxNuevaCategoria;
        protected Panel editSection; // Sección de edición

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cliente"] == null || !EsAdministradorOSoporte((Cliente)Session["cliente"]))
            {
                Response.Redirect("Login.aspx");
                return;
            }




            if (!IsPostBack)
            {
                CargarCategorias();
            }



        }

        private bool EsAdministradorOSoporte(Cliente cliente)
        {
            return cliente.nombrePerfil == "Administrador" || cliente.nombrePerfil == "Soporte" || cliente.nombrePerfil == "Vendedor";
        }


        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            GridViewCategorias.DataSource = negocio.ListarCategorias();
            GridViewCategorias.DataBind();
        }

        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria categoria = new Categoria();

            if (TextBoxCategoria != null && !string.IsNullOrEmpty(TextBoxCategoria.Text))
            {
                categoria.nombre = TextBoxCategoria.Text;
                if (!negocio.buscarCategoriaNombre(categoria.nombre)){


                    categoria.nombre = TextBoxCategoria.Text;
                    negocio.AgregarCategoria(categoria);

                }
                else
                {
                    negocio.altaLogica(categoria.nombre);
                }
                    CargarCategorias();
                TextBoxCategoria.Text = string.Empty; // Limpiar el TextBox después de agregar
            }
            else
            {
               
                    // Mostrar ventana emergente
                    string script = "alert('por favor proporcione un nuevo nombre de categoria.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                    return;
                
            }
        }

        protected void GridViewCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = GridViewCategorias.Rows[index];

                if (fila != null && TextBoxCategoria != null && !string.IsNullOrEmpty(TextBoxCategoria.Text))
                {
                    int idCategoria = Convert.ToInt32(fila.Cells[0].Text);
                    string nuevoNombre = TextBoxCategoria.Text;

                    CategoriaNegocio negocio = new CategoriaNegocio();
                    Categoria categoria = new Categoria
                    {
                        id = idCategoria,
                        nombre = nuevoNombre
                    };

                    // existecategoria ?

                   ;


                    if (!negocio.ExisteCategoria(categoria.nombre))
                    {


                    negocio.ActualizarCategoria(categoria);
                    CargarCategorias();

                    // Limpiar el TextBoxCategoria después de actualizar
                    TextBoxCategoria.Text = string.Empty;
                    }
                    else
                    {
                        // una ventana emergente que diga que ya existe la categoria
                        // Mostrar una ventana emergente que diga que ya existe la categoría
                        lblError.Text = "categoria_Existente";
                    



                    }



                }
                else
                {
                    
                        // Mostrar ventana emergente
                        string script = "alert('por favor proporcione un nombre y luego precione editar.');";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                        return;
                    

                }
            }



            else if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = GridViewCategorias.Rows[index];

                if (fila != null)
                {
                    int idCategoria = Convert.ToInt32(fila.Cells[0].Text);

                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.bajaLogica(idCategoria);
                    CargarCategorias();

                    // Vaciar el TextBoxNuevaCategoria si se elimina una categoría
                    if (TextBoxNuevaCategoria != null)
                    {
                        TextBoxNuevaCategoria.Text = string.Empty;
                    }
                }
            }
        }


        protected void btnActualizarCategoria_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria categoria = new Categoria();

            if (TextBoxIdCategoria != null && TextBoxNuevaCategoria != null)
            {
                categoria.id = Convert.ToInt32(TextBoxIdCategoria.Text);
                categoria.nombre = TextBoxNuevaCategoria.Text;

                negocio.ActualizarCategoria(categoria);
                CargarCategorias();

                // Ocultar la sección de edición
                if (editSection != null)
                {
                    editSection.Style["display"] = "none";
                }
            }
        }


    }
}

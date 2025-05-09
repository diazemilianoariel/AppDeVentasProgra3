﻿using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Front.CategoriasABM
{
    public partial class CategoriaModificar : System.Web.UI.Page
    {


        CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int categoriaId = Convert.ToInt32(Request.QueryString["id"]);
                CargarDatosCategoria(categoriaId);
            }

            // Verificar si el usuario tiene permisos para acceder a esta página
            if (Session["cliente"] == null || !EsAdministradorOSoporte((Cliente)Session["cliente"]))
            {
                Response.Redirect("Login.aspx");
                return;
            }

        }


        private bool EsAdministradorOSoporte(Cliente cliente)
        {
            return cliente.nombrePerfil == "Administrador" || cliente.nombrePerfil == "Soporte" || cliente.nombrePerfil == "Vendedor";
        }




        private void CargarDatosCategoria(int categoriaId)
        {
            // Implementa la lógica para obtener los datos del producto de la base de datos
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            Categoria categoria = new Categoria();

            categoria = categoriaNegocio.BuscarCategoria(categoriaId);



            if (categoria != null)
            {
                LabelId.Text = categoria.id.ToString();
                TextBoxNombre.Text = categoria.nombre;
                CheckBoxEstado.Checked = categoria.estado;

                // Guardar el nombre original en un HiddenField
                HiddenFieldNombreOriginal.Value = categoria.nombre;

            }

        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            // Validar que el campo 'Nombre' no esté vacío
            if (string.IsNullOrWhiteSpace(TextBoxNombre.Text))
            {
                LabelError.Text = "El campo 'Nombre' es obligatorio.";
                LabelError.Visible = true;
                return;
            }

            // Verificar si el nombre fue modificado
            if (!TextBoxNombre.Text.Equals(HiddenFieldNombreOriginal.Value, StringComparison.OrdinalIgnoreCase))
            {
                // Validar si el nombre ya existe
                List<Categoria> listaDeCategorias = categoriaNegocio.ListarCategorias();
                foreach (var categoria2 in listaDeCategorias)
                {
                    if (categoria2.nombre.Equals(TextBoxNombre.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        LabelErrorCategoriaExistente.Text = "El nombre de la categoría ya existe.";
                        LabelErrorCategoriaExistente.Visible = true;
                        return;
                    }
                }
            }

            // Actualizar la categoría
            int categoriaId = Convert.ToInt32(LabelId.Text);
            var categoria = new Categoria
            {
                id = categoriaId,
                nombre = TextBoxNombre.Text,
                estado = CheckBoxEstado.Checked
            };

            categoriaNegocio.ActualizarCategoria(categoria);

            // Redirige a la página de lista de categorías
            Response.Redirect("../Categorias.aspx");
        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {

            // Redirige a la página de lista de productos
            Response.Redirect("../Categorias.aspx");
        }



    }
}
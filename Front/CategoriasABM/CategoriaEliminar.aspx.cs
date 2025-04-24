using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front.CategoriasABM
{
    public partial class CategoriaEliminar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int categoriaId = Convert.ToInt32(Request.QueryString["id"]);
                CargarDatosCategoria(categoriaId);
            }

            if (Session["cliente"] == null || !IDPerfilValido())
            {
                Response.Redirect("Login.aspx");
                return;
            }



        }
        private bool IDPerfilValido()
        {
            Cliente cliente = (Cliente)Session["cliente"];
            return cliente.nombrePerfil == "Administrador" || cliente.nombrePerfil == "Soporte" || cliente.nombrePerfil == "Vendedor";
        }

        private void CargarDatosCategoria(int categoriaId)
        {
            // Implementa la lógica para obtener los datos de la categoría de la base de datos
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            var categoria = categoriaNegocio.BuscarCategoria(categoriaId);
            if (categoria != null)
            {
                LabelNombreCategoria.Text = categoria.nombre;
                LabelEstadoCategoria.Text = categoria.estado ? "Activo" : "Inactivo";


            }
            else
            {
                // Manejar el caso en que no se encuentra la categoría
                // Puedes redirigir a otra página o mostrar un mensaje de error

                LabelError.Text = "Categoría no encontrada.";
                LabelError.Visible = true;

            }
        }


        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            int categoriaId = Convert.ToInt32(Request.QueryString["id"]);
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            categoriaNegocio.bajaLogica(categoriaId);
            Response.Redirect("../Categorias.aspx");
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Categorias.aspx");
        }





    }
}
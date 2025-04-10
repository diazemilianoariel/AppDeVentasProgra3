using dominio;
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

        }


        private void CargarDatosCategoria(int categoriaId)
        {
            // Implementa la lógica para obtener los datos del producto de la base de datos
            var categoria = categoriaNegocio.BuscarCategoria(categoriaId);
            if (categoria != null)
            {
                LabelId.Text = categoria.id.ToString();
                TextBoxNombre.Text = categoria.nombre;
                CheckBoxEstado.Checked = categoria.estado;
            }
        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            int categoriaId = Convert.ToInt32(LabelId.Text);
            var categoria = new Categoria
            {
                id = categoriaId,
                nombre = TextBoxNombre.Text,
                estado = CheckBoxEstado.Checked
            };



            // Implementa la lógica para actualizar el producto en la base de datos
            categoriaNegocio.ActualizarCategoria(categoria);


            // Redirige a la página de lista de productos después de guardar
            Response.Redirect("../Categorias.aspx");
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {

            // Redirige a la página de lista de productos
            Response.Redirect("../Categorias.aspx");
        }



    }
}
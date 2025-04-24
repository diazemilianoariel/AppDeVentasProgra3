using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front.MarcasABM
{
    public partial class MarcaModificar : System.Web.UI.Page
    {
        MarcaNegocio marcaNegocio = new MarcaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int marcaId = Convert.ToInt32(Request.QueryString["id"]);
                CargarDatosMarca(marcaId);
            }


        }


        private void CargarDatosMarca(int marcaId)
        {
            // Implementa la lógica para obtener los datos del producto de la base de datos
            var marca = new MarcaNegocio().BuscarMarca(marcaId);
            if (marca != null)
            {
                LabelId.Text = marca.id.ToString();
                TextBoxNombre.Text = marca.nombre;
                CheckBoxEstado.Checked = marca.estado;
            }
        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            int marcaId = Convert.ToInt32(LabelId.Text);
            Marca marca = new Marca();

            marca.id = marcaId;
            marca.nombre = TextBoxNombre.Text;
            marca.estado = CheckBoxEstado.Checked;
            
            // Implementa la lógica para actualizar el producto en la base de datos
            marcaNegocio.ActualizarMarca(marca);
            // Redirige a la página de lista de productos después de guardar
            Response.Redirect("../Marcas.aspx");
        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            // Redirige a la página de lista de productos sin guardar cambios
            Response.Redirect("../Marcas.aspx");
        }


    }
}
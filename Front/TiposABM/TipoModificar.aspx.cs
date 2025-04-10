using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Front.TiposABM
{
    public partial class TIpoModificar : System.Web.UI.Page
    {

        TipoNegocio tipoNegocio = new TipoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int tipoId = Convert.ToInt32(Request.QueryString["id"]);
                CargarDatosTipo(tipoId);
            }


        }

        private void CargarDatosTipo(int tipoId)
        {
            // Implementa la lógica para obtener los datos del producto de la base de datos
            var tipo = new TipoNegocio().BuscarTipo(tipoId);
            if (tipo != null)
            {
                LabelId.Text = tipo.id.ToString();
                TextBoxNombre.Text = tipo.nombre;
                CheckBoxEstado.Checked = tipo.estado;
            }
        }


        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {


            int tipoId = Convert.ToInt32(LabelId.Text);
            var tipo = new Tipos
            {
                id = tipoId,
                nombre = TextBoxNombre.Text,
                estado = CheckBoxEstado.Checked
            };


            // Implementa la lógica para actualizar el producto en la base de datos
            tipoNegocio.ActualizarTipo(tipo);


            // Redirige a la página de lista de productos después de guardar
            Response.Redirect("../Tipos.aspx");
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Tipos.aspx");
        }


    }
}
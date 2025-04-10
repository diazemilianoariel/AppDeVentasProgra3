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
    public partial class TipoEliminar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int tipoId = Convert.ToInt32(Request.QueryString["id"]);
                CargarDatosTipo(tipoId);
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

        private void CargarDatosTipo(int tipoId)
        {
            // Implementa la lógica para obtener los datos del tipo de la base de datos
            TipoNegocio tipoNegocio = new TipoNegocio();
            var tipo = tipoNegocio.BuscarTipo(tipoId);
            if (tipo != null)
            {
                LabelNombreTipo.Text = tipo.nombre;
                LabelEstadoTipo.Text = tipo.estado.ToString();
            }
            else
            {
                // Manejar el caso en que no se encuentra el tipo
                // Puedes redirigir a otra página o mostrar un mensaje de error
                LabelError.Text = "Tipo no encontrado.";
                LabelError.Visible = true;
            }
        }


        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            int tipoId = Convert.ToInt32(Request.QueryString["id"]);
            TipoNegocio tipoNegocio = new TipoNegocio();
            tipoNegocio.bajaLogica(tipoId);
            Response.Redirect("../Tipos.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Tipos.aspx");
        }
    }
}
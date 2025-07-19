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
    public partial class MarcaEliminar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int marcaId = Convert.ToInt32(Request.QueryString["id"]);
                CargarDatosMarca(marcaId);
            }

            if (Session["cliente"] == null || !IDPerfilValido())
            {
                Response.Redirect("Login.aspx");
                return;
            }

        }

        private bool IDPerfilValido()
        {
            Usuario cliente = (Usuario)Session["cliente"];
            return cliente.nombrePerfil == "Administrador" || cliente.nombrePerfil == "Soporte" || cliente.nombrePerfil == "Vendedor";
        }

        private void CargarDatosMarca(int marcaId)
        {
            // Implementa la lógica para obtener los datos de la marca de la base de datos
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            var marca = marcaNegocio.BuscarMarca(marcaId);
            if (marca != null)
            {
                LabelNombreMarca.Text = marca.nombre;
                LabelEstadoMarca.Text = marca.estado ? "Activo" : "Inactivo";
            }
            else
            {
                // Manejar el caso en que no se encuentra la marca
                // Puedes redirigir a otra página o mostrar un mensaje de error
                LabelError.Text = "Marca no encontrada.";
                LabelError.Visible = true;
            }
        }



        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            int marcaId = Convert.ToInt32(Request.QueryString["id"]);
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            marcaNegocio.bajaLogica(marcaId);
            Response.Redirect("../Marcas.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Marcas.aspx");
        }
    }
}
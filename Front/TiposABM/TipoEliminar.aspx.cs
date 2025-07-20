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

            Usuario usuario = Session["usuario"] as Usuario;
            if(usuario == null || !EsAdmin(usuario))
            {
                Response.Redirect("../Login.aspx");
            }


            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int tipoId = Convert.ToInt32(Request.QueryString["id"]);
                    CargarDatosTipo(tipoId);
                }
                else
                {
                    Response.Redirect("../Tipos.aspx");

                }
            }

          

        }


        private bool EsAdmin(Usuario usuario)
        {
            // Según el plan, solo los Administradores pueden gestionar Tipos.
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }




        private void CargarDatosTipo(int tipoId)
        {
            // Implementa la lógica para obtener los datos del tipo de la base de datos
            TipoNegocio tipoNegocio = new TipoNegocio();
            var tipo = tipoNegocio.BuscarTipo(tipoId);
            if (tipo != null)
            {
                LabelNombreTipo.Text = tipo.nombre;
                LabelEstadoTipo.Text = tipo.estado ? "Activo" : "Inactivo";


            }
            else
            {
                // Manejar el caso en que no se encuentra el tipo
                // Puedes redirigir a otra página o mostrar un mensaje de error
                LabelError.Text = "Tipo no encontrado.";
                LabelError.Visible = true;
                btnConfirmar.Visible = false;
            }
        }


        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                int tipoId = Convert.ToInt32(Request.QueryString["id"]);
                TipoNegocio tipoNegocio = new TipoNegocio();
                tipoNegocio.bajaLogica(tipoId); // Asumo que es baja lógica
                Response.Redirect("../Tipos.aspx");
            }
            catch (Exception ex)
            {
                LabelError.Text = "Ocurrió un error al eliminar el tipo." + ex.Message;
                LabelError.Visible = true;
               
                
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Tipos.aspx");
        }
    }
}
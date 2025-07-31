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



            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null || !EsAdmin(usuario))
            {
                Response.Redirect("../Login.aspx");
                return;
            }


            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int marcaId = Convert.ToInt32(Request.QueryString["id"]);
                    CargarDatosMarca(marcaId);
                }
                else
                {
                    // Si no hay ID, no hay nada que eliminar.
                    Response.Redirect("../Marcas.aspx");
                }
            }


        }

        private bool EsAdmin(Usuario usuario)
        {
            // Según el plan, solo los Administradores pueden gestionar Marcas.
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }





        private void CargarDatosMarca(int marcaId)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            var marca = marcaNegocio.BuscarMarca(marcaId);
            if (marca != null)
            {
                LabelNombreMarca.Text = marca.nombre;
                //  Se asigna el texto del estado desde el código.
                LabelEstadoMarca.Text = marca.estado ? "Activo" : "Inactivo";
            }
            else
            {
                // Si no se encuentra la marca, redirigir.
                Response.Redirect("../Marcas.aspx");
            }
        }



        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                int marcaId = Convert.ToInt32(Request.QueryString["id"]);
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                marcaNegocio.bajaLogica(marcaId);
                Response.Redirect("../Marcas.aspx");
            }
            catch (Exception ex)
            {
                LabelError.Text = "Ocurrió un error al eliminar la marca.";
                LabelError.Visible = true;
                // Opcional: Registrar el error 'ex' para depuración.


                // para ver en la consola.
                Console.WriteLine(ex.Message);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Marcas.aspx");
        }
    }
}
using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static negocio.TipoNegocio;

namespace Front.TiposABM
{
    public partial class TipoAgregar : System.Web.UI.Page
    {

        TipoNegocio tipoNegocio = new TipoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {

            Usuario usuario = Session["usuario"] as Usuario;

            if( usuario == null || !EsAdmin(usuario))
            {
                Response.Redirect("../Login.aspx");
                return;
            }



        }

        private bool EsAdmin(Usuario usuario)
        {
            // Según el plan, solo los Administradores pueden gestionar Tipos.
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }





        // EN: TipoAgregar.aspx.cs

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TextBoxNombre.Text))
                {
                    LabelError.Text = "El campo 'Nombre' es obligatorio.";
                    LabelError.Visible = true;
                    return;
                }

                var tipo = new dominio.Tipos
                {
                    nombre = TextBoxNombre.Text,
                    estado = CheckBoxEstado.Checked
                };

                tipoNegocio.AgregarTipo(tipo);
                Response.Redirect("../Tipos.aspx");
            }
            catch (TipoInactivoException ex)
            {
                // CASO ESPECIAL: El tipo existe pero está inactivo. Lo reactivamos.
                tipoNegocio.ReactivarTipo(ex.TipoExistente.Id);
                Response.Redirect("../Tipos.aspx");
            }
            catch (Exception ex)
            {
                // CASO GENERAL: Otro error (ej: ya existe y está activo).
                LabelErrorTipoExistente.Text = "Error: " + ex.Message;
                LabelErrorTipoExistente.Visible = true;
            }
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            // Redirige a la página de lista de productos
            Response.Redirect("../Tipos.aspx");
        }


    }
}
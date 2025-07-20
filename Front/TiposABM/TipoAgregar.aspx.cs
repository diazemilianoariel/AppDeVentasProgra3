using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

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

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {

            try
            {
                LabelErrorTipoExistente.Text = "";
                LabelError.Text = "";

                if (string.IsNullOrWhiteSpace(TextBoxNombre.Text))
                {
                    LabelError.Text = "El campo 'Nombre' es obligatorio.";
                    LabelError.Visible = true;
                    return;
                }

                string nombreNuevo = TextBoxNombre.Text;
                List<dominio.Tipos> listaDeTipos = tipoNegocio.ListarTipos();

                if (listaDeTipos.Any(t => t.nombre.Equals(nombreNuevo, StringComparison.OrdinalIgnoreCase)))
                {
                    LabelErrorTipoExistente.Text = "El nombre del Tipo ya existe.";
                    LabelErrorTipoExistente.Visible = true;
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
            catch (Exception ex)
            {
                LabelError.Text = "Ocurrió un error al guardar el tipo. Intente nuevamente.";
                LabelError.Visible = true;
                // Opcional: Registrar el error 'ex' para depuración.

            }
        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            // Redirige a la página de lista de productos
            Response.Redirect("../Tipos.aspx");
        }


    }
}
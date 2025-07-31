using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Front.TiposABM
{
    public partial class TipoModificar : System.Web.UI.Page
    {

        TipoNegocio tipoNegocio = new TipoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {

            Usuario usuario = Session["usuario"] as Usuario;
            if(usuario ==null || !EsAdmin(usuario))
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
            var tipo = tipoNegocio.BuscarTipo(tipoId);
            if (tipo != null)
            {
                LabelId.Text = tipo.Id.ToString();
                TextBoxNombre.Text = tipo.nombre;
                CheckBoxEstado.Checked = tipo.estado;
                // Guardamos el nombre original para la validación.
                HiddenFieldNombreOriginal.Value = tipo.nombre;
            }
            else
            {
                Response.Redirect("../Tipos.aspx");
            }
        }


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

                //  Se añade la lógica para validar si el nombre ya existe (si fue modificado).
                if (!TextBoxNombre.Text.Equals(HiddenFieldNombreOriginal.Value, StringComparison.OrdinalIgnoreCase))
                {
                    List<dominio.Tipos> listaDeTipos = tipoNegocio.ListarTipos();
                    if (listaDeTipos.Any(t => t.nombre.Equals(TextBoxNombre.Text, StringComparison.OrdinalIgnoreCase)))
                    {
                        LabelErrorTipoExistente.Text = "El nombre del Tipo ya existe.";
                        LabelErrorTipoExistente.Visible = true;
                        return;
                    }
                }

                var tipo = new dominio.Tipos
                {
                    Id = Convert.ToInt32(LabelId.Text),
                    nombre = TextBoxNombre.Text,
                    estado = CheckBoxEstado.Checked
                };

                tipoNegocio.ActualizarTipo(tipo);
                Response.Redirect("../Tipos.aspx");
            }
            catch (Exception ex)
            {
                LabelError.Text = "Ocurrió un error al guardar el tipo.";
                LabelError.Visible = true;
                // Opcional: Registrar el error 'ex' para depuración.
            }
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Tipos.aspx");
        }


    }
}
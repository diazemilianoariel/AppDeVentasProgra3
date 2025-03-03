using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace Front
{
    public partial class Tipo : System.Web.UI.Page
    {
        protected TextBox TextBoxIdTipo;
        protected TextBox TextBoxNuevoTipo;
        protected Panel editSection;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["cliente"] == null || !EsAdministradorOSoporte((Cliente)Session["cliente"]))
            {
                Response.Redirect("Login.aspx");
                return;
            }


            if (!IsPostBack)
            {
                CargarTipos();
            }
        }

        private bool EsAdministradorOSoporte(Cliente cliente)
        {
            return cliente.nombrePerfil == "Administrador" || cliente.nombrePerfil == "Soporte" || cliente.nombrePerfil == "Vendedor";
        }

        private void CargarTipos()
        {
            TipoNegocio negocio = new TipoNegocio();
            GridViewTipos.DataSource = negocio.ListarTipos();
            GridViewTipos.DataBind();
        }

        protected void btnAgregarTipo_Click(object sender, EventArgs e)
        {

            TipoNegocio negocio = new TipoNegocio();
            Tipos tipo = new Tipos();

            if (TextBoxTipo != null && !string.IsNullOrEmpty(TextBoxTipo.Text))
            {

                tipo.nombre = TextBoxTipo.Text;
                if (!negocio.buscarTipoNombre(tipo.nombre))
                {
                    tipo.nombre = TextBoxTipo.Text;
                    negocio.AgregarTipo(tipo);
                }
                else
                {
                    negocio.altaLogica(tipo.nombre);
                }
                CargarTipos();
                TextBoxTipo.Text = string.Empty; // Limpiar el TextBox después de agregar
            }



        }

        protected void GridViewTipos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = GridViewTipos.Rows[index];

                if (fila != null && TextBoxTipo != null && !string.IsNullOrEmpty(TextBoxTipo.Text))
                {
                    int idTipo = Convert.ToInt32(fila.Cells[0].Text);
                    string nuevoNombre = TextBoxTipo.Text;

                    TipoNegocio negocio = new TipoNegocio();
                    Tipos tipo = new Tipos
                    {
                        id = idTipo,
                        nombre = nuevoNombre
                    };

                    negocio.ActualizarTipo(tipo);
                    CargarTipos();

                    // Limpiar el TextBoxTipo después de actualizar
                    TextBoxTipo.Text = string.Empty;
                }
                else
                {
                    // Mostrar ventana emergente
                    string script = "alert('por favor proporcione un nombre y luego precione editar.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                    return;

                }
            }
            else if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = GridViewTipos.Rows[index];

                if (fila != null)
                {
                    int idTipo = Convert.ToInt32(fila.Cells[0].Text);

                    TipoNegocio negocio = new TipoNegocio();
                    negocio.bajaLogica(idTipo);
                    CargarTipos();

                    // Vaciar el TextBoxNuevoTipo si se elimina un tipo
                    if (TextBoxNuevoTipo != null)
                    {
                        TextBoxNuevoTipo.Text = string.Empty;
                    }
                }
            }
        }

        protected void btnActualizarTipo_Click(object sender, EventArgs e)
        {
            TipoNegocio negocio = new TipoNegocio();
            Tipos tipo = new Tipos();

            if (TextBoxIdTipo != null && TextBoxNuevoTipo != null)
            {
                tipo.id = Convert.ToInt32(TextBoxIdTipo.Text);
                tipo.nombre = TextBoxNuevoTipo.Text;

                negocio.ActualizarTipo(tipo);
                CargarTipos();

                // Ocultar la sección de edición
                if (editSection != null)
                {
                    editSection.Style["display"] = "none";
                }
            }
        }
    }
}

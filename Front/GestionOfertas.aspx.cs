using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Front
{
    public partial class GestionOfertas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // FALTANTE: Agregar tu validación de seguridad para que solo entre el Admin.
            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            OfertaNegocio negocio = new OfertaNegocio();
            gvProductosOfertas.DataSource = negocio.ListarTodosParaGestion();
            gvProductosOfertas.DataBind();
        }

        protected void btnGuardarOfertas_Click(object sender, EventArgs e)
        {
            OfertaNegocio negocio = new OfertaNegocio();
            List<Oferta> ofertasActualizadas = new List<Oferta>();

            foreach (GridViewRow row in gvProductosOfertas.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkEnOferta");
                if (chk.Checked)
                {
                    Oferta oferta = new Oferta();
                    oferta.IdProducto = (int)gvProductosOfertas.DataKeys[row.RowIndex].Value;

                    TextBox txtPrecio = (TextBox)row.FindControl("txtPrecioOferta");
                    if (decimal.TryParse(txtPrecio.Text, out decimal precioOferta) && precioOferta > 0)
                        oferta.PrecioOferta = precioOferta;

                    TextBox txtInicio = (TextBox)row.FindControl("txtFechaInicio");
                    if (DateTime.TryParse(txtInicio.Text, out DateTime fechaInicio))
                        oferta.FechaInicio = fechaInicio;

                    TextBox txtFin = (TextBox)row.FindControl("txtFechaFin");
                    if (DateTime.TryParse(txtFin.Text, out DateTime fechaFin))
                        oferta.FechaFin = fechaFin;

                    ofertasActualizadas.Add(oferta);
                }
            }

            try
            {
                negocio.GuardarOfertas(ofertasActualizadas);
                lblMensaje.Text = "Ofertas actualizadas correctamente.";
                lblMensaje.Visible = true;
                CargarGrilla(); // Recargamos para ver los cambios.
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar las ofertas: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;


namespace Front
{
    public partial class Marcas : System.Web.UI.Page
    {
        //protected TextBox TextBoxMarca;
        protected TextBox TextBoxIdMarca;
        protected TextBox TextBoxNuevaMarca;
        protected Panel editSection;
      //  protected GridView GridViewMarcas;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["cliente"] == null || !EsAdministradorOSoporte((Cliente)Session["cliente"]))
            {
                Response.Redirect("Login.aspx");
                return;
            }


            if (!IsPostBack)
            {
                CargarMarcas();
            }
        }


        private bool EsAdministradorOSoporte(Cliente cliente)
        {
            return cliente.nombrePerfil == "Administrador" || cliente.nombrePerfil == "Soporte" || cliente.nombrePerfil == "Vendedor";
        }

        private void CargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            GridViewMarcas.DataSource = negocio.ListarMarcas();
            GridViewMarcas.DataBind();
        }

        protected void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            Marca marca = new Marca();

            if (TextBoxMarca != null && !string.IsNullOrEmpty(TextBoxMarca.Text))
            {

                marca.nombre = TextBoxMarca.Text;

                if (!negocio.BuscarMarcaNombre(marca.nombre))
                {
               
                    marca.nombre = TextBoxMarca.Text;
                    negocio.AgregarMarca(marca);
                    
                }
                else
                {
                    // tiene que dar de alta la marca cambiando el estado a  1 y  listarla nuevamente
                    negocio.altaLogica(marca.nombre);

                }
                    CargarMarcas();
                    TextBoxMarca.Text = string.Empty; // Limpiar el TextBox después de agregar

              
            }
        }

        protected void GridViewMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = GridViewMarcas.Rows[index];

                if (fila != null && TextBoxMarca != null && !string.IsNullOrEmpty(TextBoxMarca.Text))
                {
                    int idMarca = Convert.ToInt32(fila.Cells[0].Text);
                    string nuevoNombre = TextBoxMarca.Text;

                    MarcaNegocio negocio = new MarcaNegocio();
                    Marca marca = new Marca
                    {
                        id = idMarca,
                        nombre = nuevoNombre
                    };

                    negocio.ActualizarMarca(marca);
                    CargarMarcas();

                    // Limpiar el TextBoxMarca después de actualizar
                    TextBoxMarca.Text = string.Empty;
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
                GridViewRow fila = GridViewMarcas.Rows[index];

                if (fila != null)
                {
                    int idMarca = Convert.ToInt32(fila.Cells[0].Text);

                    MarcaNegocio negocio = new MarcaNegocio();
                    negocio.bajaLogica(idMarca);
                    CargarMarcas();

                    // Vaciar el TextBoxNuevaMarca si se elimina una marca
                    if (TextBoxNuevaMarca != null)
                    {
                        TextBoxNuevaMarca.Text = string.Empty;
                    }
                }
            }
        }

        protected void btnActualizarMarca_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            Marca marca = new Marca();

            if (TextBoxIdMarca != null && TextBoxNuevaMarca != null)
            {
                marca.id = Convert.ToInt32(TextBoxIdMarca.Text);
                marca.nombre = TextBoxNuevaMarca.Text;

                negocio.ActualizarMarca(marca);
                CargarMarcas();

                // Ocultar la sección de edición
                if (editSection != null)
                {
                    editSection.Style["display"] = "none";
                }
            }
        }


    }
}

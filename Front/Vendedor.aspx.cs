using System;
using System.Web.UI;
using dominio;
using negocio;

namespace Front
{
    public partial class Vendedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicializar la página si es necesario
            }
        }

        protected void ButtonCargarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar los campos
                if (string.IsNullOrEmpty(TextBoxCliente.Text) ||
                    string.IsNullOrEmpty(TextBoxProducto.Text) ||
                    string.IsNullOrEmpty(TextBoxCantidad.Text) ||
                    string.IsNullOrEmpty(TextBoxFecha.Text))
                {
                    MostrarMensaje("Todos los campos son obligatorios.");
                    return;
                }

                // Crear una nueva compra
                Compra compra = new Compra
                {
                    Cliente = TextBoxCliente.Text,
                    Producto = TextBoxProducto.Text,
                    Cantidad = Convert.ToInt32(TextBoxCantidad.Text),
                    Fecha = Convert.ToDateTime(TextBoxFecha.Text)
                };

                // Guardar la compra en la base de datos
                CompraNegocio negocio = new CompraNegocio();
                negocio.CargarCompra(compra);

                // Mostrar mensaje de éxito
                MostrarMensaje("Compra cargada exitosamente.", false);
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error
                MostrarMensaje("Ocurrió un error: " + ex.Message);
            }
        }

        private void MostrarMensaje(string mensaje, bool esError = true)
        {
            LabelMensaje.Text = mensaje;
            LabelMensaje.ForeColor = esError ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            LabelMensaje.Visible = true;
        }
    }
}

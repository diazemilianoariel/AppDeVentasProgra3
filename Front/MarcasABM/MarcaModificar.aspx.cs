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
    public partial class MarcaModificar : System.Web.UI.Page
    {
        MarcaNegocio marcaNegocio = new MarcaNegocio();
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
                    // Manejar el caso donde no se proporciona un ID.
                    // Redirigimos a la lista de marcas si no hay ID.
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
            // Implementa la lógica para obtener los datos del producto de la base de datos
            var marca = new MarcaNegocio().BuscarMarca(marcaId);
            if (marca != null)
            {
                LabelId.Text = marca.Id.ToString();
                TextBoxNombre.Text = marca.nombre;
                CheckBoxEstado.Checked = marca.estado;
            }
            else
            {
                Response.Redirect("../Marcas.aspx");
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

                //  lógica para validar si el nombre ya existe (si fue modificado)
                if (!TextBoxNombre.Text.Equals(HiddenFieldNombreOriginal.Value, StringComparison.OrdinalIgnoreCase))
                {
                    List<Marca> listaDeMarcas = marcaNegocio.ListarMarcas();
                    if (listaDeMarcas.Any(m => m.nombre.Equals(TextBoxNombre.Text, StringComparison.OrdinalIgnoreCase)))
                    {
                        LabelErrorMarcaExistente.Text = "El nombre de la Marca ya existe.";
                        LabelErrorMarcaExistente.Visible = true;
                        return;
                    }
                }

                Marca marca = new Marca
                {
                    Id = Convert.ToInt32(LabelId.Text),
                    nombre = TextBoxNombre.Text,
                    estado = CheckBoxEstado.Checked
                };

                marcaNegocio.ActualizarMarca(marca);
                Response.Redirect("../Marcas.aspx");
            }
            catch (Exception ex)
            {
                LabelError.Text = "Ocurrió un error al guardar la marca.";
                LabelError.Visible = true;

                
                Console.WriteLine(ex.Message); // en la consola del servidor
            }
        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("../Marcas.aspx");
        }


    }
}
using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static negocio.MarcaNegocio;

namespace Front.MarcasABM
{
    public partial class MarcaAgregar : System.Web.UI.Page
    {



        MarcaNegocio marcaNegocio = new MarcaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {


            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null || !EsAdmin(usuario))
            {
                // CORRECCIÓN 2: La ruta de redirección debe subir un nivel.
                Response.Redirect("../Login.aspx");
                return;
            }


            if (!IsPostBack)
            {


          



            }


        }


        private bool EsAdmin(Usuario usuario)
        {
            // Según el plan, solo los Administradores pueden gestionar Marcas.
            return usuario.Perfil != null && usuario.Perfil.Id == (int)TipoPerfil.Administrador;
        }


        // EN: MarcaAgregar.aspx.cs

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

                var marca = new Marca
                {
                    nombre = TextBoxNombre.Text,
                    estado = CheckBoxEstado.Checked
                };

                marcaNegocio.AgregarMarca(marca);
                Response.Redirect("../Marcas.aspx");
            }
            catch (MarcaInactivaException ex)
            {
                // CASO ESPECIAL: La marca existe pero está inactiva. La reactivamos.
                marcaNegocio.ReactivarMarca(ex.MarcaExistente.Id);
                Response.Redirect("../Marcas.aspx");
            }
            catch (Exception ex)
            {
                // CASO GENERAL: Otro error (ej: ya existe y está activa).
                LabelError.Text = "Error: " + ex.Message;
                LabelError.Visible = true;
            }
        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("../Marcas.aspx");


        }
    }
}
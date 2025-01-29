using dominio;

using negocio;
using System;
using System.Web.UI.WebControls;




namespace Front
{
    public partial class Perfiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



            if (Session["cliente"] == null || !IDPerfilValido())
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarPerfiles();
            }

        }

        private void CargarPerfiles()
        {
            PerfilesNegocio perfilesNegocio = new PerfilesNegocio();
            GridViewPerfiles.DataSource = perfilesNegocio.ListarPerfiles();
            GridViewPerfiles.DataBind();
        }

        private bool IDPerfilValido()
        {
            Cliente cliente = (Cliente)Session["cliente"];
            return cliente.idPerfil == 2 || cliente.idPerfil == 4;
        }

        protected void MostrarMensaje(string mensaje, bool esError = true)
        {
            LabelMensaje.Text = mensaje;
            LabelMensaje.ForeColor = esError ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            LabelMensaje.Visible = true;
        }


        protected void BtnAgregar_Click(object sender, EventArgs e)
        {

            try
            {

                if (ValidarCampos())
                {
                    PerfilesNegocio negocio = new PerfilesNegocio();
                    Perfil perfil = new Perfil
                    {
                        Nombre = TxtNombre.Text
                    };

                    // Verificar si el perfil ya existe
                    if (negocio.ExistePerfil(perfil.Nombre))
                    {
                        Perfil PerfilExistente = negocio.ObtenerPerfilPoreNombre(perfil.Nombre);

                        if (PerfilExistente.Estado)
                        {
                            MostrarMensaje("El perfil ya existe. No se puede Agregar Perfil con El Mismo Nombre");



                        }
                        else
                        {



                            // mostrar mensajes de confirmacion 
                            lblConfirmacion.Text = "El Perfil ya existe pero está inactivo. ¿Desea reactivarlo?";
                            lblConfirmacion.Visible = true;
                            btnConfirmarReactivacion.CommandArgument = PerfilExistente.Id.ToString();
                            btnConfirmarReactivacion.Visible = true;



                        }
                    }




                    else
                    {

                        negocio.AgregarPerfil(perfil);
                        // el mensaje de Perfil Agregado  correctamente debe desaparecer luego de unos segundos
                        MostrarMensaje("Perfil agregado correctamente", false);

                        CargarPerfiles();
                        LimpiarCampos();
                    }
                }
                else
                {
                    MostrarMensaje("Error al agregar el perfil.");
                }
            }
            catch (Exception ex)
            {
                Response.Write("Ocurrió un error: " + ex.Message);

            }
        }
        

        // modificar 
        protected void BtnModificar_Click(object sender, EventArgs e)
        {


            if(string.IsNullOrEmpty(TxtNombre.Text))
            {
                MostrarMensaje("El campo nombre es obligatorio.");
                return;
            }


            if (int.TryParse(HiddenFieldPerfilId.Value, out int perfilId))
            {
                Perfil perfil = new Perfil
                {
                    Id = perfilId,
                    Nombre = TxtNombre.Text,
                    Estado = CheckBoxEstado.Checked,
                };
                PerfilesNegocio perfilesNegocio = new PerfilesNegocio();
                perfilesNegocio.ModificarPerfil(perfil);
                MostrarMensaje("Perfil modificado exitosamente.", false);
                CargarPerfiles();
                LimpiarCampos();
            }
            else
            {
                MostrarMensaje("Error al modificar el perfil.");
            }
           


        }

        // eliminar

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            // aca se debe seleccionar el perfil a eliminar y llamar al metodo bajaLogicaPerfil
            if (int.TryParse(HiddenFieldPerfilId.Value, out int perfilId))
            {
                PerfilesNegocio perfilesNegocio = new PerfilesNegocio();
                Perfil perfil = new Perfil
                {
                    Id = perfilId
                };

                try
                {
                    perfilesNegocio.EliminarPerfil(perfil);

                    MostrarMensaje("Perfil eliminado exitosamente.", false);
                    CargarPerfiles();
                    LimpiarCampos();
                }
                catch(Exception ex)
                {
                    MostrarMensaje("Ocurrió un error: " + ex.Message);
                }








            }
            else
            {
                MostrarMensaje("Error al eliminar el perfil.");
            }
        }

        //cancelar
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        // evento boton   seleccionar perfil 
        protected void GridViewPerfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridViewPerfiles.SelectedRow;
            TxtNombre.Text = row.Cells[2].Text;
        }

        protected void GridViewClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                PerfilesNegocio perfilesNegocio = new PerfilesNegocio();
                Perfil perfil = perfilesNegocio.ObtenerPerfil(id);
                TxtNombre.Text = perfil.Nombre;



            }
            else
            {
                MostrarMensaje("Error al seleccionar el perfil.");
            }
        }

        public  void SeleccionarPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(((Button)sender).CommandArgument);
                PerfilesNegocio perfilesNegocio = new PerfilesNegocio();
                Perfil perfil = perfilesNegocio.ObtenerPerfil(id);
                TxtNombre.Text = perfil.Nombre;
                CheckBoxEstado.Checked = perfil.Estado;
                HiddenFieldPerfilId.Value = perfil.Id.ToString();
            }
            catch (FormatException)
            {
                MostrarMensaje("El formato del ID del perfil no es correcto.");
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrió un error: " + ex.Message);
            }
        }





        protected void LimpiarCampos()
        {
            TxtNombre.Text = string.Empty;

        }


        protected void btnConfirmarReactivacion_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(((Button)sender).CommandArgument);
            PerfilesNegocio perfilesNegocio = new PerfilesNegocio();
            perfilesNegocio.AltaLogicaPerfiles(new Perfil { Id = id });
            MostrarMensaje("Perfil reactivado exitosamente.", false);
            CargarPerfiles();
            LimpiarCampos();
            lblConfirmacion.Visible = false;
            btnConfirmarReactivacion.Visible = false;
        }


        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(TxtNombre.Text))
            {
                MostrarMensaje("El campo nombre es obligatorio");
                return false;
            }
            return true;



        }
    }
}
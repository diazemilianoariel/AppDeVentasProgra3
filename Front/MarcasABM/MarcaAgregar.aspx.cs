﻿using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

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



        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                LabelErrorMarcaExistente.Text = "";
                LabelError.Text = "";

                if (string.IsNullOrWhiteSpace(TextBoxNombre.Text))
                {
                    LabelError.Text = "El campo 'Nombre' es obligatorio.";
                    LabelError.Visible = true;
                    return;
                }

                string nombreNuevo = TextBoxNombre.Text;
                List<Marca> listaDeMarcas = marcaNegocio.ListarMarcas();

                if (listaDeMarcas.Any(m => m.nombre.Equals(nombreNuevo, StringComparison.OrdinalIgnoreCase)))
                {
                    LabelErrorMarcaExistente.Text = "El nombre de la Marca ya existe.";
                    LabelErrorMarcaExistente.Visible = true;
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
            catch (Exception ex)
            {
                LabelError.Text = "Ocurrió un error al guardar la marca. Intente nuevamente.";
                LabelError.Visible = true;

                Console.WriteLine(ex.Message); // este  mensaje se puede ver en la consola
            }

        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("../Marcas.aspx");


        }
    }
}
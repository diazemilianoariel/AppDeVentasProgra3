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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {




                // Verificar si el usuario tiene permisos para acceder a esta página
                if (Session["cliente"] == null || !EsAdministradorOSoporte((Cliente)Session["cliente"]))
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
            }


        }

        private bool EsAdministradorOSoporte(Cliente cliente)
        {
            return cliente.nombrePerfil == "Administrador" || cliente.nombrePerfil == "Soporte" || cliente.nombrePerfil == "Vendedor";
        }


        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            
            TipoNegocio tipoNegocio = new TipoNegocio();


            var Tipo = new Tipos
            {
                nombre = TextBoxNombre.Text,
                estado = CheckBoxEstado.Checked
            };

            // Implementa la lógica para agregar el producto en la base de datos
            tipoNegocio.AgregarTipo(Tipo);

            // Redirige a la página de lista de productos
            Response.Redirect("../Tipos.aspx");
        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            // Redirige a la página de lista de productos
            Response.Redirect("../Tipos.aspx");
        }


    }
}
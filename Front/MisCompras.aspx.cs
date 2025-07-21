using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace Front
{
    public partial class MisCompras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                if (!IsPostBack)
                {
                    Usuario usuario = Session["usuario"] as Usuario;

                    // ¿Existe el usuario?
                    if (usuario == null)
                    {
                        Response.Redirect("Login.aspx");
                        return;
                    }

                    //  ¿El usuario tiene un perfil asignado?
                    if (usuario.Perfil == null)
                    {
                        Session["error"] = new Exception("El usuario '" + usuario.Email + "' no tiene un perfil válido asignado.");
                        Response.Redirect("Error.aspx");
                        return;
                    }

                    // ¿El perfil es el correcto?
                    bool esCliente = usuario.Perfil.Id == (int)TipoPerfil.Cliente;
                    bool esAdmin = usuario.Perfil.Id == (int)TipoPerfil.Administrador;

                    if (!esCliente && !esAdmin)
                    {
                        Session["error"] = new Exception("Acceso denegado. El perfil actual no tiene permiso.");
                        Response.Redirect("Error.aspx");
                        return;
                    }

                    // Si todo está bien, cargamos las compras.
                    CargarCompras(usuario.Id);
                }
            }
            catch (Exception ex)
            {
                Session["error"] = ex;
                Response.Redirect("Error.aspx");
            }
        }

        
        private void CargarCompras(int idUsuario)
        {
            FacturaNegocio negocio = new FacturaNegocio();
            var listaCompleta = negocio.ListarComprasPorCliente(idUsuario);

            if (listaCompleta != null && listaCompleta.Any())
            {
                // Agrupamos las compras por Año y Mes usando LINQ
                var todosLosGrupos = listaCompleta
                .Where(c => c.Fecha != DateTime.MinValue && c.Fecha != null)
                .OrderByDescending(c => c.Fecha)
                .GroupBy(c => new { c.Fecha.Year, c.Fecha.Month })
                .Select(g => new {
                    Periodo = new DateTime(g.Key.Year, g.Key.Month, 1),
                    ComprasDelPeriodo = g.ToList()
                });



                var gruposParaMostrar = todosLosGrupos.Take(this.MesesPorPagina).ToList();


                // Asignamos  datos al Repeater principal
                rptGruposCompras.DataSource = gruposParaMostrar;
                rptGruposCompras.DataBind();
            }
            else
            {
                //  mensaje.
                pnlNoHayCompras.Visible = true;
            }
        }

        // MÉTODO DE AYUDA PARA LOS BADGES DE ESTADO 
        public string GetStatusBadgeClass(object estadoObj)
        {
            if (estadoObj == null || estadoObj == DBNull.Value)
            {
                return "badge badge-secondary"; // Color neutral para estados desconocidos
            }
            string estado = estadoObj.ToString();
            switch (estado)
            {
                case "Aprobado":
                    return "badge badge-success";
                case "Cancelado":
                    return "badge badge-danger";
                case "Pendiente":
                    return "badge badge-warning";
                default:
                    return "badge badge-secondary";
            }
        }

        // MANEJADOR DE EVENTOS PARA EL BOTÓN "VER DETALLES" 
        protected void rptCompras_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalles")
            {
                string idVenta = e.CommandArgument.ToString();
                Response.Redirect("DetalleCompra.aspx?id=" + idVenta, false);
            }
        }



        private int MesesPorPagina
        {
            get { return ViewState["MesesPorPagina"] != null ? (int)ViewState["MesesPorPagina"] : 3; } // Mostramos 3 meses inicialmente
            set { ViewState["MesesPorPagina"] = value; }
        }

        protected void btnCargarMas_Click(object sender, EventArgs e)
        {
            // Aumentamos la cantidad de meses a mostrar
            this.MesesPorPagina += 3; // Cargamos los siguientes 3 meses

            // Volvemos a cargar los datos
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario != null)
            {
                CargarCompras(usuario.Id);
            }
        }



    }
}
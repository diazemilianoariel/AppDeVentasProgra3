using System;
using System.Web.UI.WebControls;
using dominio;
using negocio;


namespace Front
{
    public partial class Compras : System.Web.UI.Page
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
                // Cargar proveedores y productos en los DropDownList
                CargarProveedores();
                CargarProductos();
            }



        }



        public bool IDPerfilValido()
        {
            Cliente cliente = (Cliente)Session["cliente"];
            return cliente.idPerfil == 2;
        }






        private void CargarProveedores()
        {

            try
            {
                ProveedoresNegocio negocio = new ProveedoresNegocio();
                var proveedores = negocio.ListarProveedores();
                if (proveedores != null && proveedores.Count > 0)
                {
                    DropDownListProveedor.DataSource = proveedores;
                    DropDownListProveedor.DataBind(); // Ajusta según el nombre del campo
                    DropDownListProveedor.DataValueField = "nombre"; // Ajusta según el nombre del campo
                    DropDownListProveedor.DataBind();
                    DropDownListProveedor.Items.Insert(0, new ListItem("Seleccione un proveedor", "0"));

                }
                else
                {
                    DropDownListProveedor.Items.Insert(0, new ListItem("No hay proveedores disponibles", "0"));

                }

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");




            }
        }

        private void CargarProductos()
        {
            try
            {
                ProductoNegocio negocio = new ProductoNegocio();
                var productos = negocio.ListarProductos();
                if (productos != null && productos.Count > 0)
                {
                    DropDownListProducto.DataSource = productos;
                    DropDownListProducto.DataBind(); 
                    DropDownListProducto.DataValueField = "nombre"; 
                    DropDownListProducto.DataBind();
                    DropDownListProducto.Items.Insert(0, new ListItem("Seleccione un producto", "0"));
                }
                else
                {
                    DropDownListProducto.Items.Insert(0, new ListItem("No hay productos disponibles", "0"));

                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
          
           


        }
        protected void btnRealizarCompra_Click(object sender, EventArgs e)
        {
            // Lógica para manejar la compra
        }







    }
}
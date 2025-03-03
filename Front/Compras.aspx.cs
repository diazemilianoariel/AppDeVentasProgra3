using System;
using System.Web.UI;
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
                    DropDownListProveedor.DataBind();
                    DropDownListProveedor.DataTextField = "nombre"; // Mostrar el nombre del proveedor

                    DropDownListProveedor.DataValueField = "id";
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
                    DropDownListProducto.DataTextField = "nombre";
                    DropDownListProducto.DataValueField = "id";
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
            // se cargan los datos del formulario y se los manda al metodo RealizarCompra que va a  hacer los insert correspondientes.



            CompraNegocio negocio = new CompraNegocio();
            Compra compra = new Compra();

            ProductoNegocio negocioProducto = new ProductoNegocio();
            Producto producto = new Producto();



            int productoId;
            if (int.TryParse(DropDownListProducto.SelectedValue, out productoId))
            {
                compra.IdProveedor = int.Parse(DropDownListProveedor.SelectedValue);
                compra.IdProducto = DropDownListProducto.SelectedValue;

                if (string.IsNullOrEmpty(TextBoxCantidad.Text))
                {
                    // Mostrar ventana emergente
                    string script = "alert('porfavor complete Todos los campos.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                    return;
                }


                compra.Cantidad = int.Parse(TextBoxCantidad.Text);

                compra.Fecha = DateTime.Parse(TextBoxFecha.Text);

                producto = negocioProducto.ObtenerProducto(productoId);

                // Calcular el precio de compra
                compra.PrecioCompra = producto.precio - (producto.precio * producto.margenGanancia / 100);

                // Calcular el total de la compra
                compra.Total = compra.Cantidad * compra.PrecioCompra;



                negocio.InsertarCompra(compra);



            }
            else
            {
                // Manejo de error si el IdProducto no es un entero válido
                // Mostrar mensaje de error al usuario
                MostrarMensaje("El ID del producto no es válido.", true);
            }




        }


        private void MostrarMensaje(string mensaje, bool esError = true)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "alert alert-danger" : "alert alert-success";
            lblMensaje.Visible = true;
        }










    }
}
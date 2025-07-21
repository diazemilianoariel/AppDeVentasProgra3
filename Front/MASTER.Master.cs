using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Front
{
    public partial class MASTER : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // Esta lógica ahora se ejecuta en cada carga de página (PostBack o no)
            // para que el contador esté siempre sincronizado.

            // Obtenemos la lista de productos de la sesión.
            var carrito = Session["Carrito"] as List<Producto>;
            int totalProductos = 0;

            if (carrito != null)
            {
                // Sumamos la propiedad "Cantidad" de cada producto en la lista.
                // Asegúrate de que tu clase Producto tenga una propiedad 'Cantidad' para el carrito.
                // Si no la tiene y solo guardas un producto por línea, puedes usar 'carrito.Count'.
                totalProductos = carrito.Sum(p => p.Cantidad);
            }

            // Actualizamos el contador en el HTML.
            ActualizarCarrito.InnerText = totalProductos.ToString();

            // El resto de tu lógica para el saludo del usuario.
            // Ojo: Usas Session["cliente"] y Session["usuario"], asegúrate de usar el correcto.
            // Voy a usar "usuario" como en tu archivo .aspx
            var usuario = Session["usuario"] as Usuario;
            if (usuario != null)
            {
                // En tu .aspx usas DataBinding (<%#), así que no necesitarías esta línea si funciona.
                // Pero si no, esta es la forma correcta desde el code-behind.
                lblNombre.Text = "Bienvenido " + usuario.Nombre;
            }

        }

        public void ActualizarContadorCarrito(int totalProductos = 0)
        {
            ActualizarCarrito.InnerText = totalProductos.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "ActualizarContadorCarrito", $"actualizarContadorCarrito({totalProductos});", true);
        }


        public string CartCountClientID
        {
            get { return ActualizarCarrito.ClientID; }
        }


        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            // Eliminar la sesión del usuario
            Session["cliente"] = null;
            Session.Abandon();

            // Redirigir a la página de inicio de sesión
            Response.Redirect("Login.aspx");
        }
    }
}
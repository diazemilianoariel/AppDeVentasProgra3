using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class Proveedor
    {
        // será un proveedor de productos de una tienda de venta: 
        // nombre, dirección, teléfono, email, productos que provee
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public List<Producto> Productos { get; set; }

        // constructor
        public Proveedor() { }

        // constructor con parámetros
        public Proveedor(string nombre, string direccion, string telefono, string email)
        {
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
            Productos = new List<Producto>();
        }
    }
}
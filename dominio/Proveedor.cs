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
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public bool estado { get; set; }


    }
}
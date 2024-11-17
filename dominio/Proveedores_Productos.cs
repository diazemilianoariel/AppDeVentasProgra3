using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class Proveedores_Productos
    {
        public int IdProveedor { get; set; }
        public int IdProducto { get; set; }
        public int Precio { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
    }
}
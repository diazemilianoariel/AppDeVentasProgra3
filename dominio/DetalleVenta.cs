using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class DetalleVenta
    {
        int id { get; set; }
        int idVenta { get; set; }
        int idProducto { get; set; }
        int cantidad { get; set; }

        decimal precioVenta { get; set; }

        public bool estado { get; set; }
    }
}
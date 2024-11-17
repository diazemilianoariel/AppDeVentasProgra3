using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class DetalleCompra
    {
        int id { get; set; }
        int idCompra { get; set; }

        int idProducto { get; set; }
        int cantidad { get; set; }

        decimal precioCompra { get; set; }
    }
}
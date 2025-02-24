using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class Compra
    {
        // esta clase registrará compras a proveedores:
        public int IdCompra { get; set; }




        public int IdProveedor { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        // cliente 
        public string Cliente { get; set; }

        // producto
        public string IdProducto { get; set; }

        // cantidad
        public int Cantidad { get; set; }

        public decimal PrecioCompra { get; set; }


    }
}
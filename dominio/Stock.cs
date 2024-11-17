using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class Stock
    {
        int idProducto { get; set; }

        int cantidad { get; set; }

        int stockMinimo { get; set; }

        DateTime Fecha { get; set; }
    }
}
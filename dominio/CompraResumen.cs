using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class CompraResumen
    {
       
            public int IdVenta { get; set; }
            public decimal TotalFactura { get; set; }
            public DateTime Fecha { get; set; }
            public string Estado { get; set; }
        



    }
}
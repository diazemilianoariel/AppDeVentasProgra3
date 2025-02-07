using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class Factura
    {


        // esta clase registrará las facturas de las ventas:
        public int Id { get; set; } // auutonumerico en la base de atos 
        public int IdVenta { get; set; }
        public decimal TotalFactura{ get; set; }

        public decimal SubTotalFactura { get; set; }

        public DateTime Fecha { get; set; }




    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class Factura
    {
        // esta clase registrará las facturas de las ventas:
        public int IdFactura { get; set; }
        public int IdVenta { get; set; }
        public int idCliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalFacutra{ get; set; }
        public decimal subTotalFactura { get; set; }
        public decimal Iva { get; set; }
        public decimal Descuento { get; set; }
        

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class Oferta
    {
        public int IdProducto { get; set; }

        // El '?' permite que el precio sea nulo, por si solo querés
        // marcar un producto en oferta sin cambiarle el precio.
        public decimal? PrecioOferta { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }
    }
}
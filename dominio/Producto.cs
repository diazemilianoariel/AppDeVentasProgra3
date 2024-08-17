using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class Producto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public string imagen { get; set; }
        public int stock { get; set; }
        public string marca { get; set; }
        public string tipo { get; set; }
        public string categoria { get; set; }
        public string proveedor { get; set; }
        public string estado { get; set; }
    }
}
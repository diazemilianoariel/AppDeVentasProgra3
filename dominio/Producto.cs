using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace dominio
{
    public class Producto
    {
        public int id { get; set; }
        // validar con data anotation  que no supere los 50 caracteres
        [StringLength(50)]
        public string nombre { get; set; }
        public string descripcion { get; set; }

        // que no admita numeros negativos y que tenga 2 decimales
        [Range(0, 9999999999999999.99)]
        public decimal precio { get; set; }

        public string Imagen { get; set; }
        public int stock { get; set; }
        public string marca { get; set; }
        public string tipo { get; set; }
        public string categoria { get; set; }
        public string proveedor { get; set; }
        public bool estado { get; set; }


    }
}
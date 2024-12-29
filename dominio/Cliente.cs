using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class Cliente
    {
        // será un cliente para un aplicacion de venta:
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public string clave { get; set; }

        public int idPerfil { get; set; }

        public string nombrePerfil { get; set; }

        public bool estado { get; set; }

    }
}
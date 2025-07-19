using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class Usuario
    {
        
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string clave { get; set; }

        public Perfil Perfil { get; set; }

        public bool estado { get; set; }

        public Usuario()
        {
            Perfil = new Perfil();
        }

    }
}
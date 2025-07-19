using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{


    public enum TipoPerfil
    {
        Cliente = 1,
        Administrador = 2,
        Vendedor = 3,
        soporte = 4,
    }


    public class Perfil
    {
        




        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
    }

   

}
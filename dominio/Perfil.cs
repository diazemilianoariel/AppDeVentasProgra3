using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class Perfil
    {
        // esta clase registrará los perfiles tanto de administradores como de vendedores, tipo de perfil Enum:

        public enum TipoPerfil
        {
            Administrador,
            Vendedor
        }
        public int IdPerfil { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
    }

}
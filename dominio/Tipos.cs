using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace dominio
{
    public class Tipos
    {
       public int Id { get; set; }
        [StringLength(50)]
        public string nombre { get; set; }

        public bool estado { get; set; }
    }
}
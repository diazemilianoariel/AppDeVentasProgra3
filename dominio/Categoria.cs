using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace dominio

{
    public class Categoria
    {// id y nombre 
        public int id { get; set; }
        [StringLength(50)]
        public string nombre { get; set; }
    }
}
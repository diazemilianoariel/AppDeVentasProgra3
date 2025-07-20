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
    {// id y nombre y estado
        public int Id { get; set; }
        [StringLength(50)]
        public string nombre { get; set; }
        public bool estado { get; set; }
    }
}
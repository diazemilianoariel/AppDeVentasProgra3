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

        public decimal SubTotal => precio * Cantidad;

        public decimal SubTotalEnFactura
        {
            get { return precio * Cantidad; }
            set { /* No hacer nada, solo para evitar errores de solo lectura */ }
        }

        public string Imagen { get; set; }
        public int stock { get; set; }  // propiedad para que el usuario  va a ingresar la cantidad de productos que tiene en stock

        public int Cantidad { get; set; }  // propiedad para que el cliente eliga cuantos va a comprar

        public string marca { get; set; }
        public string tipo { get; set; }
        public string categoria { get; set; }
        public string proveedor { get; set; }  // mmmmm ?

        public decimal margenGanancia { get; set; }
        public bool estado { get; set; }






        /*[id] [int] IDENTITY(1,1) NOT NULL,
	    [nombre] [nvarchar](50) NULL,
	    [descripcion] [nvarchar](100) NULL,
	    [precio] [decimal](10, 2) NULL,
	    [imagen] [nvarchar](255) NULL,
	    [idMarca] [int] NULL,
	    [idTipo] [int] NULL,
	    [idCategoria] [int] NULL,
	    [margenGanancia] [decimal](10, 2) NOT NULL,
	    [estado] [bit] NOT NULL*/


    }
}
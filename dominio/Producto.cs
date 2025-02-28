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

        public Marca Marca { get; set; }
        public int idMarca { get; set; }


        public Tipos Tipo { get; set; }
        public int idTipo { get; set; }


        public Categoria Categoria { get; set; }
        public int IdCategoria { get; set; }



        public Proveedor proveedor { get; set; }  // mmmmm ?
        public int IdProveedor  { get; set; }


        public decimal margenGanancia { get; set; }
        public bool estado { get; set; }







    }
}
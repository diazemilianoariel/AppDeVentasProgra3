using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class Venta
    {
        // esta será una clase que registre las ventas: 
        // fecha, monto, cliente, productos, etc.

        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public Cliente Cliente { get; set; }
        public List<Producto> Productos { get; set; }

        // constructor
        public Venta()
        {
            Productos = new List<Producto>();
        }

        // constructor con parámetros
        public Venta(int idVenta, DateTime fecha, decimal monto, Cliente cliente)
        {
            IdVenta = idVenta;
            Fecha = fecha;
            Monto = monto;
            Cliente = cliente;
            Productos = new List<Producto>();
        }

       
    }
}
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace dominio
{
    public class Venta
    {
        // esta será una clase que registre las ventas: 
        // fecha, monto, cliente, productos, etc.

        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        private decimal _monto;
        public decimal Monto
        {
            get => _monto;
            set => _monto = value;
        }
        public Cliente Cliente { get; set; }

        public bool EnLocal { get; set; }

        public int idEstadoVenta { get; set; }

        public string nombreEstadoVenta { get; set; }


        public List<Producto> Productos { get; set; }

        public Venta()
        {
            Productos = new List<Producto>();
        }

        public Venta(int idVenta, DateTime fecha, Cliente cliente)
        {
            IdVenta = idVenta;
            Fecha = fecha;
            Cliente = cliente;
            Productos = new List<Producto>();
        }

        private decimal CalcularMonto()
        {
            decimal total = 0;
            foreach (var producto in Productos)
            {
                total += producto.precioVenta * producto.Cantidad;
            }
            return total;
        }

        public decimal MontoCalculado
        {
            get { return CalcularMonto(); }
        }



    }
}
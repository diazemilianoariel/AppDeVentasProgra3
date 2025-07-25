using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dominio
{
    public class Producto
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(50)]
        public string nombre { get; set; }

        [StringLength(100)] // Coincide con la BD
        public string descripcion { get; set; }

        [Range(0, 99999999.99, ErrorMessage = "El precio no puede ser negativo.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El precio debe tener como máximo dos decimales.")]
        public decimal precio { get; set; }

        // Propiedad calculada. El setter privado evita que se asigne desde fuera.
        public decimal precioVenta { get; private set; }

        // Propiedad que calcula el subtotal para el carrito
        public decimal SubTotal => Math.Round(precioVenta * Cantidad, 2);

        public string Imagen { get; set; }

        // Stock disponible en el inventario
        public int stock { get; set; }

        // Cantidad seleccionada por el cliente para la compra
        public int Cantidad { get; set; }

        // Propiedades de navegación para relaciones uno a muchos
        public Marca Marca { get; set; }
        public int idMarca { get; set; }

        public Tipos Tipo { get; set; }
        public int idTipo { get; set; }

        public Categoria Categoria { get; set; }
        public int IdCategoria { get; set; }

        // Propiedad para la relación muchos a muchos
        public List<Proveedor> Proveedores { get; set; }

        [Range(0, 1000, ErrorMessage = "El margen de ganancia debe ser un valor porcentual positivo.")]
        public decimal margenGanancia { get; set; }

        public bool estado { get; set; }

        // Constructor para inicializar la lista y calcular el precio de venta
        public Producto()
        {
            Proveedores = new List<Proveedor>();
            // El cálculo de precioVenta se puede hacer aquí o al asignar sus dependencias
        }

        // Método para recalcular el precio de venta si cambian sus componentes
        public void CalcularPrecioVenta()
        {
            this.precioVenta = Math.Round(this.precio + (this.precio * (this.margenGanancia / 100)), 2);
        }
    }
}
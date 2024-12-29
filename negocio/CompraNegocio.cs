using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dominio;


namespace negocio
{
    public class CompraNegocio
    {
        public void CargarCompra(Compra compra)
        {
            // Validar los campos
            if (string.IsNullOrEmpty(compra.Cliente) ||
                string.IsNullOrEmpty(compra.Producto) ||
                compra.Cantidad <= 0)
            {
                throw new Exception("Todos los campos son obligatorios.");
            }

            // Guardar la compra en la base de datos
            // ...
        }
    }
}
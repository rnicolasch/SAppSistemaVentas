using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos
{
    public class DetalleVenta
    {
        public Producto Producto { get; set; } = new();

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public decimal Subtotal => Cantidad * Precio;
    }
}

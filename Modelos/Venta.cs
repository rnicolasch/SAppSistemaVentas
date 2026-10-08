using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos
{
    public class Venta
    {
        public int Id { get; set; }

        public Usuario Usuario { get; set; } = new();

        public DateTime Fecha { get; set; }

        public List<DetalleVenta> Detalles { get; set; } = new();

        public decimal Total => Detalles.Sum(d => d.Subtotal);
    }
}

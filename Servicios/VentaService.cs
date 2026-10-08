using Interfaces;
using Modelos;

namespace Servicios
{
    public class VentaService : IVentaService
    {
        private readonly List<Venta> _ventas;

        public VentaService(List<Venta> ventas)
        {
            _ventas = ventas;
        }

        public bool Registrar(Venta venta)
        {
            try
            {
                if (venta == null)
                {
                    throw new ArgumentNullException(nameof(venta));
                }

                if (!venta.Detalles.Any())
                {
                    return false;
                }

                // Validar cantidades
                if (venta.Detalles.Any(d => d.Cantidad <= 0))
                {
                    return false;
                }

                // Validar stock antes de modificarlo
                foreach (DetalleVenta detalle in venta.Detalles)
                {
                    if (detalle.Producto.Stock < detalle.Cantidad)
                    {
                        Console.WriteLine(
                            $"Stock insuficiente para: " +
                            $"{detalle.Producto.Nombre}");

                        return false;
                    }
                }

                // Generar ID
                venta.Id = _ventas.Any()
                    ? _ventas.Max(v => v.Id) + 1
                    : 1;

                venta.Fecha = DateTime.Now;

                // Descontar stock
                foreach (DetalleVenta detalle in venta.Detalles)
                {
                    detalle.Producto.Stock -= detalle.Cantidad;
                }

                // Registrar venta
                _ventas.Add(venta);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error al registrar venta: {ex.Message}");

                return false;
            }
        }

        public List<Venta> Listar()
        {
            try
            {
                return _ventas
                    .OrderByDescending(v => v.Fecha)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error al listar ventas: {ex.Message}");

                return new List<Venta>();
            }
        }

        public decimal ObtenerTotalVentas()
        {
            try
            {
                return _ventas.Sum(v => v.Total);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error al obtener total: {ex.Message}");

                return 0;
            }
        }
    }
}

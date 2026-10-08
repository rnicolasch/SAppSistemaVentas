using Interfaces;
using Modelos;

namespace Servicios
{
    public class ProductoService :IProductoService
    {
        private readonly List<Producto> _productos;

        public ProductoService(List<Producto> productos)
        {
            _productos = productos;
        }

        public bool Registrar(Producto producto)
        {
            try
            {
                if (producto == null)
                {
                    throw new ArgumentNullException(nameof(producto));
                }

                if (string.IsNullOrWhiteSpace(producto.Codigo))
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(producto.Nombre))
                {
                    return false;
                }

                if (producto.Precio < 0)
                {
                    return false;
                }

                if (producto.Stock < 0)
                {
                    return false;
                }

                bool existe = _productos.Any(p =>
                    p.Codigo.Equals(
                        producto.Codigo,
                        StringComparison.OrdinalIgnoreCase));

                if (existe)
                {
                    return false;
                }

                producto.Id = _productos.Any()
                    ? _productos.Max(p => p.Id) + 1
                    : 1;

                _productos.Add(producto);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error al registrar producto: {ex.Message}");

                return false;
            }
        }

        public List<Producto> Listar()
        {
            try
            {
                return _productos
                    .OrderBy(p => p.Id)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error al listar productos: {ex.Message}");

                return new List<Producto>();
            }
        }

        public Producto? BuscarPorCodigo(string codigo)
        {
            try
            {
                return _productos.FirstOrDefault(p =>
                    p.Codigo.Equals(
                        codigo,
                        StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error al buscar producto: {ex.Message}");

                return null;
            }
        }

        public bool ExisteCodigo(string codigo)
        {
            try
            {
                return _productos.Any(p =>
                    p.Codigo.Equals(
                        codigo,
                        StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error al verificar código: {ex.Message}");

                return false;
            }
        }

        public bool ActualizarStock(int idProducto, int cantidad)
        {
            try
            {
                Producto? producto = _productos
                    .FirstOrDefault(p => p.Id == idProducto);

                if (producto == null)
                {
                    return false;
                }

                int nuevoStock = producto.Stock + cantidad;

                if (nuevoStock < 0)
                {
                    return false;
                }

                producto.Stock = nuevoStock;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error al actualizar stock: {ex.Message}");

                return false;
            }
        }
    }
}

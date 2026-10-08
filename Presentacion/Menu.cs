using Interfaces;
using Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentacion
{
    public class Menu
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IProductoService _productoService;
        private readonly IVentaService _ventaService;

        private Usuario? _usuarioActual;

        public Menu(
            IUsuarioService usuarioService,
            IProductoService productoService,
            IVentaService ventaService)
        {
            _usuarioService = usuarioService;
            _productoService = productoService;
            _ventaService = ventaService;
        }

        public void Iniciar()
        {
            bool salir = false;

            while (!salir)
            {
                try
                {
                    Console.Clear();

                    Console.WriteLine("======================================");
                    Console.WriteLine("         SISTEMA DE VENTAS");
                    Console.WriteLine("======================================");
                    Console.WriteLine("1. Iniciar sesión");
                    Console.WriteLine("2. Registrar usuario");
                    Console.WriteLine("0. Salir");
                    Console.WriteLine("======================================");
                    Console.Write("Seleccione una opción: ");

                    string opcion = Console.ReadLine() ?? string.Empty;

                    switch (opcion)
                    {
                        case "1":
                            Login();
                            break;

                        case "2":
                            RegistrarUsuario();
                            break;

                        case "0":
                            salir = true;
                            Console.WriteLine("Programa finalizado.");
                            break;

                        default:
                            MostrarMensaje("Opción no válida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error inesperado: {ex.Message}");
                }
            }
        }

        private void Login()
        {
            try
            {
                Console.Clear();

                Console.WriteLine("======================================");
                Console.WriteLine("                 LOGIN");
                Console.WriteLine("======================================");

                Console.Write("Usuario: ");
                string usuario = Console.ReadLine() ?? string.Empty;

                Console.Write("Contraseña: ");
                string password = Console.ReadLine() ?? string.Empty;

                Usuario? usuarioEncontrado =
                    _usuarioService.Login(usuario, password);

                if (usuarioEncontrado == null)
                {
                    MostrarMensaje(
                        "Usuario o contraseña incorrectos.");

                    return;
                }

                _usuarioActual = usuarioEncontrado;

                Console.WriteLine();
                Console.WriteLine(
                    $"Bienvenido, {_usuarioActual.Nombre}");

                Console.ReadKey();

                MenuPrincipal();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error en login: {ex.Message}");
            }
        }

        private void RegistrarUsuario()
        {
            try
            {
                Console.Clear();

                Console.WriteLine("======================================");
                Console.WriteLine("          REGISTRO DE USUARIO");
                Console.WriteLine("======================================");

                Console.Write("Nombre: ");
                string nombre = Console.ReadLine() ?? string.Empty;

                Console.Write("Usuario: ");
                string usuario = Console.ReadLine() ?? string.Empty;

                if (_usuarioService.ExisteUsuario(usuario))
                {
                    MostrarMensaje("El usuario ya existe.");
                    return;
                }

                Console.Write("Contraseña: ");
                string password = Console.ReadLine() ?? string.Empty;

                Usuario nuevoUsuario = new()
                {
                    Nombre = nombre,
                    UsuarioLogin = usuario,
                    Password = password
                };

                bool resultado =
                    _usuarioService.Registrar(nuevoUsuario);

                if (resultado)
                {
                    MostrarMensaje(
                        "Usuario registrado correctamente.");
                }
                else
                {
                    MostrarMensaje(
                        "No se pudo registrar el usuario.");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(
                    $"Error al registrar usuario: {ex.Message}");
            }
        }

        private void MenuPrincipal()
        {
            bool cerrarSesion = false;

            while (!cerrarSesion)
            {
                try
                {
                    Console.Clear();

                    Console.WriteLine("======================================");
                    Console.WriteLine("         SISTEMA DE VENTAS");
                    Console.WriteLine("======================================");

                    Console.WriteLine(
                        $"Usuario: {_usuarioActual?.Nombre}");

                    Console.WriteLine("======================================");
                    Console.WriteLine("1. Registrar producto");
                    Console.WriteLine("2. Reporte de productos");
                    Console.WriteLine("3. Registrar venta");
                    Console.WriteLine("4. Reporte de ventas");
                    Console.WriteLine("5. Cerrar sesión");
                    Console.WriteLine("======================================");

                    Console.Write("Seleccione una opción: ");

                    string opcion = Console.ReadLine() ?? string.Empty;

                    switch (opcion)
                    {
                        case "1":
                            RegistrarProducto();
                            break;

                        case "2":
                            ReporteProductos();
                            break;

                        case "3":
                            RegistrarVenta();
                            break;

                        case "4":
                            ReporteVentas();
                            break;

                        case "5":
                            _usuarioActual = null;
                            cerrarSesion = true;

                            MostrarMensaje(
                                "Sesión cerrada correctamente.");

                            break;

                        default:
                            MostrarMensaje("Opción no válida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error: {ex.Message}");
                }
            }
        }

        private void RegistrarProducto()
        {
            try
            {
                Console.Clear();

                Console.WriteLine("======================================");
                Console.WriteLine("         REGISTRAR PRODUCTO");
                Console.WriteLine("======================================");

                Console.Write("Código: ");
                string codigo = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(codigo))
                {
                    MostrarMensaje("El código es obligatorio.");
                    return;
                }

                if (_productoService.ExisteCodigo(codigo))
                {
                    MostrarMensaje(
                        "El código del producto ya existe.");

                    return;
                }

                Console.Write("Nombre: ");
                string nombre = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    MostrarMensaje("El nombre es obligatorio.");
                    return;
                }

                Console.Write("Precio: ");

                if (!decimal.TryParse(
                        Console.ReadLine(),
                        out decimal precio))
                {
                    MostrarMensaje("Precio inválido.");
                    return;
                }

                Console.Write("Stock inicial: ");

                if (!int.TryParse(
                        Console.ReadLine(),
                        out int stock))
                {
                    MostrarMensaje("Stock inválido.");
                    return;
                }

                if (precio < 0)
                {
                    MostrarMensaje(
                        "El precio no puede ser negativo.");

                    return;
                }

                if (stock < 0)
                {
                    MostrarMensaje(
                        "El stock no puede ser negativo.");

                    return;
                }

                Producto producto = new()
                {
                    Codigo = codigo.Trim(),
                    Nombre = nombre.Trim(),
                    Precio = precio,
                    Stock = stock
                };

                bool resultado =
                    _productoService.Registrar(producto);

                MostrarMensaje(
                    resultado
                        ? "Producto registrado correctamente."
                        : "No se pudo registrar el producto.");
            }
            catch (Exception ex)
            {
                MostrarMensaje(
                    $"Error al registrar producto: {ex.Message}");
            }
        }

        private void ReporteProductos()
        {
            try
            {
                Console.Clear();

                Console.WriteLine(
                    "=========================================================================");

                Console.WriteLine(
                    "{0,-5} {1,-10} {2,-30} {3,12} {4,10}",
                    "ID",
                    "CODIGO",
                    "PRODUCTO",
                    "PRECIO",
                    "STOCK");

                Console.WriteLine(
                    "-------------------------------------------------------------------------");

                List<Producto> productos =
                    _productoService.Listar();

                if (!productos.Any())
                {
                    Console.WriteLine(
                        "No existen productos registrados.");
                }
                else
                {
                    foreach (Producto producto in productos)
                    {
                        Console.WriteLine(
                            "{0,-5} {1,-10} {2,-30} {3,12:C} {4,10}",
                            producto.Id,
                            producto.Codigo,
                            producto.Nombre,
                            producto.Precio,
                            producto.Stock);
                    }
                }

                Console.WriteLine(
                    "=========================================================================");

                Console.WriteLine();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                MostrarMensaje(
                    $"Error en reporte: {ex.Message}");
            }
        }

        private void RegistrarVenta()
        {
            try
            {
                Console.Clear();

                Console.WriteLine("======================================");
                Console.WriteLine("           REGISTRAR VENTA");
                Console.WriteLine("======================================");

                List<DetalleVenta> detalles = new();

                while (true)
                {
                    Console.Clear();

                    Console.WriteLine("======================================");
                    Console.WriteLine("           PRODUCTOS");
                    Console.WriteLine("======================================");

                    List<Producto> productos =
                        _productoService
                            .Listar()
                            .Where(p => p.Stock > 0)
                            .ToList();

                    if (!productos.Any())
                    {
                        MostrarMensaje(
                            "No existen productos con stock disponible.");

                        return;
                    }

                    Console.WriteLine(
                        "{0,-10} {1,-25} {2,12} {3,10}",
                        "CODIGO",
                        "PRODUCTO",
                        "PRECIO",
                        "STOCK");

                    Console.WriteLine(
                        "----------------------------------------------------------");

                    foreach (Producto producto in productos)
                    {
                        Console.WriteLine(
                            "{0,-10} {1,-25} {2,12:C} {3,10}",
                            producto.Codigo,
                            producto.Nombre,
                            producto.Precio,
                            producto.Stock);
                    }

                    Console.WriteLine();
                    Console.Write(
                        "Código del producto (0 para terminar): ");

                    string codigo =
                        Console.ReadLine() ?? string.Empty;

                    if (codigo == "0")
                    {
                        break;
                    }

                    Producto? productoSeleccionado =
                        _productoService.BuscarPorCodigo(codigo);

                    if (productoSeleccionado == null)
                    {
                        MostrarMensaje("Producto no encontrado.");
                        continue;
                    }

                    Console.Write("Cantidad: ");

                    if (!int.TryParse(
                            Console.ReadLine(),
                            out int cantidad))
                    {
                        MostrarMensaje("Cantidad inválida.");
                        continue;
                    }

                    if (cantidad <= 0)
                    {
                        MostrarMensaje(
                            "La cantidad debe ser mayor que cero.");

                        continue;
                    }

                    DetalleVenta? detalleExistente =
                        detalles.FirstOrDefault(
                            d => d.Producto.Id ==
                                 productoSeleccionado.Id);

                    int cantidadActual =
                        detalleExistente?.Cantidad ?? 0;

                    if (cantidadActual + cantidad >
                        productoSeleccionado.Stock)
                    {
                        MostrarMensaje(
                            $"Stock insuficiente. " +
                            $"Disponible: {productoSeleccionado.Stock}");

                        continue;
                    }

                    if (detalleExistente == null)
                    {
                        detalles.Add(new DetalleVenta
                        {
                            Producto = productoSeleccionado,
                            Cantidad = cantidad,
                            Precio = productoSeleccionado.Precio
                        });
                    }
                    else
                    {
                        detalleExistente.Cantidad += cantidad;
                    }

                    Console.WriteLine();
                    Console.WriteLine(
                        "Producto agregado correctamente.");

                    Console.ReadKey();
                }

                if (!detalles.Any())
                {
                    MostrarMensaje(
                        "No se agregaron productos a la venta.");

                    return;
                }

                ConfirmarVenta(detalles);
            }
            catch (Exception ex)
            {
                MostrarMensaje(
                    $"Error al registrar venta: {ex.Message}");
            }
        }

        private void ConfirmarVenta(List<DetalleVenta> detalles)
        {
            try
            {
                Console.Clear();

                Console.WriteLine("======================================");
                Console.WriteLine("           RESUMEN DE VENTA");
                Console.WriteLine("======================================");

                Console.WriteLine(
                    "{0,-25} {1,8} {2,12} {3,12}",
                    "PRODUCTO",
                    "CANT.",
                    "PRECIO",
                    "SUBTOTAL");

                Console.WriteLine(
                    "-----------------------------------------------------------");

                foreach (DetalleVenta detalle in detalles)
                {
                    Console.WriteLine(
                        "{0,-25} {1,8} {2,12:C} {3,12:C}",
                        detalle.Producto.Nombre,
                        detalle.Cantidad,
                        detalle.Precio,
                        detalle.Subtotal);
                }

                decimal total =
                    detalles.Sum(d => d.Subtotal);

                Console.WriteLine(
                    "-----------------------------------------------------------");

                Console.WriteLine(
                    $"TOTAL: {total:C}");

                Console.WriteLine();
                Console.Write("¿Confirmar venta? (S/N): ");

                string respuesta =
                    Console.ReadLine() ?? string.Empty;

                if (!respuesta.Equals(
                        "S",
                        StringComparison.OrdinalIgnoreCase))
                {
                    MostrarMensaje("Venta cancelada.");
                    return;
                }

                if (_usuarioActual == null)
                {
                    MostrarMensaje(
                        "No existe un usuario con sesión activa.");

                    return;
                }

                Venta venta = new()
                {
                    Usuario = _usuarioActual,
                    Fecha = DateTime.Now,
                    Detalles = detalles
                };

                bool resultado =
                    _ventaService.Registrar(venta);

                if (!resultado)
                {
                    MostrarMensaje(
                        "No se pudo registrar la venta.");

                    return;
                }

                Console.WriteLine();
                Console.WriteLine("======================================");
                Console.WriteLine("      VENTA REGISTRADA CORRECTAMENTE");
                Console.WriteLine("======================================");
                Console.WriteLine($"Número de venta : {venta.Id}");
                Console.WriteLine($"Fecha           : {venta.Fecha}");
                Console.WriteLine($"Usuario         : {venta.Usuario.Nombre}");
                Console.WriteLine($"Total           : {venta.Total:C}");

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                MostrarMensaje(
                    $"Error al confirmar venta: {ex.Message}");
            }
        }

        private void ReporteVentas()
        {
            try
            {
                Console.Clear();

                Console.WriteLine("======================================");
                Console.WriteLine("           REPORTE DE VENTAS");
                Console.WriteLine("======================================");

                List<Venta> ventas =
                    _ventaService.Listar();

                if (!ventas.Any())
                {
                    MostrarMensaje(
                        "No existen ventas registradas.");

                    return;
                }

                foreach (Venta venta in ventas)
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "--------------------------------------");

                    Console.WriteLine(
                        $"Venta N° : {venta.Id}");

                    Console.WriteLine(
                        $"Fecha    : {venta.Fecha}");

                    Console.WriteLine(
                        $"Usuario  : {venta.Usuario.Nombre}");

                    Console.WriteLine();
                    Console.WriteLine("DETALLE:");

                    foreach (DetalleVenta detalle in venta.Detalles)
                    {
                        Console.WriteLine(
                            $"  {detalle.Producto.Codigo} - " +
                            $"{detalle.Producto.Nombre} | " +
                            $"Cantidad: {detalle.Cantidad} | " +
                            $"Precio: {detalle.Precio:C} | " +
                            $"Subtotal: {detalle.Subtotal:C}");
                    }

                    Console.WriteLine(
                        $"TOTAL: {venta.Total:C}");
                }

                Console.WriteLine();
                Console.WriteLine(
                    "======================================");

                Console.WriteLine(
                    $"TOTAL VENDIDO: " +
                    $"{_ventaService.ObtenerTotalVentas():C}");

                Console.WriteLine();
                Console.WriteLine(
                    "Presione una tecla para continuar...");

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                MostrarMensaje(
                    $"Error en reporte de ventas: {ex.Message}");
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            Console.WriteLine();
            Console.WriteLine("======================================");
            Console.WriteLine(mensaje);
            Console.WriteLine("======================================");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}

using Interfaces;
using Modelos;
using Presentacion;
using Servicios;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            // ==========================================
            // LISTAS
            // ==========================================

            List<Usuario> usuarios = new();

            List<Producto> productos = new();

            List<Venta> ventas = new();


            // ==========================================
            // USUARIO INICIAL
            // ==========================================

            usuarios.Add(new Usuario
            {
                Id = 1,
                Nombre = "Administrador",
                UsuarioLogin = "admin",
                Password = "1234"
            });


            // ==========================================
            // PRODUCTOS INICIALES
            // ==========================================

            productos.Add(new Producto
            {
                Id = 1,
                Codigo = "P001",
                Nombre = "Laptop Lenovo",
                Precio = 2500.00m,
                Stock = 10
            });

            productos.Add(new Producto
            {
                Id = 2,
                Codigo = "P002",
                Nombre = "Mouse Logitech",
                Precio = 80.00m,
                Stock = 20
            });

            productos.Add(new Producto
            {
                Id = 3,
                Codigo = "P003",
                Nombre = "Teclado Gamer",
                Precio = 150.00m,
                Stock = 15
            });


            // ==========================================
            // SERVICIOS
            // ==========================================

            IUsuarioService usuarioService =
                new UsuarioService(usuarios);

            IProductoService productoService =
                new ProductoService(productos);

            IVentaService ventaService =
                new VentaService(ventas);


            // ==========================================
            // MENU
            // ==========================================

            Menu menu = new(
                usuarioService,
                productoService,
                ventaService);

            menu.Iniciar();
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine(
                $"Error crítico de la aplicación: {ex.Message}");

            Console.WriteLine();
            Console.WriteLine(
                "Presione una tecla para cerrar...");

            Console.ReadKey();
        }
    }
}
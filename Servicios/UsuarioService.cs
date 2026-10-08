using Modelos;
using Interfaces;

namespace Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly List<Usuario> _usuarios;

        public UsuarioService(List<Usuario> usuarios)
        {
            _usuarios = usuarios;
        }

        public bool Registrar(Usuario usuario)
        {
            try
            {
                if (usuario == null)
                {
                    throw new ArgumentNullException(nameof(usuario));
                }

                if (string.IsNullOrWhiteSpace(usuario.Nombre))
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(usuario.UsuarioLogin))
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(usuario.Password))
                {
                    return false;
                }

                bool existe = _usuarios.Any(u =>
                    u.UsuarioLogin.Equals(
                        usuario.UsuarioLogin,
                        StringComparison.OrdinalIgnoreCase));

                if (existe)
                {
                    return false;
                }

                usuario.Id = _usuarios.Any()
                    ? _usuarios.Max(u => u.Id) + 1
                    : 1;

                _usuarios.Add(usuario);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");
                return false;
            }
        }

        public Usuario? Login(string usuario, string password)
        {
            try
            {
                return _usuarios.FirstOrDefault(u =>
                    u.UsuarioLogin.Equals(
                        usuario,
                        StringComparison.OrdinalIgnoreCase)
                    &&
                    u.Password == password);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en login: {ex.Message}");
                return null;
            }
        }

        public bool ExisteUsuario(string usuario)
        {
            try
            {
                return _usuarios.Any(u =>
                    u.UsuarioLogin.Equals(
                        usuario,
                        StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar usuario: {ex.Message}");
                return false;
            }
        }

        public List<Usuario> Listar()
        {
            try
            {
                return _usuarios
                    .OrderBy(u => u.Id)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar usuarios: {ex.Message}");
                return new List<Usuario>();
            }
        }
    }
}

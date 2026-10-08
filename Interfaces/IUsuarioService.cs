using Modelos;

namespace Interfaces
{
    public interface IUsuarioService
    {
        bool Registrar(Usuario usuario);

        Usuario? Login(string usuario, string password);

        bool ExisteUsuario(string usuario);

        List<Usuario> Listar();
    }
}

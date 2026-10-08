namespace Modelos
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string UsuarioLogin { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}

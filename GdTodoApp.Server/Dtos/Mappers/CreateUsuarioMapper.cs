using GdToDoApp.Server.Model;

namespace GdTodoApp.Server.Dtos.Mappers
{
    public class CreateUsuarioMapper
    {
        public static Usuario CreateUsuarioToUsuario(CreateUsuario createUsuario)
        {
            var usuario = new Usuario()
            {
                Username = createUsuario.Username
            };
            usuario.PasswordHash = Util.Util.HashPassword(usuario, createUsuario.Password);

            return usuario;
        }
    }
}

using GdToDoApp.Server.Model;

namespace GdToDoApp.Server.Services.Interfaces
{
    public interface IUsuarioService
    {
        public Task<Usuario[]> GetUsuariosAsync();
        public Task<(Usuario usuario, string jwt)> GetUsuarioLoginAsync(string userName, string password);
        public Task AddUsuarioAsync(Usuario usuario);
    }
}

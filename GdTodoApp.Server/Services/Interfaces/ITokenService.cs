using GdToDoApp.Server.Model;

namespace GdToDoApp.Server.Services
{
    public interface ITokenService
    {
        public string GenerateToken(Usuario usuario);
    }
}

using GdToDoApp.Server.Model;

namespace GdToDoApp.Server.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(Usuario usuario);
    }
}

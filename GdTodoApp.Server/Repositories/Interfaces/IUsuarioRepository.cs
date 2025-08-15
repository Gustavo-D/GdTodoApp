using GdToDoApp.Server.Model;

namespace GdToDoApp.Server.Repositories.Interfaces
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        public Task<Usuario> GetByUsername(string userName);
    }
}

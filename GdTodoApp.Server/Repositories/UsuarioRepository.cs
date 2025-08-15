using GdToDoApp.Server.Model;
using GdToDoApp.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace GdToDoApp.Server.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly MyDbContext _context;

        public UsuarioRepository(MyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Usuario> GetByUsername(string userName)
        {
            var usuario = await _context.Usuarios.SingleOrDefaultAsync
                        (p => EF.Functions.ILike(p.Username, userName));
            return usuario;
        }
    }
}

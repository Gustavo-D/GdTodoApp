using GdToDoApp.Server.Model;
using GdToDoApp.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GdToDoApp.Server.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbContext _context;
        protected DbSet<T> _dbSet;

        public RepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task Create(T model)
        {
            _dbSet.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteById(long id)
        {
            var dados = await _dbSet.FindAsync(id);
            if (dados != null)
            {
                _dbSet.Remove(dados);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<T[]> GetById(long? id, bool tracked = true)
        {
            if (id != null)
            {
                var dados = await _dbSet.FindAsync(id);
                if (!tracked)
                {
                    _context.Entry(dados).State = EntityState.Detached;
                }
                return dados != null ? new[] { dados } : Array.Empty<T>();
            }

            var retorno = tracked ? await _dbSet.ToArrayAsync() : await _dbSet.AsNoTracking().ToArrayAsync();
            return retorno != null && retorno.Length != 0 ? retorno : Array.Empty<T>();
        }

        public async Task Update(T model)
        {
            _dbSet.Attach(model);
            if (_context.Entry(model).State == EntityState.Modified)
            {
                await _context.SaveChangesAsync();
            }
        }
    }
}

using GdToDoApp.Server.Model;
using GdToDoApp.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GdToDoApp.Server.Repositories
{
    public class TarefaRepository : RepositoryBase<Tarefa>, ITarefaRepository
    {
        private readonly MyDbContext _context;

        public TarefaRepository(MyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Tarefa[]> GetAsync()
        {
            var tarefas = await _context.Tarefas.ToArrayAsync();
            return tarefas;
        }

        public async Task<Tarefa[]> GetByFilterAsync(long? userId, int? isCompleted,
                                                     DateTimeOffset? dateCreatedAtStart, DateTimeOffset? dateCreatedAtEnd,
                                                     DateTimeOffset? dateUpdatedAtStart, DateTimeOffset? dateUpdatedAtEnd)
        {
            var tarefas = await _context.Tarefas.Where(p => (userId == null || p.UserId == userId)
                                                         && (isCompleted == null || p.IsCompleted == isCompleted)
                                                         && (dateCreatedAtStart == null || p.CreatedAt >= dateCreatedAtStart)
                                                         && (dateCreatedAtEnd == null || p.CreatedAt <= dateCreatedAtEnd)

                                                         && (dateUpdatedAtStart == null || p.UpdatedAt >= dateUpdatedAtStart)
                                                         && (dateUpdatedAtEnd == null || p.UpdatedAt <= dateUpdatedAtEnd)
                                                      ).ToArrayAsync();
            return tarefas;
        }

        public async Task<Tarefa> UpdateCategory(long id, string category)
        {
            var editadas = await _context.Tarefas
                           .Where(p => p.Id == id)
                           .ExecuteUpdateAsync(q => q.SetProperty(r => r.Category, category)
                                                     .SetProperty(r => r.UpdatedAt, DateTimeOffset.UtcNow));

            var atualizada = await _context.Tarefas.SingleAsync(p => p.Id == id);
            return atualizada;
        }

        public async Task<HashSet<string>> GetTarefaCategories()
        {
            var categories = await _context.Tarefas
                            .Where(t => !string.IsNullOrWhiteSpace(t.Category))
                            .Select(t => t.Category)
                            .Distinct()
                            .ToListAsync();

            return [.. categories];
        }
    }
}

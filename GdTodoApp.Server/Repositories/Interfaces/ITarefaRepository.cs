using GdToDoApp.Server.Model;

namespace GdToDoApp.Server.Repositories.Interfaces
{
    public interface ITarefaRepository : IRepositoryBase<Tarefa>
    {
        public Task<Tarefa[]> GetAsync();
        public Task<Tarefa[]> GetByFilterAsync(long? userId, int? isCompleted,
                                                     DateTimeOffset? dateCreatedAtStart, DateTimeOffset? dateCreatedAtEnd,
                                                     DateTimeOffset? dateUpdatedAtStart, DateTimeOffset? dateUpdatedAtEnd);
        public Task<Tarefa> UpdateCategory(long id, string category);
        public Task<HashSet<string>> GetTarefaCategories();
    }
}

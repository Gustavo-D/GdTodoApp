using GdToDoApp.Server.Model;

namespace GdToDoApp.Server.Repositories.Interfaces
{
    public interface ITarefaRepository : IRepositoryBase<Tarefa>
    {
        public Task<Tarefa[]> GetAsync(long? Id);
        public Task<Tarefa[]> GetByFilterAsync(long[] userId, int? isCompleted, string[] category,
                                                     DateTimeOffset? dateCreatedAtStart, DateTimeOffset? dateCreatedAtEnd,
                                                     DateTimeOffset? dateUpdatedAtStart, DateTimeOffset? dateUpdatedAtEnd);
        public Task<Tarefa> UpdateCategory(long id, string category);
        public Task<HashSet<string>> GetTarefaCategories();
    }
}

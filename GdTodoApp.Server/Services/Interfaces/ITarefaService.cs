using GdToDoApp.Server.Model;

namespace GdToDoApp.Server.Services.Interfaces
{
    public interface ITarefaService
    {
        public Task<Tarefa> GetTarefaAsync(long id);
        public Task<Tarefa[]> GetTarefasAsync();
        public Task<Tarefa[]> GetTarefasByFilterAsync(long[]? userId, int? isCompleted, string[] category,
                                               DateTimeOffset? dateCreatedAtStart, DateTimeOffset? dateCreatedAtEnd,
                                               DateTimeOffset? dateUpdatedAtStart, DateTimeOffset? dateUpdatedAtEnd);
        public Task<Tarefa> CreateTarefaAsync(Dtos.TarefaDto tarefa);
        public Task<Tarefa> UpdateTarefaAsync(Dtos.TarefaDto tarefa);
        public Task DeleteTarefaAsync(long id);
        public Task<Tarefa> UpdateTarefaCategoryAsync(long id, string category);
        public Task<HashSet<string>> GetTarefaCategoriesAsync();
    }
}

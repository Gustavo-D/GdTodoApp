using GdToDoApp.Server.Dtos.Mappers;
using GdToDoApp.Server.Model;
using GdToDoApp.Server.Repositories.Interfaces;
using GdToDoApp.Server.Services.Interfaces;

namespace GdToDoApp.Server.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _repository;

        public TarefaService(ITarefaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Tarefa> GetTarefaAsync(long id)
        {
            var tarefa = await _repository.GetById(id);
            return tarefa != null && tarefa.Length > 0 ? tarefa.Single() : null;
        }

        public async Task<Tarefa[]> GetTarefasAsync()
        {
            var tarefas = await _repository.GetAsync();
            return tarefas;
        }

        public async Task<Tarefa[]> GetTarefasByFilterAsync(long? userId, int? isCompleted,
                                       DateTimeOffset? dateCreatedAtStart, DateTimeOffset? dateCreatedAtEnd,
                                       DateTimeOffset? dateUpdatedAtStart, DateTimeOffset? dateUpdatedAtEnd)
        {
            var tarefas = await _repository.GetByFilterAsync
                          (userId, isCompleted, dateCreatedAtStart, dateCreatedAtEnd, dateUpdatedAtStart, dateUpdatedAtEnd);
            return tarefas;
        }

        public async Task<Tarefa> CreateTarefaAsync(Dtos.TarefaDto tarefaDto)
        {
            var tarefa = TarefaDtoMapper.TarefaDtoToTarefa(tarefaDto, null);
            await _repository.Create(tarefa);

            var tarefaCriada = await _repository.GetById(tarefa.Id);
            return tarefaCriada.Single();
        }

        public async Task<Tarefa> UpdateTarefaAsync(Dtos.TarefaDto tarefaDto)
        {
            tarefaDto.UpdatedAt = DateTimeOffset.UtcNow;
            var tarefaOriginal = (await _repository.GetById(tarefaDto.Id)).Single();
            var tarefa = TarefaDtoMapper.TarefaDtoToTarefa(tarefaDto, tarefaOriginal);

            await _repository.Update(tarefa);

            var tarefaAtualizada = await _repository.GetById(tarefa.Id);

            return tarefaAtualizada.Single();
        }

        public async Task DeleteTarefaAsync(long id)
        {
            var excluido = await _repository.DeleteById(id);
        }

        public async Task<Tarefa> UpdateTarefaCategoryAsync(long id, string category)
        {
            var atualizada = await _repository.UpdateCategory(id, category);
            return atualizada;
        }

        public async Task<HashSet<string>> GetTarefaCategoriesAsync()
        {
            var categories = await _repository.GetTarefaCategories();
            return categories;
        }
    }
}

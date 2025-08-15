using GdToDoApp.Server.Dtos;
using GdToDoApp.Server.Model;
using GdToDoApp.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace GdToDoApp.Server.Controllers
{
    public class TarefaController : MyBaseController
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpGet]
        [Route("tarefa")]
        public async Task<Tarefa> GetTarefa(long id)
        {
            var tarefa = await _tarefaService.GetTarefaAsync(id);
            return tarefa;
        }

        [HttpGet]
        [Route("tarefas")]
        public async Task<Tarefa[]> GetTarefas()
        {
            var tarefas = await _tarefaService.GetTarefasAsync();
            return tarefas;
        }

        [HttpGet]
        [Route("tarefas/filter")]
        public async Task<Tarefa[]> GetByFilter(long? userId, int? isCompleted,
                                               DateTimeOffset? dateCreatedAtStart, DateTimeOffset? dateCreatedAtEnd,
                                               DateTimeOffset? dateUpdatedAtStart, DateTimeOffset? dateUpdatedAtEnd)
        {
            var tarefas = await _tarefaService.GetTarefasByFilterAsync(userId, isCompleted,
                                                                dateCreatedAtStart, dateCreatedAtEnd,
                                                                dateUpdatedAtStart, dateUpdatedAtEnd);
            return tarefas;
        }

        [HttpPost]
        [Route("tarefa")]
        public async Task<Tarefa> CreateTarefa(Dtos.TarefaDto tarefaDto)
        {
            var tarefaCriada = await _tarefaService.CreateTarefaAsync(tarefaDto);
            return tarefaCriada;
        }

        [HttpPatch]
        [Route("tarefa")]
        public async Task<Tarefa> UpdateTarefaAsync(Dtos.TarefaDto tarefaDto)
        {
            var tarefaAtualizada = await _tarefaService.UpdateTarefaAsync(tarefaDto);
            return tarefaAtualizada;
        }

        [HttpDelete]
        [Route("tarefa")]
        public async Task<(bool success, string message)> DeleteTarefaAsync(long id)
        {
            await _tarefaService.DeleteTarefaAsync(id);
            return (true, "Tarefa excluída com sucesso");
        }

        [HttpPatch]
        [Route("tarefa/category")]
        public async Task<Tarefa> UpdateTarefaCategory(long id, string category)
        {
            var tarefaAtualizada = await _tarefaService.UpdateTarefaCategoryAsync(id, category);
            return tarefaAtualizada;
        }

        [HttpGet]
        [Route("categories")]
        public async Task<string[]> GetTarefaCategories()
        {
            var categories = await _tarefaService.GetTarefaCategoriesAsync();
            return categories.Select(p => p).ToArray();
        }
    }
}

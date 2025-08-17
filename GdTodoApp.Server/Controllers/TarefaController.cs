using GdTodoApp.Server.Dtos.Shared;
using GdToDoApp.Server.Model;
using GdToDoApp.Server.Services.Interfaces;
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
        [Route("{id}")]
        public async Task<ResultadoApi<Tarefa>> GetTarefa(long id)
        {
            var tarefa = await _tarefaService.GetTarefaAsync(id);
            return new ResultadoApi<Tarefa>
            {
                Data = tarefa,
            };
        }

        [HttpGet]
        public async Task<ResultadoApi<Tarefa[]>> GetTarefas()
        {
            var tarefas = await _tarefaService.GetTarefasAsync();
            return new ResultadoApi<Tarefa[]>
            {
                Data = tarefas,
            };
        }

        [HttpGet]
        [Route("filter")]
        public async Task<ResultadoApi<Tarefa[]>> GetByFilter(long? userId, int? isCompleted,
                                               DateTimeOffset? dateCreatedAtStart, DateTimeOffset? dateCreatedAtEnd,
                                               DateTimeOffset? dateUpdatedAtStart, DateTimeOffset? dateUpdatedAtEnd)
        {
            var tarefas = await _tarefaService.GetTarefasByFilterAsync(userId, isCompleted,
                                                                dateCreatedAtStart, dateCreatedAtEnd,
                                                                dateUpdatedAtStart, dateUpdatedAtEnd);
            return new ResultadoApi<Tarefa[]>
            {
                Data = tarefas,
            };
        }

        [HttpPost]
        public async Task<ResultadoApi<Tarefa>> CreateTarefa(Dtos.TarefaDto tarefaDto)
        {
            var tarefaCriada = await _tarefaService.CreateTarefaAsync(tarefaDto);
            return new ResultadoApi<Tarefa>
            {
                Data = tarefaCriada,
            };
        }

        [HttpPatch]
        public async Task<ResultadoApi<Tarefa>> UpdateTarefaAsync(Dtos.TarefaDto tarefaDto)
        {
            var tarefaAtualizada = await _tarefaService.UpdateTarefaAsync(tarefaDto);
            return new ResultadoApi<Tarefa>
            {
                Data = tarefaAtualizada,
            };
        }

        [HttpDelete]
        public async Task<ResultadoApi<object>> DeleteTarefaAsync(long id)
        {
            await _tarefaService.DeleteTarefaAsync(id);
            return new ResultadoApi<object>
            {
                Data = null,
                Message = "Tarefa excluída com sucesso"
            };
        }

        [HttpPatch]
        [Route("category")]
        public async Task<ResultadoApi<Tarefa>> UpdateTarefaCategory(long id, string category)
        {
            var tarefaAtualizada = await _tarefaService.UpdateTarefaCategoryAsync(id, category);
            return new ResultadoApi<Tarefa>
            {
                Data = tarefaAtualizada,
            };
        }

        [HttpGet]
        [Route("categories")]
        public async Task<ResultadoApi<string[]>> GetTarefaCategories()
        {
            var categories = await _tarefaService.GetTarefaCategoriesAsync();
            return new ResultadoApi<string[]>
            {
                Data = [.. categories.Select(p => p)]
            };
        }
    }
}

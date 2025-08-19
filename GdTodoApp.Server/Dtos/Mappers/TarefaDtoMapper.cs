using GdToDoApp.Server.Model;

namespace GdToDoApp.Server.Dtos.Mappers
{
    public class TarefaDtoMapper
    {
        public static Tarefa TarefaDtoToTarefa(TarefaDto tarefaDto, Tarefa tarefaInstance)
        {
            var tarefa = tarefaInstance ?? new Tarefa();

            tarefa.Id = tarefaDto.Id;
            tarefa.Title = tarefaDto.Title;
            tarefa.Description = tarefaDto.Description;
            tarefa.IsCompleted = tarefaDto.IsCompleted;
            tarefa.Category = tarefaDto.Category;
            tarefa.CreatedAt = tarefaInstance?.CreatedAt ?? tarefaDto.CreatedAt;
            tarefa.UpdatedAt = tarefaDto.UpdatedAt;
            tarefa.UserId = tarefaDto.UserId;
            if (tarefaInstance != null )
            {
                tarefa.User = tarefaInstance.User;
            }

            return tarefa;
        }

        public static TarefaDto TarefaToTarefaDto(Tarefa tarefa)
        {
            var tarefaDto = new TarefaDto()
            {
                Id = tarefa.Id,
                Title = tarefa.Title,
                Description = tarefa.Description,
                IsCompleted = tarefa.IsCompleted,
                Category = tarefa.Category,
                CreatedAt = tarefa.CreatedAt,
                UpdatedAt = tarefa.UpdatedAt,
                UserId = (long)tarefa.UserId
            };
            return tarefaDto;
        }
    }
}

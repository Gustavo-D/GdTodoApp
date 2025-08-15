namespace GdToDoApp.Server.Dtos
{
    public class TarefaDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int IsCompleted { get; set; }
        public string Category { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public long UserId { get; set; }
    }
}

namespace GdTodoApp.Server.Dtos.Shared
{
    public class ResultadoApi<T>
    {
        public ResultadoApi() {; }

        public virtual Meta Meta { get; set; } = new Meta()
        {
            Status = System.Net.HttpStatusCode.OK
        };
        public virtual T? Data { get; set; }

        public string Message { get; set; }
    }
}

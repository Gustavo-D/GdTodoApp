using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GdTodoApp.Server.Dtos.Shared
{
    public class Meta
    {
        public Meta() {; }

        public virtual HttpStatusCode? Status { get; set; }
        public virtual Error[]? Errors { get; set; }
    }
}

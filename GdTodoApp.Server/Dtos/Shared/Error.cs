using GdToDoApp.Server.Util;

namespace GdTodoApp.Server.Dtos.Shared
{
    public class Error
    {
        public virtual string? QueryString { get; set; }
        public virtual string? FriendlyErrorMessage { get; set; }
    }
}
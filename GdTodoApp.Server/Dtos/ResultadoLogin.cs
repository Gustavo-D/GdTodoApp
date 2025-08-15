using GdToDoApp.Server.Model;

namespace GdToDoApp.Server.Dtos
{
    public class ResultadoLogin
    {
        public bool Success { get; set; }
        public Usuario User { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}

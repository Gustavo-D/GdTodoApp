using GdToDoApp.Server.Model;

namespace GdToDoApp.Server.Dtos
{
    public class ResultadoLogin
    {
        public Usuario User { get; set; }
        public string Token { get; set; }
    }
}

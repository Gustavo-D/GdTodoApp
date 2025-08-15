using GdToDoApp.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GdToDoApp.Server.Dtos;
using GdTodoApp.Server.Dtos.Shared;

namespace GdToDoApp.Server.Controllers
{
    public class LoginController : MyBaseController
    {
        private readonly IUsuarioService _usuarioService;

        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ResultadoApi<ResultadoLogin>> AuthenticateAsync([FromBody] Login model)
        {
            var resultado = await _usuarioService.GetUsuarioLoginAsync(model.Username, model.Password);

            if (resultado.usuario != null && !string.IsNullOrWhiteSpace(resultado.jwt))
            {
                return new ResultadoApi<ResultadoLogin>
                {
                    Data = new ResultadoLogin
                    {
                        Token = resultado.jwt,
                        User = new Model.Usuario
                        {
                            Id = resultado.usuario.Id,
                            Username = resultado.usuario.Username,
                            CreatedAt = resultado.usuario.CreatedAt
                        }
                    }
                };
            }

            throw new Exception("[401]Usuário e/ou senha incorretos. Tente novamente.");
        }
    }
}

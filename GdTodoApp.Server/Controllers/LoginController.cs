using GdToDoApp.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GdToDoApp.Server.Dtos;

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
        public async Task<ResultadoLogin> AuthenticateAsync([FromBody] Login model)
        {
            var resultado = await _usuarioService.GetUsuarioLoginAsync(model.Username, model.Password);

            if (resultado.usuario != null && !string.IsNullOrWhiteSpace(resultado.jwt))
            {
                return new ResultadoLogin
                {
                    Message = "Sucesso.",
                    Success = true,
                    Token = resultado.jwt,
                    User = new Model.Usuario
                    {
                        Id = resultado.usuario.Id,
                        Username = resultado.usuario.Username
                    }
                };
            }

            return new ResultadoLogin
            {
                Message = "Usuário e/ou senha incorretos. Tente novamente.",
                Success = false,
                Token = null,
                User = null,
            };
        }
    }
}

using GdTodoApp.Server.Dtos.Shared;
using GdToDoApp.Server.Model;
using GdToDoApp.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GdToDoApp.Server.Controllers
{
    public class UsuarioController : MyBaseController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("usuarios")]
        public async Task<ResultadoApi<Usuario[]>> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetUsuariosAsync();
            return new ResultadoApi<Usuario[]>
            {
                Data = usuarios,
            };
        }

        [HttpPost]
        [Route("usuario")]
        [AllowAnonymous]
        public async Task<ResultadoApi<object>> CreateUsuario(Usuario usuario)
        {
            await _usuarioService.AddUsuarioAsync(usuario);

            return new ResultadoApi<object>()
            {
                Data = null,
                Message = "Usuário criado com sucesso"
            };
        }
    }
}

using GdToDoApp.Server.Model;
using GdToDoApp.Server.Services;
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
        public async Task<Usuario[]> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetUsuariosAsync();
            return usuarios;
        }

        [HttpPost]
        [Route("usuario")]
        [AllowAnonymous]
        public async Task<(bool success, string message)> CreateUsuario(Usuario usuario)
        {
            await _usuarioService.AddUsuarioAsync(usuario);

            return (success: true, message: "Usuário criado com sucesso");
        }
    }
}

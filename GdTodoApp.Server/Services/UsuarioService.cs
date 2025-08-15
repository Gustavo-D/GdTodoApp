using GdToDoApp.Server.Model;
using GdToDoApp.Server.Repositories.Interfaces;
using GdToDoApp.Server.Services.Interfaces;

namespace GdToDoApp.Server.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly ITokenService _tokenService;

        public UsuarioService(IUsuarioRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<Usuario[]> GetUsuariosAsync()
        {
            var usuarios = await _repository.GetById(null);
            return usuarios;
        }

        public async Task<(Usuario usuario, string jwt)> GetUsuarioLoginAsync(string userName, string password)
        {
            var usuario = await _repository.GetByUsername(userName);
            var validouSenha = false;
            if (usuario != null && !string.IsNullOrWhiteSpace(usuario.Username))
            {
                validouSenha = Util.Util.VerificarHashPassword(usuario, usuario.PasswordHash, password);
            }

            string jwt = null;
            if (validouSenha)
            {
                jwt = _tokenService.GenerateToken(usuario);
            }

            return validouSenha ? (usuario, jwt) : (null, null);
        }

        public async Task AddUsuarioAsync(Usuario usuario)
        {
            var existente = await _repository.GetByUsername(usuario.Username);
            if (existente == null)
            {
                await _repository.Create(usuario);
            }
            else
            {
                throw new Exception("Username inválido.");
            }
        }
    }
}

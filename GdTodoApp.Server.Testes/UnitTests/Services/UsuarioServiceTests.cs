using GdToDoApp.Server.Repositories.Interfaces;
using GdToDoApp.Server.Services;
using GdToDoApp.Server.Services.Interfaces;
using Moq;
using GdToDoApp.Server.Model;
using GdTodoApp.Server.Dtos;
using GdTodoApp.Server.Dtos.Mappers;

namespace GdTodoApp.Server.Testes.UnitTests.Services
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _mockRepository;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            _mockRepository = new Mock<IUsuarioRepository>();
            _mockTokenService = new Mock<ITokenService>();
            _service = new UsuarioService(_mockRepository.Object, _mockTokenService.Object);
        }

        [Fact]
        public async Task GetUsuariosAsync_RetornaUsuarios()
        {
            //Arrange
            var usuarios = new Usuario[]
            {
                new Usuario
                {
                    Id = 1,
                    Username = "username1"
                },
                new Usuario
                {
                    Id = 2,
                    Username = "username2"
                }
            };

            _mockRepository.Setup(p => p.GetById(null, true)).ReturnsAsync(usuarios);

            // Act
            var retorno = await _service.GetUsuariosAsync();

            // Assert
            Assert.Equal(usuarios, retorno);
        }

        [Fact]
        public async Task GetUsuarioLoginAsync_AceitaLoginCorreto()
        {
            // Arrange
            const string usernameCorreto = "usuario";
            const string passwordCorreto = "123";
            const string jwtCorreto = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkdEIiwiaWF0IjoxNzU1Mjg5MTM1LCJleHAiOjE3NTUyOTI3MzV9.6Ds74kk74lehpTgWWfoqpv9lpa0Cqmft243NPqnF9e4";
            var usuarioCorreto = new Usuario()
            {
                Id = 1,
                Username = usernameCorreto,
                PasswordHash = Util.Util.HashPassword(null, passwordCorreto)
            };

            _mockRepository.Setup(p => p.GetByUsername(usernameCorreto)).ReturnsAsync(usuarioCorreto);
            _mockTokenService.Setup(p => p.GenerateToken(usuarioCorreto)).Returns(jwtCorreto);

            // Act
            var retorno = await _service.GetUsuarioLoginAsync(usernameCorreto, passwordCorreto);

            // Assert
            Assert.Equal(usernameCorreto, retorno.usuario.Username);
            Assert.Equal(usuarioCorreto.Id, retorno.usuario.Id);
            Assert.Equal(jwtCorreto, retorno.jwt);
        }

        [Fact]
        public async Task GetUsuarioLoginAsync_RecusaLoginUsernameIncorreto()
        {
            // Arrange
            const string usernameCorreto = "usuario";
            const string passwordCorreto = "123";
            const string jwtCorreto = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkdEIiwiaWF0IjoxNzU1Mjg5MTM1LCJleHAiOjE3NTUyOTI3MzV9.6Ds74kk74lehpTgWWfoqpv9lpa0Cqmft243NPqnF9e4";
            var usuarioCorreto = new Usuario()
            {
                Id = 1,
                Username = usernameCorreto,
                PasswordHash = Util.Util.HashPassword(null, passwordCorreto)
            };

            _mockRepository.Setup(p => p.GetByUsername(usernameCorreto)).ReturnsAsync(usuarioCorreto);
            _mockTokenService.Setup(p => p.GenerateToken(usuarioCorreto)).Returns(jwtCorreto);

            // Act
            var retorno = await _service.GetUsuarioLoginAsync("usernameErrado", passwordCorreto);

            // Assert
            Assert.Null(retorno.usuario);
            Assert.Null(retorno.jwt);
        }

        [Fact]
        public async Task GetUsuarioLoginAsync_RecusaLoginPasswordIncorreto()
        {
            // Arrange
            const string usernameCorreto = "usuario";
            const string passwordCorreto = "123";
            const string jwtCorreto = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkdEIiwiaWF0IjoxNzU1Mjg5MTM1LCJleHAiOjE3NTUyOTI3MzV9.6Ds74kk74lehpTgWWfoqpv9lpa0Cqmft243NPqnF9e4";
            var usuarioCorreto = new Usuario()
            {
                Id = 1,
                Username = usernameCorreto,
                PasswordHash = Util.Util.HashPassword(null, passwordCorreto)
            };

            _mockRepository.Setup(p => p.GetByUsername(usernameCorreto)).ReturnsAsync(usuarioCorreto);
            _mockTokenService.Setup(p => p.GenerateToken(usuarioCorreto)).Returns(jwtCorreto);

            // Act
            var retorno = await _service.GetUsuarioLoginAsync(usernameCorreto, "passwordErrado");

            // Assert
            Assert.Null(retorno.usuario);
            Assert.Null(retorno.jwt);
        }

        [Fact]
        public async Task GetUsuarioLoginAsync_RecusaLoginUsernameEPasswordIncorretos()
        {
            // Arrange
            const string usernameCorreto = "usuario";
            const string passwordCorreto = "123";
            const string jwtCorreto = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkdEIiwiaWF0IjoxNzU1Mjg5MTM1LCJleHAiOjE3NTUyOTI3MzV9.6Ds74kk74lehpTgWWfoqpv9lpa0Cqmft243NPqnF9e4";
            var usuarioCorreto = new Usuario()
            {
                Id = 1,
                Username = usernameCorreto,
                PasswordHash = Util.Util.HashPassword(null, passwordCorreto)
            };

            _mockRepository.Setup(p => p.GetByUsername(usernameCorreto)).ReturnsAsync(usuarioCorreto);
            _mockTokenService.Setup(p => p.GenerateToken(usuarioCorreto)).Returns(jwtCorreto);

            // Act
            var retorno = await _service.GetUsuarioLoginAsync("usernameErrado", "passwordErrado");

            // Assert
            Assert.Null(retorno.usuario);
            Assert.Null(retorno.jwt);
        }

        [Fact]
        public async Task AddUsuarioAsync_QuandoUsernameNaoExiste_DeveCriarUsuario()
        {
            // Arrange
            var createUsuario = new CreateUsuario { Username = "usuarioInexistente", Password = "password" };
            var usuario = CreateUsuarioMapper.CreateUsuarioToUsuario(createUsuario);

            _mockRepository
                .Setup(r => r.GetByUsername(createUsuario.Username))
                .ReturnsAsync((Usuario)null);

            _mockRepository
                .Setup(r => r.Create(usuario))
                .Returns(Task.CompletedTask);

            // Act
            await _service.AddUsuarioAsync(createUsuario);

            // Assert
            _mockRepository.Verify(p => p.GetByUsername(createUsuario.Username), Times.Once);
            _mockRepository.Verify(p => p.Create(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public async Task AddUsuarioAsync_QuandoUsernameExiste_DeveLancarExcecao()
        {
            // Arrange
            var usuarioExistente = new Usuario { Username = "usuarioExistente" };
            var createUsuario = new CreateUsuario { Username = "usuarioExistente", Password = "password" };
            var novoUsuario = CreateUsuarioMapper.CreateUsuarioToUsuario(createUsuario);

            _mockRepository
                .Setup(r => r.GetByUsername(novoUsuario.Username))
                .ReturnsAsync(usuarioExistente);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(
                () => _service.AddUsuarioAsync(createUsuario)
            );

            Assert.Equal("Username inválido.", exception.Message);
            _mockRepository.Verify(r => r.GetByUsername(novoUsuario.Username), Times.Once);
            _mockRepository.Verify(r => r.Create(It.IsAny<Usuario>()), Times.Never);
        }
    }
}

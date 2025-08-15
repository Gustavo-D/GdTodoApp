using GdToDoApp.Server.Repositories.Interfaces;
using GdToDoApp.Server.Services;
using GdToDoApp.Server.Services.Interfaces;
using Moq;
using GdToDoApp.Server.Model;
using GdToDoApp.Server.Util;

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
                PasswordHash = Util.HashPassword(null, passwordCorreto)
            };

            _mockRepository.Setup(p => p.GetByUsername(usernameCorreto)).ReturnsAsync(usuarioCorreto);
            _mockTokenService.Setup(p => p.GenerateToken(usuarioCorreto)).Returns(jwtCorreto);

            // Act
            var retorno = await _service.GetUsuarioLoginAsync(usernameCorreto, passwordCorreto);

            //Assert
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
                PasswordHash = Util.HashPassword(null, passwordCorreto)
            };

            _mockRepository.Setup(p => p.GetByUsername(usernameCorreto)).ReturnsAsync(usuarioCorreto);
            _mockTokenService.Setup(p => p.GenerateToken(usuarioCorreto)).Returns(jwtCorreto);

            // Act
            var retorno = await _service.GetUsuarioLoginAsync("usernameErrado", passwordCorreto);

            //Assert
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
                PasswordHash = Util.HashPassword(null, passwordCorreto)
            };

            _mockRepository.Setup(p => p.GetByUsername(usernameCorreto)).ReturnsAsync(usuarioCorreto);
            _mockTokenService.Setup(p => p.GenerateToken(usuarioCorreto)).Returns(jwtCorreto);

            // Act
            var retorno = await _service.GetUsuarioLoginAsync(usernameCorreto, "passwordErrado");

            //Assert
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
                PasswordHash = Util.HashPassword(null, passwordCorreto)
            };

            _mockRepository.Setup(p => p.GetByUsername(usernameCorreto)).ReturnsAsync(usuarioCorreto);
            _mockTokenService.Setup(p => p.GenerateToken(usuarioCorreto)).Returns(jwtCorreto);

            // Act
            var retorno = await _service.GetUsuarioLoginAsync("usernameErrado", "passwordErrado");

            //Assert
            Assert.Null(retorno.usuario);
            Assert.Null(retorno.jwt);
        }
    }
}

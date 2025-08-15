using GdToDoApp.Server.Model;
using Microsoft.AspNetCore.Identity;

namespace GdToDoApp.Server.Util
{
    public static class Util
    {
        public static string HashPassword(Usuario usuario, string senha)
        {
            var passwordHasher = new PasswordHasher<Usuario>();
            var hash = passwordHasher.HashPassword(usuario, senha);

            return hash;
        }

        public static bool VerificarHashPassword(Usuario usuario, string hashSenha, string senhaProvidenciada)
        {
            var passwordHasher = new PasswordHasher<Usuario>();
            PasswordVerificationResult resultado = passwordHasher.VerifyHashedPassword(usuario, hashSenha, senhaProvidenciada);
            return resultado == PasswordVerificationResult.Failed ? false : true;
        }
    }
}

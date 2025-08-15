using GdToDoApp.Server.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace GdToDoApp.Server.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = SHA3_256.HashData(Util.Util.JwtSecret);
            var Expires = DateTime.UtcNow;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Username),
                    new Claim(ClaimTypes.SerialNumber, usuario.Id.ToString())
                }),

                Expires = Expires.AddHours(24),
                NotBefore = Expires,
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

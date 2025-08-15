using GdTodoApp.Server.Dtos.Shared;
using GdToDoApp.Server.Model;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Cryptography;

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

        public static Exception GetExcecaoRaiz(Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException(nameof(ex));
            }

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            return ex;
        }

        public static void GetResultadoApiFromException(HttpContext context, Exception ex, out ResultadoApi<object> resultadoApi)
        {
            var response = context != null ? context.Response : null;
            string rastreamento = ex.ToString();
            string friendlyErrorMessage = GetExcecaoRaiz(ex)?.Message ?? "Ocorreu um erro durante o processamento desta requisição.";
            HttpStatusCode statusCode = (HttpStatusCode?)response?.StatusCode ?? HttpStatusCode.InternalServerError;


            statusCode = statusCode == HttpStatusCode.OK ? HttpStatusCode.InternalServerError : statusCode;
            var possuiStatusCodeMensagem = friendlyErrorMessage.IndexOf('[') > -1 &&
                                            friendlyErrorMessage.IndexOf('[') < friendlyErrorMessage.IndexOf(']') &&
                                            friendlyErrorMessage.IndexOf(']') > 1;
            int statusCodeMensagem = 0;
            bool resultadoParse = possuiStatusCodeMensagem ? int.TryParse
                                    (
                                    friendlyErrorMessage.AsSpan(friendlyErrorMessage.IndexOf("[") + 1, friendlyErrorMessage.IndexOf("]") - 1),
                                    out statusCodeMensagem
                                    ) : false;
            statusCode = resultadoParse ? (HttpStatusCode)statusCodeMensagem : statusCode;

            if (context != null)
            {
                context.Response.StatusCode = (int)statusCode;
            }

            friendlyErrorMessage = possuiStatusCodeMensagem ? friendlyErrorMessage.Substring(friendlyErrorMessage.IndexOf("]") + 1) : friendlyErrorMessage;

            resultadoApi = new()
            {
                Meta = new()
                {
                    Status = statusCode,
                    Errors = [new()
                    {
                        QueryString = context != null ? new Uri(context.Request.GetDisplayUrl()).OriginalString : null,
                        FriendlyErrorMessage = friendlyErrorMessage,
                    }]
                }
            };
        }
    }
}

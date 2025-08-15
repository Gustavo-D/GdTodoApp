using GdTodoApp.Server.Dtos.Shared;
using System.Text.Json;

namespace GdTodoApp.Server.Middlewares
{
    public class MyExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public MyExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exceptionObj)
            {
                await HandleExceptionAsync(context, exceptionObj);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            /* Em uma aplicação real seria necessário avaliar o volume de requisições
               para saber se é possível utilizar exceções para lidar mesmo com erros
               conhecidos. Caso o volume de requisições não seja muito grande e/ou os
               recursos disponíveis para a aplicação consigam suportar este comportam
               ento, não vejo problema em ter um possível impacto no desempenho em no
               me de um desenvolvimento mais direto do fluxo de error-handling.       */
            Util.Util.GetResultadoApiFromException(context, ex, out ResultadoApi<object> resultadoApi);

            var resultado = JsonSerializer.Serialize(resultadoApi);
            context.Response.Headers["Content-Type"] = "application/json; charset=utf-8";
            return context.Response.WriteAsync(resultado!);
        }
    }
}

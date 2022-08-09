using FinanceApp.Api.Interfaces;
using FinanceApp.Api.Models;
using FinanceApp.Api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FinanceApp.Api.Middleware
{
    /// <summary>
    /// Tratamento global para as exceptions da aplicação
    /// </summary>
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;


        public CustomExceptionMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext httpContext, IServiceProvider serviceProvider, ILog log)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Cria o objeto de retorno
        /// </summary>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // status code
            int statusCode = Validacoes.BuscaStatusCode(exception.Message);

            // verifica se o banco esta conectado
            // validação para evitar tentar gravar o log no banco
            if (statusCode != 1045)
            {
                RegistraLog(context, exception, statusCode);
            }

            statusCode = statusCode == 1045 ? StatusCodes.Status500InternalServerError : statusCode;

            // Retorno
            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(
                new { StatusCode = statusCode, ErrorMessage = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }        

        #region Métodos auxiliares    

        /// <summary>
        /// Registra o Log
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="exception">Exception</param>
        /// <param name="statusCode">int</param>
        private void RegistraLog(HttpContext context, Exception exception, int statusCode)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                ILog _log;
                _log = scope.ServiceProvider.GetRequiredService<ILog>();

                // monta log
                Log log = new Log
                {
                    Acao = $"{context.Request.Method} {context.Request.Path.Value}".ToString(),
                    Retorno = $"{exception.Message}".ToString(),
                    StatusCode = statusCode,
                    UsuarioNome = "AdministradorSistema",
                    DataAcao = DateTime.Now,
                    RemoteIpCliente = _log.ObterIpClienteRemote()
                };
                _log.GravarLog(log);
            }
        }

        #endregion
    }
}

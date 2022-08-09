using Newtonsoft.Json;

namespace FinanceApp.Api.Utils
{
    public class RetornoAPI
    {
        public int StatusCode { get; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }
        public RetornoAPI(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }
        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 200:
                    return "Operação realizada com sucesso.";
                case 401:
                    return "Não autorizado (não autenticado).";
                case 404:
                    return "Recurso não encontrado.";
                case 405:
                    return "Método não permitido.";
                case 500:
                    return "Um erro não tratado ocorreu no request.";
                default:
                    return null;
            }
        }
    }
}

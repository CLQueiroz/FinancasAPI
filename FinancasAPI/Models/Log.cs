using System;

namespace FinanceApp.Api.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Acao { get; set; }
        public string Retorno { get; set; }
        public int StatusCode { get; set; }
        public string UsuarioNome { get; set; }
        public DateTime DataAcao { get; set; }
        public string RemoteIpCliente { get; set; }
    }
}

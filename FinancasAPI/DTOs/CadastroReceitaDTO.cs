using System;

namespace FinanceApp.Api.Interfaces
{
    public class CadastroReceitaDTO
    {
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public int UsuarioId { get; set; }
        public double Valor { get; set; }
    }
}
using FinanceApp.Api.Models;
using System;
using System.Text.Json.Serialization;

namespace FinanceApp.Api.Models
{
    public class Despesa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public double Valor { get; set; }
        public int UsuarioId { get; set; }
        [JsonIgnore]
        public Usuario Usuario { get; set; }
        public bool Baixada { get; set; }
        public bool DespesaFixa { get; set; }
    }
}

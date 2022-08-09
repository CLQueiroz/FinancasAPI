using FinanceApp.Api.Utils;
using Newtonsoft.Json;
using System;

namespace FinanceApp.Api.DTOs
{
    public class CadastroDespesaDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public double Valor { get; set; }
        public int UsuarioId { get; set; }
        public bool DespesaFixa { get; set; }

        // verifica se os camppos estão preenchidos
        public bool Validar()
        {
            return !string.IsNullOrEmpty(Descricao) && !string.IsNullOrEmpty(Valor.ToString()) && !string.IsNullOrEmpty(DataVencimento.ToString());
        }
    }
}

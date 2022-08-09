using System;
using System.Text.Json.Serialization;

namespace FinanceApp.Api.Models
{
    /// <summary>
    /// Model de receitat
    /// </summary>
    public class Receita
    {
        /// <summary>
        /// Variável do tipo INT, auto incremento no banco.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Variável do tipo string.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Variável do tipo decimal
        /// </summary>
        public double Valor { get; set; }

        /// <summary>
        /// Variável do tipo DateTime
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Id do usuário
        /// </summary>
        public int UsuarioId { get; set; }

        [JsonIgnore]
        public Usuario Usuario { get; set; }
    }
}

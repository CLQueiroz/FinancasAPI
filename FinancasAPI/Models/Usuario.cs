using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinanceApp.Api.Models
{
    /// <summary>
    /// Model de usuários
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Variável do tipo INT, auto increment no banco.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// variável do tipo string.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// variável do tipo string.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// variável do tipo string.
        /// </summary>
        [JsonIgnore]
        public string Senha { get; set; }


        [JsonIgnore]
        public List<Receita> Receitas { get; set; }

        [JsonIgnore]
        public List<Despesa> Despesas { get; set; }
    }
}

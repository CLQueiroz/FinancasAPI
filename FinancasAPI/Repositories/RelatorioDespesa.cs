using FinanceApp.Api.Exceptions;
using FinanceApp.Api.Interfaces;
using FinanceApp.Api.Mensagens;
using FinanceApp.Api.Models;
using FinanceApp.Api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceApp.Api.Repositories
{
    public class RelatorioDespesa
    {
        private readonly IDespesa _despesa;

        public RelatorioDespesa(IDespesa despesa)
        {
            _despesa = despesa;
        }

        /// <summary>
        /// Retorna a listagem de despesas por pessoa
        /// </summary>
        public List<Despesa> ListagemPorPessoa(string usuarioNome)
        {
            try
            {
                List<Despesa> listaDespesas = new List<Despesa>();
                if (Validacoes.isNotNull(usuarioNome))
                {
                    listaDespesas = _despesa.BuscaDespesas().Where(e => e.Usuario.Nome.ToUpper() == usuarioNome.ToUpper()).ToList();
                }
                return listaDespesas.Count > 0 ? listaDespesas : throw new DomainException(MensagemRetorno.NaoExisteRegistros);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

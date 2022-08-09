using FinanceApp.Api.Interfaces;
using FinanceApp.Api.Models;
using FinanceApp.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        private readonly IDespesa _despesa;

        public RelatoriosController(IDespesa despesa)
        {
            _despesa = despesa;
        }

        /// <summary>
        /// Retorna despesas do usuário pelo nome
        /// </summary>
        [HttpGet("{nomeusuario}")]
        public List<Despesa> Get(string nomeUsuario)
        {
            var relatorio = new RelatorioDespesa(_despesa);
            return relatorio.ListagemPorPessoa(nomeUsuario);
        }
    }
}

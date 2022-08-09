using FinanceApp.Api.Interfaces;
using FinanceApp.Api.Mensagens;
using FinanceApp.Api.Models;
using FinanceApp.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FinanceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitasController : ControllerBase
    {
        private readonly IReceita _receita;


        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="receita"></param>
        public ReceitasController(IReceita receita)
        {
            _receita = receita;
        }


        /// <summary>
        /// Retornar todos as receitas
        /// </summary>
        /// <returns>Um List de Receita</returns>
        [HttpGet]
        public ActionResult<List<Receita>> GetReceitas()
        {
            return _receita.BuscaReceitas();            
        }


        /// <summary>
        /// Retorna uma receita pelo ID
        /// </summary>
        /// <param name="id">Parametro do tipo INT</param>
        /// <returns>Uma receita</returns>
        [HttpGet("{id}")]
        public ActionResult<Receita> GetReceita(int id)
        {
            return _receita.BuscaReceita(id);
        }


        /// <summary>
        /// Cadastra a receita
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PostReceita(CadastroReceitaDTO modelo)
        {
            _receita.CadastraReceita(modelo);
            return Ok(new RetornoAPI(200, MensagemRetorno.CadastroSucesso));            
        }


        /// <summary>
        /// Deletar receita
        /// </summary>
        /// <param name="id">tipo int</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteReceita(int id)
        {
            _receita.DeletaReceita(id);
            return Ok(new RetornoAPI(200, MensagemRetorno.DeleteSucesso));
        }
    }
}

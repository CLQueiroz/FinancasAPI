using FinanceApp.Api.DTOs;
using FinanceApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using FinanceApp.Api.Utils;
using FinanceApp.Api.Mensagens;
using FinanceApp.Api.Interfaces;

namespace FinanceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DespesasController : ControllerBase
    {
        private readonly IDespesa _despesa;
        private readonly ILog _log;


        /// <summary>
        /// Construtor
        /// </summary>
        public DespesasController(IDespesa despesa, ILog log)
        {
            _despesa = despesa;
            _log = log;
        }

        
        /// <summary>
        /// Retornar todos as despesas
        /// </summary>
        /// <returns>Um List de Despesa</returns>
        [HttpGet]
        public List<Despesa> GetDespesas()
        {  
            return _despesa.BuscaDespesas(); 
        }


        /// <summary>
        /// Retorna uma despesa pelo ID
        /// </summary>
        /// <param name="id">Parametro do tipo INT</param>
        /// <returns>Uma despesa</returns>
        [HttpGet("{id}")]
        public ActionResult<Despesa> GetDespesa(int id)
        {
            return _despesa.BuscaDespesa(id);
        }       

        /// <summary>
        /// Cadastra a despesa
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PostDespesa(CadastroDespesaDTO modelo)
        {
            try
            {
                _despesa.CadastraDespesa(modelo);
                return Ok(new RetornoAPI(200, MensagemRetorno.CadastroSucesso));            
            }
            catch (Exception e)
            {
                return BadRequest(new RetornoAPI(StatusCodes.Status400BadRequest, e.Message));
            }
        }

        /// <summary>
        /// Baixa a despesa
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("baixar")]
        public ActionResult BaixaDespesa(CadastroDespesaDTO model)
        {
            try
            {
                _despesa.BaixaDespesa(model.Id);
                return Ok(new RetornoAPI(200, MensagemRetorno.DespesaBaixadaSucesso));
            }
            catch (Exception e)
            {
                return BadRequest(new RetornoAPI(StatusCodes.Status400BadRequest, e.Message));
            }
        }

        /// <summary>
        /// Deletar despesa
        /// </summary>
        /// <param name="id">tipo int</param>
        /// <returns></returns>
        [HttpPost]
        [Route("deletar/{id}")]
        public IActionResult DeleteDespesa(int id)
        {
            _despesa.DeletaDespesa(id);
            return Ok(new RetornoAPI(200, MensagemRetorno.DeleteSucesso));    
        }
    }
}

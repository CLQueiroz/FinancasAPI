using FinanceApp.Api.Interfaces;
using FinanceApp.Api.Models;
using FinanceApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumoController : ControllerBase
    {
        private readonly IResumo _resumo;

        public ResumoController(IResumo resumo)
        {
            _resumo = resumo;
        }

        /// <summary>
        /// ...
        /// </summary>
        /// <returns>Um List de Log</returns>
        [HttpGet]
        public Resumo GetResumo()
        {
            return _resumo.BuscaResumo();
        }        
    }
}

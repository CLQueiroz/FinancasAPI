using FinanceApp.Api.Interfaces;
using FinanceApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILog _log;

        public LogController(ILog log)
        {
            _log = log;
        }

        /// <summary>
        /// Retorna a lista de logs no formato JSON
        /// </summary>
        /// <returns>Um List de Log</returns>
        [HttpGet]
        public List<Log> GetLogs() => _log.BuscarLogs();


        /// <summary>
        /// Cria um arquivo TXT com os logs
        /// </summary>
        [HttpPost]
        public void PostLogTXT() => _log.GerarLogsFile();
    }
}

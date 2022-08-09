using FinanceApp.Api.Models;
using System.Collections.Generic;

namespace FinanceApp.Api.Interfaces
{
    public interface ILog
    {
        public List<Log> BuscarLogs();
        public void GravarLog(Log modelo);
        public void GerarLogsFile();
        public void MontaLog(string acao, string retorno);
        public string ObterIpClienteRemote();
    }
}

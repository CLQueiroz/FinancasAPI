using FinanceApp.Api.Contexto;
using FinanceApp.Api.Exceptions;
using FinanceApp.Api.Interfaces;
using FinanceApp.Api.Mensagens;
using FinanceApp.Api.Models;
using FinanceApp.Api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace FinanceApp.Api.Services
{
    public class LogService : ILog
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _config;

        public LogService(ApplicationContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        /// <summary>
        /// Busca todos os logs
        /// </summary>
        /// <returns>Retorna a lista de logs em formato JSON</returns>
        public List<Log> BuscarLogs()
        {
            try
            {
                List<Log> logs = _context.Log.ToList();
                if (Validacoes.isNotNull(logs))
                {
                    return logs;
                }
                throw new DomainException(MensagemRetorno.NaoExisteRegistros);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Grava o log no banco
        /// </summary>
        /// <param name="modelo"></param>
        public void GravarLog(Log modelo)
        {
            try
            {
                if (Validacoes.isNotNull(modelo))
                {
                    _context.Log.Add(modelo);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        #region Métodos auxiliares

        /// <summary>
        /// Monta o log e gera o mesmo
        /// </summary>
        public void MontaLog(string acao, string retorno)
        {
            try
            {
                if(acao != "") 
                {            
                    GravarLog(new Log
                    {
                        Acao = acao,
                        Retorno = retorno,
                        StatusCode = StatusCodes.Status200OK,
                        UsuarioNome = "AdministradorSistema",
                        DataAcao = DateTime.Now,
                        RemoteIpCliente = ObterIpClienteRemote()
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gera o Log em TXT
        /// </summary>
        public void GerarLogsFile()
        {
            string caminhoArquivoLog = string.Concat(_config.GetValue<string>("Paths:PathLog"), DateTime.Now.ToString("yy-mm-dd h m"), ".txt");
            List<string> linhaLogString = new List<string>();

            foreach (var item in BuscarLogs())
            {
                linhaLogString.Add($"{item.DataAcao} - {item.StatusCode} - {item.Acao} - {item.Retorno} - {item.UsuarioNome}");
            }

            if (!File.Exists(caminhoArquivoLog))
            {
                FileStream fileCreated = File.Create(caminhoArquivoLog);
                fileCreated.Close();
            }

            using (StreamWriter tw = new StreamWriter(caminhoArquivoLog, true))
            {
                linhaLogString.ForEach(log => tw.WriteLine(log));
            }
        }

        /// <summary>
        /// Obtem o ip do cliente
        /// </summary>
        public string ObterIpClienteRemote()
        {
            var heServer = Dns.GetHostEntry(Dns.GetHostName());
            return heServer.AddressList[3].ToString();
        }


        #endregion
    }
}
 

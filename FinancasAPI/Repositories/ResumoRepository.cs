using FinanceApp.Api.Contexto;
using FinanceApp.Api.Exceptions;
using FinanceApp.Api.Interfaces;
using FinanceApp.Api.Mensagens;
using FinanceApp.Api.Models;
using FinanceApp.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySqlConnector;
using FinanceApp.Api.Models;

namespace FinanceApp.Api.Repositories
{
    public class ResumoRepository : IResumo
    {
        private readonly IConfiguration _config;

        public ResumoRepository(IConfiguration config)
        {
            _config = config;
        }

        public Resumo BuscaResumo()
        {
            Resumo resumo = new Resumo();
            using (MySqlConnection connection = new MySqlConnection(_config.GetConnectionString("Default")))
            {
                resumo = connection.Query<Resumo>("SELECT * FROM vw_dashboard", buffered: false).FirstOrDefault();
            }

            return resumo;
        }
    }
}

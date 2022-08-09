using FinanceApp.Api.DTOs;
using FinanceApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FinanceApp.Api.Interfaces
{
    public interface IResumo
    {
        public Resumo BuscaResumo();
    }
}

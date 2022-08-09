using FinanceApp.Api.DTOs;
using FinanceApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FinanceApp.Api.Interfaces
{
    public interface IDespesa
    {
        public List<Despesa> BuscaDespesas();

        public Despesa BuscaDespesa(int id);

        public IActionResult AtualizaDespesa();

        public bool DeletaDespesa(int id);

        public bool BaixaDespesa(int id);

        public Despesa CadastraDespesa(CadastroDespesaDTO modelo);
    }
}

using FinanceApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FinanceApp.Api.Interfaces
{
    public interface IReceita
    {
        public List<Receita> BuscaReceitas();

        public Receita BuscaReceita(int id);

        public IActionResult AtualizaReceita();

        public bool DeletaReceita(int id);

        public Receita CadastraReceita(CadastroReceitaDTO modelo);

        public bool ReceitaExiste(int id);
    }
}

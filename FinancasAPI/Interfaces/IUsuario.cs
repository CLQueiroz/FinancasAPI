using FinanceApp.Api.Models;
using FinanceApp.Api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FinanceApp.Api.Interfaces
{
    public interface IUsuario
    {
        public List<Usuario> BuscaUsuarios();

        public Usuario BuscaUsuario(int id);

        public IActionResult AtualizaUsuario();

        public bool DeletaUsuario(int id);
        
        public Usuario CadastraUsuario(CadastroUsuarioDTO cadastroUsuarioDTO);

        public bool UsuarioExiste(int id);
    }
}

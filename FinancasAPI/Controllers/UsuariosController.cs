using FinanceApp.Api.Interfaces;
using FinanceApp.Api.Mensagens;
using FinanceApp.Api.Models;
using FinanceApp.Api.Models.DTOs;
using FinanceApp.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FinanceApp.Api.Controllers
{
    /// <summary>
    /// Controller Usuário.
    /// </summary>
    [ApiController]
    [Route("api/[controller]/")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuario _usuario;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="usuarioRepository"></param>
        public UsuariosController(IUsuario usuarioRepository)
        {
            _usuario = usuarioRepository;
        }

        /// <summary>
        /// Método para retornar todos usuários cadastrados.
        /// </summary>
        /// <returns>Uma lista de Usuários</returns>
        [HttpGet]
        public ActionResult <List<Usuario>> GetUsuarios()
        {
            return _usuario.BuscaUsuarios();
        }

        /// <summary>
        /// Método para retornar usuário pelo Id.
        /// </summary>
        /// <returns>Um usuário</returns>
        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            return _usuario.BuscaUsuario(id);            
        }

        /// <summary>
        /// Cadastrar um novo usuário.
        /// </summary>
        /// <param name="cadastroUsuarioDTO">Parametro do CadastroUsuarioDTO</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PostUsuario(CadastroUsuarioDTO cadastroUsuarioDTO)
        {
            _usuario.CadastraUsuario(cadastroUsuarioDTO);
            return Ok(new RetornoAPI(200, MensagemRetorno.CadastroSucesso));
        }

        /// <summary>
        /// Deleta um usuário.
        /// </summary>
        /// <param name="id">parametro do tipo int</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            _usuario.DeletaUsuario(id);
            return Ok(new RetornoAPI(200, MensagemRetorno.DeleteSucesso));            
        }       
    }
}

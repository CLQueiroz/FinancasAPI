using FinanceApp.Api.Mensagens;
using FinanceApp.Api.Utils;
using FinanceApp.Api.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public ActionResult Login(LoginDTO loginDTO)
        {
            if (Validacoes.isNotNull(loginDTO))
            {
                if (loginDTO.Email == "caiqueonisto@live.com" && loginDTO.Senha == "123")
                {
                    return Ok(new { Message = MensagemRetorno.SucessoLogar, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Message = MensagemRetorno.UsuarioSenhaInvalidos, StatusCode = StatusCodes.Status401Unauthorized });
            }else
            {
                return BadRequest();
            }
        }

    }
}

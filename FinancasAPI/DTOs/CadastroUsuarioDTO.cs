namespace FinanceApp.Api.Models.DTOs
{
    /// <summary>
    /// Modelo para cadastrar o Usuário
    /// </summary>
    public class CadastroUsuarioDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}

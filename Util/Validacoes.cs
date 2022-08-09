using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FinanceApp.Api.Utils
{
    /// <summary>
    /// Validações globais da aplicação
    /// </summary>
    public static class Validacoes
    {
        /// <summary>
        /// Verifica se é null
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        public static bool IsNull<T>(T modelo)
        {
            if (modelo == null) 
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se não está null
        /// </summary>
        /// <param name="modelo"></param>
        public static bool isNotNull<T>(T modelo)
        {
            return !IsNull(modelo);
        }


        /// <summary>
        /// Verifica se o retorno é zero
        /// </summary>
        /// <returns>True ou false</returns>
        public static bool IsZero<T>(T cont)
        {
            return cont.Equals(0);
        }

        /// <summary>
        /// Verifica se o e-mail é válido
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool EmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Busca o status code através da mensagem do exception
        /// </summary>
        public static int BuscaStatusCode(string message)
        {
            int statusCode = 0;
            switch (message)
            {
                case string a when a.Contains("cadastrado com sucesso"):
                    statusCode = StatusCodes.Status201Created;
                    break;

                case string b when b.Contains("Todos os campos são obrigatórios"):
                    statusCode = StatusCodes.Status406NotAcceptable;
                    break;

                case string c when c.Contains("não encontrado"):
                    statusCode = StatusCodes.Status404NotFound;
                    break;

                case string d when d.Contains("Não existem registros"):
                    statusCode = StatusCodes.Status404NotFound;
                    break;

                case string e when e.Contains("deletado com sucesso"):
                    statusCode = StatusCodes.Status200OK;
                    break;

                case string f when f.Contains("Não existem registros"):
                    statusCode = StatusCodes.Status404NotFound;
                    break;

                case string g when g.Contains("Formato inválido"):
                    statusCode = StatusCodes.Status400BadRequest;
                    break;

                case string h when h.Contains("operação não permitida"):
                    statusCode = StatusCodes.Status400BadRequest;
                    break;

                case string i when i.Contains("Access denied for user"):
                    statusCode = 1045;
                    break;

                case string j when j.Contains("Formato do e-mail inválido"):
                    statusCode = StatusCodes.Status406NotAcceptable;
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            return statusCode;
        }
    }
}

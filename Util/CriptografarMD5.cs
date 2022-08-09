using System.Security.Cryptography;
using System.Text;

namespace FinanceApp.Util
{
    public static class CriptografarMD5
    {
        /// <summary>
        /// Classe criada para realizar a criptografia da senha.
        /// </summary>
        public static string Criar(string senha)
        {
            MD5 md5Hash = MD5.Create();

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}

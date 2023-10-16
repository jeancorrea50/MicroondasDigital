using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Domain.Security
{
    public static class CriptografaSHA256
    {
        public static string CriptografarSenha(string senha)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] senhaBytes = Encoding.UTF8.GetBytes(senha);

                byte[] hashBytes = sha256.ComputeHash(senhaBytes);

                string senhaString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                return senhaString;
            }
        }
        public static bool ValidaSenha(string senha, string hashedSenha)
        {
            string inputSenha = CriptografarSenha(senha);
            return inputSenha == hashedSenha;
        }
    }
}

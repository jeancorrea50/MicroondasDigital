using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Domain.Extensions.Exceptions
{
    public static class SalvarExceptions
    {

        public static void CriarArquivoTexto(string texto, string inner = null, string stacktrace = null)
        {
            string diretorio = @"c:\Temp";
            string nomeArquivo = "Exceptions.txt";
            string caminhoCompleto = Path.Combine(diretorio, nomeArquivo);

            // Verifica se o diretório existe, caso contrário, cria o diretório
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            // Escreve a mensagem no arquivo de texto
            using (StreamWriter writer = new StreamWriter(caminhoCompleto, true))
            {
                writer.WriteLine($"mensagem: {texto} inner: {inner} stacktrace: {stacktrace}// {DateTime.Now}");
            }
        }
    }
}

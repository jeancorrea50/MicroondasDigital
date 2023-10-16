using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Domain.Extensions.Exceptions
{
    public class FalhaServidorException : Exception
    {
        public FalhaServidorException(string additionalInfo = null) : base($"Falha no Servidor.")
        {
            SalvarExceptions.CriarArquivoTexto(Message);
        }

        public FalhaServidorException(string message, Exception innerException) : base(message, innerException)
        {
            SalvarExceptions.CriarArquivoTexto(message, innerException?.InnerException?.ToString(), innerException.StackTrace);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Domain.Extensions.Exceptions
{
    public class PadraoException : Exception
    {
        public PadraoException(string additionalInfo = null) : base($"Não foi possível processeguir, {additionalInfo}")
        {
            SalvarExceptions.CriarArquivoTexto(Message);
        }

        public PadraoException(string message, Exception innerException) : base(message, innerException)
        {
            SalvarExceptions.CriarArquivoTexto(message, innerException?.InnerException?.ToString(), innerException.StackTrace);
        }
    }
}

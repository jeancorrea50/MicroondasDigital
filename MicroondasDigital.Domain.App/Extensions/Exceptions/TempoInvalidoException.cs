using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Domain.Extensions.Exceptions
{
    public class TempoInvalidoException : Exception
    {
        public TempoInvalidoException(string additionalInfo = null) : base($"Não foi possível adicionar o tempo, o tempo deve ficar entre 1 e 120 segundos. {additionalInfo}")
        {
            SalvarExceptions.CriarArquivoTexto(Message);
        }

        public TempoInvalidoException(string message, Exception innerException) : base(message, innerException)
        {
            SalvarExceptions.CriarArquivoTexto(Message, innerException.Message, innerException.StackTrace);
        }
    }
}

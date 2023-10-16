using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Domain.Extensions.Exceptions
{
    public class ModelStateException : Exception
    {
        public string ModelStateKey { get; }

        public ModelStateException(string message, string modelStateKey) : base(message)
        {
            ModelStateKey = modelStateKey;
            SalvarExceptions.CriarArquivoTexto(Message);
        }
        public ModelStateException(string message, Exception innerException) : base(message, innerException)
        {
            SalvarExceptions.CriarArquivoTexto(message, innerException?.InnerException?.ToString(), innerException.StackTrace);
        }
    }
}


using System;
using System.ComponentModel.DataAnnotations;

namespace MicroondasDigital.Domain.Extensions.Attributes
{
    public class TempoValidation : ValidationAttribute
    {
        private readonly int tempoMaxima;
        public TempoValidation(int tempoMaxima)
        {
            this.tempoMaxima = tempoMaxima;
        }

        public override bool IsValid(object valor)
        {
            int tempoInformado = Convert.ToInt32(valor);

            if (valor == null)
            {
                return true;
            }

            else if (tempoInformado >= 1 && tempoInformado <= tempoMaxima)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace MicroondasDigital.Domain.Extensions.Attributes
{
    public class PotenciaValidation : ValidationAttribute
    {
        private readonly int potenciaMaxima;

        public PotenciaValidation(int potenciaMaxima)
        {
            this.potenciaMaxima = potenciaMaxima;
        }

        public override bool IsValid(object valor)
        {
            int potenciaInformado = Convert.ToInt32(valor);

            if (valor == null)
            {
                return true;
            }
            else if (potenciaInformado >= 1 && potenciaInformado <= potenciaMaxima)
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

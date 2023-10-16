using MicroondasDigital.Domain.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Domain.Dtos
{
    public class MicroondasNivel1Dto
    {
        [TempoValidation(120, ErrorMessage = "O tempo deve estar entre 1 e 120 segundos.")]
        public int? Tempo { get; set; }
        [PotenciaValidation(10, ErrorMessage = "a potência deve estar entre 1 e 10.")]
        public int? Potencia { get; set; }
        public string Progresso { get; set; }
        [DisplayName("Tempo Atual")]
        public string TempoAtual { get; set; }
    }
}

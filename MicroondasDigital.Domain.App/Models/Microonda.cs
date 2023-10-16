using MicroondasDigital.Domain.Extensions.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MicroondasDigital.Domain.Models
{
    [JsonObject]
    public class Microonda : ProgramaAquecimento
    {
        [Required(ErrorMessage ="Campo obrigatório")]
        public TimeSpan Tempo { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]

        [PotenciaValidation(10,ErrorMessage = "Campo obrigatório")]
        public int Potencia { get; set; }
    }
}

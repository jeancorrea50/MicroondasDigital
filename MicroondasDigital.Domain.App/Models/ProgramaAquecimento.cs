using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Domain.Models
{
    [JsonObject]
    public class ProgramaAquecimento : Base
    {
        [Required(ErrorMessage = "Campo obrigatórios")]
        public string Alimento { get; set; }

        [Required(ErrorMessage = "Campo obrigatórios")]
        [DisplayName("String Aquecimento")]
        [MaxLength(1,ErrorMessage ="O maximo de caractere é 1")]
        public string StringAquecimento { get; set; }

        [DisplayName("Instruções")]
        public string Instrucoes { get; set; }
    }
}

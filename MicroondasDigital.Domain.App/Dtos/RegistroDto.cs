using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MicroondasDigital.Domain.Dtos
{
    public class RegistroDto
    {
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [DisplayFormat(DataFormatString = "********")]
        public string Senha { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Domain.Models
{
    public class Base
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatórios")]
        public string Nome { get; set; }
    }
}

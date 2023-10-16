using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MicroondasDigital.Domain.Models
{
    public class Usuario
    {
        public Usuario(Guid id, string senha)
        {
            Id = id;
            Senha = senha;
        }

        public Guid Id { get; set; }
        public string Senha { get; set; }
    }
}

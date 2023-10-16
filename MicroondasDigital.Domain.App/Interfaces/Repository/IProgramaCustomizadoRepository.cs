using MicroondasDigital.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Domain.Interfaces.Repository
{
    public interface IProgramaCustomizadoRepository
    {
        bool Salvar(Microonda item);

        List<Microonda> ObterProgramasCustomizados();
    }
}

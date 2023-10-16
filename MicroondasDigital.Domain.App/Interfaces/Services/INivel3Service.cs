using MicroondasDigital.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Domain.Interfaces.Services
{
    public interface INivel3Service
    {
        IEnumerable<Microonda> CarregaListaProgramas();
        string SalvarPreAquecimento(Microonda model);
        string IniciarAquecimento(Microonda microondas);
        string PausarAquecimento();
    }
}

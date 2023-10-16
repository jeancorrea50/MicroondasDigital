using MicroondasDigital.Domain.Dtos;
using MicroondasDigital.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Domain.Interfaces.Services
{
    public interface INivel2Service
    {
        IEnumerable<Microonda> CarregaListaPreDefinida();
        string IniciarAquecimento(Microonda microondas);
        string PausarAquecimento();
    }
}

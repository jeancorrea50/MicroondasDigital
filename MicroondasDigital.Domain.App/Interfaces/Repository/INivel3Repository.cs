using MicroondasDigital.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Domain.Interfaces.Repository
{
    public interface INivel3Repository
    {
        IEnumerable<Microonda> ObterTodosProgramas();
        bool SalvaPreAquecimento(Microonda model);
        bool ValidaDuplicidadeCaractere(string caractere);

        Microonda ObterPorIdProgramaPreDefinido(int iDPreAquecimento);
    }
}

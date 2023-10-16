using MicroondasDigital.Domain.Interfaces.Repository;
using MicroondasDigital.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Data.Repository
{
    public class Nivel2Repository : INivel2Repository
    {
        private readonly IProgramaPreDefinidoRepository _programasPreDefinidoRepository;

        public Nivel2Repository(IProgramaPreDefinidoRepository programasPreDefinidoRepository)
        {
            _programasPreDefinidoRepository = programasPreDefinidoRepository;
        }

        public IEnumerable<Microonda> ObterProgramasPreDefinidos()
        {
            return _programasPreDefinidoRepository.ObterProgramasPreDefinidos();
        }

        public Microonda ObterPorIdProgramaPreDefinido(int iDPreAquecimento)
        {
            return _programasPreDefinidoRepository.ObterPorIdProgramaPreDefinido(iDPreAquecimento);
        }
    }
}

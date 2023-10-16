using MicroondasDigital.Domain.Interfaces.Repository;
using MicroondasDigital.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Data.Repository
{
    public class Nivel3Repository : INivel3Repository
    {
        private readonly IProgramaPreDefinidoRepository _programaPreDefinidoRepository;
        private readonly IProgramaCustomizadoRepository _programaCustomizadoRepository;

        public Nivel3Repository(IProgramaPreDefinidoRepository programaPreDefinidoRepository,
                                    IProgramaCustomizadoRepository programaCustomizadoRepository)
        {
            _programaPreDefinidoRepository = programaPreDefinidoRepository;
            _programaCustomizadoRepository = programaCustomizadoRepository;
        }

        public IEnumerable<Microonda> ObterTodosProgramas()
        {
            IEnumerable<Microonda> listaProgramasCustomizado = _programaCustomizadoRepository.ObterProgramasCustomizados();
            IEnumerable<Microonda> listaProgramasPreDefinidos = _programaPreDefinidoRepository.ObterProgramasPreDefinidos();

           return listaProgramasCustomizado.Concat(listaProgramasPreDefinidos);
        }

        public bool SalvaPreAquecimento(Microonda model)
        {
            return _programaCustomizadoRepository.Salvar(model);
        }

        public bool ValidaDuplicidadeCaractere(string caractere)
        {
           return ObterTodosProgramas().Any(x => x.StringAquecimento == caractere);
        }

        public Microonda ObterPorIdProgramaPreDefinido(int iDPreAquecimento)
        {
            var listaProgramas = ObterTodosProgramas();

            return listaProgramas.First(t => t.Id == iDPreAquecimento);
        }
    }
}

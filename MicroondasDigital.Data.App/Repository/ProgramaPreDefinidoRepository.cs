using MicroondasDigital.Domain.Interfaces.Repository;
using MicroondasDigital.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Data.Repository
{
    public class ProgramaPreDefinidoRepository : IProgramaPreDefinidoRepository
    {
        private List<Microonda> programas = new List<Microonda>();

        public ProgramaPreDefinidoRepository()
        {
            PopuleListaProgramasPreDefinidos();
        }

        public IEnumerable<Microonda> ObterProgramasPreDefinidos()
        {
            return programas;
        }

        public Microonda ObterPorIdProgramaPreDefinido(int iDPreAquecimento)
        {
            return programas.FirstOrDefault(x => x.Id == iDPreAquecimento);
        }

        private void PopuleListaProgramasPreDefinidos()
        {
            programas.Add(new Microonda
            {
                Id = 1,
                Nome = "Pipoca",
                Alimento = "Pipoca (de micro-ondas)",
                Tempo = TimeSpan.FromMinutes(3),
                Potencia = 7,
                StringAquecimento = "!",
                Instrucoes = "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento."
            });

            programas.Add(new Microonda
            {
                Id = 2,
                Nome = "Leite",
                Alimento = "Leite",
                Tempo = TimeSpan.FromMinutes(5),
                Potencia = 5,
                StringAquecimento = "#",
                Instrucoes = "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras."
            });

            programas.Add(new Microonda
            {
                Id = 3,
                Nome = "Carnes de boi",
                Alimento = "Carne em pedaço ou fatias",
                Tempo = TimeSpan.FromMinutes(14),
                Potencia = 4,
                StringAquecimento = "$",
                Instrucoes = "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."
            });

            programas.Add(new Microonda
            {
                Id = 4,
                Nome = "Frango",
                Alimento = "Frango (qualquer corte)",
                Tempo = TimeSpan.FromMinutes(8),
                Potencia = 7,
                StringAquecimento = "%",
                Instrucoes = "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."
            });

            programas.Add(new Microonda
            {
                Id = 5,
                Nome = "Feijão",
                Alimento = "Feijão congelado",
                Tempo = TimeSpan.FromMinutes(8),
                Potencia = 9,
                StringAquecimento = "*",
                Instrucoes = "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas."
            });
        }
    }
}

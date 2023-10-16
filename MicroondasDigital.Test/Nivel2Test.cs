using MicroondasDigital.Domain.Dtos;
using MicroondasDigital.Domain.Extensions.Hubs;
using MicroondasDigital.Domain.Interfaces.Repository;
using MicroondasDigital.Domain.Models;
using MicroondasDigital.Service.App;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xunit;

namespace MicroondasDigital.Test
{
    public class Nivel2Test
    {
        [Fact]
        public void ListaPreDefinida()
        {
            var nivel2RepositoryMock = new Mock<INivel2Repository>();
            var hubContextMock = new Mock<IHubContext<Nivel2Hub>>();
            var service = new Nivel2Service(nivel2RepositoryMock.Object, hubContextMock.Object);

            var programasPreDefinidos = new List<Microonda>
            {
            new Microonda { Nome = "Programa 1", Potencia = 10, Tempo = TimeSpan.FromMinutes(3) },
            new Microonda { Nome = "Programa 2", Potencia = 10, Tempo = TimeSpan.FromMinutes(1) }
            };

            nivel2RepositoryMock.Setup(repo => repo.ObterProgramasPreDefinidos()).Returns(programasPreDefinidos);

            var result = service.CarregaListaPreDefinida();

            Assert.NotNull(result); // Verifica se o resultado não é nulo
            Assert.Equal(programasPreDefinidos, result); // Verifica se o resultado é igual à lista simulada
        }

        [Fact]
        public void AquecimentoIniciar()
        {
            var nivel2RepositoryMock = new Mock<INivel2Repository>();
            var hubContextMock = new Mock<IHubContext<Nivel2Hub>>();
            var service = new Nivel2Service(nivel2RepositoryMock.Object, hubContextMock.Object);

            // Crie um objeto Microonda simulado que você espera que seja retornado pelo repositório
            var microondaSimulado = new Microonda
            {
                Tempo = TimeSpan.FromSeconds(20),
                Potencia = 800,
                StringAquecimento = "Aquecimento Simulado",
                Alimento = "Alimento Simulado",
                Instrucoes = "Instruções Simuladas"
            };

            // Act
            var result = service.IniciarAquecimento(microondaSimulado);

            // Assert
            Assert.Equal("Aquecimento Iniciado.", result);
        }

        [Fact]
        public void quecimentoAcrecentarTempoA()
        {
            var nivel2RepositoryMock = new Mock<INivel2Repository>();
            var hubContextMock = new Mock<IHubContext<Nivel2Hub>>();
            var service = new Nivel2Service(nivel2RepositoryMock.Object, hubContextMock.Object);

            var microondas = new Microonda
            {
                Tempo = TimeSpan.FromMinutes(1), // Tempo válido
                Potencia = 10 // Potência válida
            };

            Timer timer = new Timer(1000);
            service.timer = timer;
            service.tempoAtual = 10;
            timer.Start();

            // Act
            var result = service.IniciarAquecimento(microondas);

            // Assert
            Assert.Equal("Tempo Acrescentado ao Aquecimento.", result);
        }

        [Fact]
        public void AquecimentoReiniciar()
        {
            var nivel2RepositoryMock = new Mock<INivel2Repository>();
            var hubContextMock = new Mock<IHubContext<Nivel2Hub>>();
            var service = new Nivel2Service(nivel2RepositoryMock.Object, hubContextMock.Object);

            var microondas = new Microonda
            {
                Tempo = TimeSpan.FromMinutes(1), // Tempo válido
                Potencia = 10 // Potência válida
            };

            Timer timer = new Timer(1000);
            service.timer = timer;
            service.tempoAtual = 10;
            timer.Stop();

            // Act
            var result = service.IniciarAquecimento(microondas);

            // Assert
            Assert.Equal("Aquecimento Reiniciado.", result);
        }

        [Fact]
        public void AquecimentoPausado()
        {
            var nivel2RepositoryMock = new Mock<INivel2Repository>();
            var hubContextMock = new Mock<IHubContext<Nivel2Hub>>();
            var service = new Nivel2Service(nivel2RepositoryMock.Object, hubContextMock.Object);

            Timer timer = new Timer(1000);
            service.timer = timer;
            service.tempoAtual = 10;
            timer.Start();

            var result = service.PausarAquecimento();

            Assert.Equal("Aquecimento Pausado.", result);
        }
    }
}

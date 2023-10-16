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
    public class Nivel3Test
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
        public void IniciarAquecimento()
        {
            var hubContextMock = new Mock<IHubContext<Nivel1Hub>>();
            var service = new Nivel1Service(hubContextMock.Object);

            var microondas = new MicroondasNivel1Dto
            {
                Tempo = 30, // Tempo válido
                Potencia = 10 // Potência válida
            };

            // Act
            var result = service.IniciarAquecimento(microondas);

            // Assert
            Assert.Equal("Aquecimento Iniciado.", result);
        }

        [Fact]
        public void AcrecentarTempoAquecimento()
        {
            // Arrange
            var hubContextMock = new Mock<IHubContext<Nivel1Hub>>();
            var service = new Nivel1Service(hubContextMock.Object);

            var microondas = new MicroondasNivel1Dto
            {
                Tempo = 30, // Tempo válido
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
        public void ReiniciarAquecimento()
        {
            // Arrange
            var hubContextMock = new Mock<IHubContext<Nivel1Hub>>();
            var service = new Nivel1Service(hubContextMock.Object);

            var microondas = new MicroondasNivel1Dto
            {
                Tempo = 30, // Tempo válido
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
            // Arrange
            var hubContextMock = new Mock<IHubContext<Nivel1Hub>>();
            var service = new Nivel1Service(hubContextMock.Object);

            Timer timer = new Timer(1000);
            service.timer = timer;
            service.tempoAtual = 10;
            timer.Start();

            var result = service.PausarAquecimento();

            Assert.Equal("Aquecimento Pausado.", result);
        }
    }
}

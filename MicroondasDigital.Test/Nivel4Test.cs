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
using Xunit;

namespace MicroondasDigital.Test
{
    public class Nivel4Test
    {

        [Fact]
        public void Registrar_Sucesso()
        {
            // Arrange
            var nivel4RepositoryMock = new Mock<INivel4Repository>();
            var service = new Nivel4Service(nivel4RepositoryMock.Object);
            var registro = new RegistroDto { Senha = "senha" };

            // Configurar o mock para retornar true, simulando um registro de usuário bem-sucedido
            nivel4RepositoryMock.Setup(repo => repo.Registrar(registro.Senha)).Returns(true);

            var result = service.Registrar(registro);

            Assert.Equal("Usuário Registrado com sucesso!", result);
        }

        [Fact]
        public void Registrar_Falha()
        {
            // Arrange
            var nivel4RepositoryMock = new Mock<INivel4Repository>();
            var service = new Nivel4Service(nivel4RepositoryMock.Object);
            var registro = new RegistroDto { Senha = "senha" };

            // Configurar o mock para retornar false, simulando um registro de usuário com falha
            nivel4RepositoryMock.Setup(repo => repo.Registrar(registro.Senha)).Returns(false);

            var result = service.Registrar(registro);

            Assert.Equal("Erro ao salvar Usuário, por favor, tente novamente.", result);
        }

        [Fact]
        public void Login()
        {
            // Arrange
            var nivel4RepositoryMock = new Mock<INivel4Repository>();
            var service = new Nivel4Service(nivel4RepositoryMock.Object);
            var loginDto = new LoginDto { Senha = "senha" };
            var expectedResult = "Resultado do login simulado";

            // Configurar o mock para retornar o resultado simulado do login
            nivel4RepositoryMock.Setup(repo => repo.Login(loginDto.Senha)).Returns(expectedResult);

            var result = service.Login(loginDto);

            Assert.Equal(expectedResult, result);
        }
    }
}

using MicroondasDigital.Domain.Dtos;
using MicroondasDigital.Domain.Extensions.Hubs;
using MicroondasDigital.Domain.Interfaces.Repository;
using MicroondasDigital.Domain.Interfaces.Services;
using MicroondasDigital.Domain.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MicroondasDigital.Service.App
{
    public class Nivel4Service : INivel4Service
    {
        private readonly INivel4Repository _nivel4Repository;

        public Nivel4Service(INivel4Repository nivel4Repository)
        {
            _nivel4Repository = nivel4Repository;
        }

        public string Registrar(RegistroDto registro)
        {
            string mensagemRetorno = string.Empty;

            bool usuarioCriado = _nivel4Repository.Registrar(registro.Senha);

            if (usuarioCriado)
            {
              return  mensagemRetorno = "Usuário Registrado com sucesso!";
            }

            return mensagemRetorno = "Erro ao salvar Usuário, por favor, tente novamente.";
        }
             
        public string Login(LoginDto registro)
        {
            return _nivel4Repository.Login(registro.Senha);
        }
    }
}

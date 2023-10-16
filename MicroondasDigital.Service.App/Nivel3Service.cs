using MicroondasDigital.Domain.Extensions.Exceptions;
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
    public class Nivel3Service : INivel3Service
    {
        private readonly INivel3Repository _nivel3Repository;
        private readonly IHubContext<Nivel3Hub> _hubContext;

        public Timer timer;
        public int tempoAtual;
        private int potenciaAtual;
        private string stringAquecimento;
        private string progresso;
        private string alimento;
        private string instrucoes;

        public Nivel3Service(INivel3Repository nivel3Repository, IHubContext<Nivel3Hub> hubContext)
        {
            _nivel3Repository = nivel3Repository;
            _hubContext = hubContext;
        }

        public IEnumerable<Microonda> CarregaListaProgramas()
        {
            return _nivel3Repository.ObterTodosProgramas();
        }

        public string SalvarPreAquecimento(Microonda model)
       {
            string mensagemRetorno = string.Empty;

            if (model.StringAquecimento == ".")
            {
                throw new ModelStateException($"O caractere: '{model.StringAquecimento}' não pode ser definido, digite outro caractere.", "StringAquecimento");
            }

            if (_nivel3Repository.ValidaDuplicidadeCaractere(model.StringAquecimento))
            {
                throw new ModelStateException($"O caractere '{model.StringAquecimento}' duplicado, por favor digito outro.", "StringAquecimento");
            }

            if(_nivel3Repository.SalvaPreAquecimento(model))
            {
                mensagemRetorno = "Pre-Aquecimneto Personalisado Criado com sucesso!";
            }

            return mensagemRetorno;
        }

        public string IniciarAquecimento(Microonda microondas)
        {
            string mensagemRetorno = "Aquecimento Iniciado.";

            if (!InicializarTimer())
            {
                mensagemRetorno = "Tempo Acrescentado ao Aquecimento.";
            }

            if (tempoAtual > 0 && timer != null && !timer.Enabled)
            {
                timer.Start();

                mensagemRetorno = "Aquecimento Reiniciado";
            }
            else
            {
                SetaValoresPreDefinido(microondas.Id);
            }

            return mensagemRetorno;
        }

        public string PausarAquecimento()
        {
            string mensagemRetorno = string.Empty;

            if (tempoAtual > 0 && timer.Enabled)
            {
                timer.Stop();

                return mensagemRetorno = "Aquecimento Pausado."; ;
            }
            else
            {
                CancelarAquecimento();

                return mensagemRetorno =  "Aquecimento Cancelado.";
            }
        }

        private void SetaValoresPreDefinido(int iDPreAquecimento)
        {
            Microonda microondas = _nivel3Repository.ObterPorIdProgramaPreDefinido(iDPreAquecimento);

            progresso = string.Empty;
            tempoAtual = (int)(microondas.Tempo.TotalMinutes * 60);
            potenciaAtual = microondas.Potencia;
            stringAquecimento = microondas.StringAquecimento;
            alimento = microondas.Alimento;
            instrucoes = microondas.Instrucoes;
        }

        private void CancelarAquecimento()
        {
            tempoAtual = 0;
            potenciaAtual = 0;
            stringAquecimento = string.Empty;
            alimento = string.Empty;
            instrucoes = string.Empty;
            progresso = string.Empty;

            _hubContext.Clients.All.SendAsync("SendMessage", "", "", "", "", "");
        }

        private bool InicializarTimer()
        {
            if (tempoAtual == 0)
            {
                tempoAtual = 0;
                potenciaAtual = 0;
                stringAquecimento = string.Empty;
                alimento = string.Empty;
                instrucoes = string.Empty;

                timer = new Timer(1000);
                timer.Elapsed += Timer_Elapsed;
                timer.Start();

                return true;
            }
            return false;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (tempoAtual > 0)
            {
                tempoAtual--;

                progresso += stringAquecimento;

                progresso += " ";

                if (tempoAtual == 0)
                {
                    progresso += "Aquecimento concluído.";
                    timer.Stop();
                }

                string tempoFormatado = TimeSpan.FromSeconds(tempoAtual).ToString(@"mm\:ss");

                _hubContext.Clients.All.SendAsync("SendMessage", progresso, alimento, tempoFormatado, potenciaAtual, instrucoes);
            }
        }
    }
}

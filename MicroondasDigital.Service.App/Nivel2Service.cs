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
    public class Nivel2Service : INivel2Service
    {
        private readonly INivel2Repository _nivel2Repository;
        private readonly IHubContext<Nivel2Hub> _hubContext;

        public Timer timer;
        public int tempoAtual;
        public int potenciaAtual;
        public string stringAquecimento;
        public string progresso;
        public string alimento;
        public string instrucoes;

        public Nivel2Service(INivel2Repository nivel2Repository, IHubContext<Nivel2Hub> hubContext)
        {
            _nivel2Repository = nivel2Repository;
            _hubContext = hubContext;
        }

        public IEnumerable<Microonda> CarregaListaPreDefinida()
        {
            return _nivel2Repository.ObterProgramasPreDefinidos();
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

                mensagemRetorno = "Aquecimento Reiniciado.";
            }
            else
            {
                SetaValoresPreDefinido(microondas.Id);
            }

            return mensagemRetorno;
        }

        public string PausarAquecimento()
        {
            if (tempoAtual > 0 && timer.Enabled)
            {
                timer.Stop();

                return "Aquecimento Pausado."; ;
            }
            else
            {
                CancelarAquecimento();

                return "Aquecimento Cancelado.";
            }
        }

        public void SetaValoresPreDefinido(int iDPreAquecimento)
        {
            Microonda microondas = _nivel2Repository.ObterPorIdProgramaPreDefinido(iDPreAquecimento);
            if (microondas != null)
            {
                var tempo = (int)(microondas.Tempo.TotalMinutes * 60);
                progresso = string.Empty;
                tempoAtual = tempo;
                potenciaAtual = microondas.Potencia;
                stringAquecimento = microondas.StringAquecimento;
                alimento = microondas.Alimento;
                instrucoes = microondas.Instrucoes;
            }
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

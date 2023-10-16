using MicroondasDigital.Domain.Dtos;
using MicroondasDigital.Domain.Extensions;
using MicroondasDigital.Domain.Extensions.Exceptions;
using MicroondasDigital.Domain.Extensions.Hubs;
using MicroondasDigital.Domain.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MicroondasDigital.Service.App
{
    public class Nivel1Service : INivel1Service
    {
        private readonly IHubContext<Nivel1Hub> _hubContext;

        public Timer timer;
        public int tempoAtual;
        public int tempoTotal;
        public int potenciaAtual;
        public string progresso;

        public Nivel1Service(IHubContext<Nivel1Hub> hubContext)
        {
            _hubContext = hubContext;
        }

        public string IniciarAquecimento(MicroondasNivel1Dto microondas)
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
                AjustarPotenciaETempo(microondas.Tempo, microondas.Potencia);
            }

            return mensagemRetorno;
        }

        public string PausarAquecimento()
        {
            string mensagemRetorno = string.Empty;

            if (tempoAtual > 0 && timer != null && timer.Enabled)
            {
                timer.Stop();

                mensagemRetorno = "Aquecimento Pausado.";
            }
            else
            {
                CancelarAquecimento();

                mensagemRetorno = "Aquecimento Cancelado.";
            }

            return mensagemRetorno;
        }

        private void CancelarAquecimento()
        {
            tempoAtual = 0;
            potenciaAtual = 0;
            tempoTotal = 0;
            progresso = string.Empty;

            _hubContext.Clients.All.SendAsync("SendMessage", "", 0, 0);
        }

        private bool InicializarTimer()
        {
            if (tempoAtual == 0)
            {
                tempoAtual = 0;
                potenciaAtual = 0;
                tempoTotal = 0;
                progresso = string.Empty;

                timer = new Timer(1000);
                timer.Elapsed += Timer_Elapsed;
                timer.Start();

                return true;
            }
            return false;
        }

        private void AjustarPotenciaETempo(int? tempo, int? potencia)
        {
            int tempoDefinido = tempo ?? 30;
            potenciaAtual = potencia ?? 10;

            if (tempoAtual + tempoDefinido > 120)
            {
                throw new TempoInvalidoException();
            }

            tempoAtual += tempoDefinido;
            tempoTotal += tempoDefinido;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (tempoAtual > 0)
            {
                tempoAtual--;

                for (int i = 0; i < potenciaAtual; i++)
                {
                    progresso += ".";
                }

                progresso += " ";

                if (tempoAtual == 0)
                {
                    progresso += "Aquecimento concluído.";
                    timer.Stop();
                }

                string tempoFormatado = tempoTotal >= 60 && tempoTotal <= 90
                ? TimeSpan.FromSeconds(tempoAtual).ToString(@"mm\:ss")
                : tempoAtual.ToString();

                _hubContext.Clients.All.SendAsync("SendMessage", progresso, tempoFormatado, potenciaAtual);
            }
        }
    }
}

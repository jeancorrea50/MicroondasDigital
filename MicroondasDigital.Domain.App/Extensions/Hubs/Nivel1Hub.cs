using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Domain.Extensions.Hubs
{
    public class Nivel1Hub : Hub
    {
        public async Task SendMessage(string progresso, string tempoAtual, int potenciaAtual)
        {

            await Clients.All.SendAsync("SendMessage", progresso, tempoAtual, potenciaAtual);
        }
    }
}

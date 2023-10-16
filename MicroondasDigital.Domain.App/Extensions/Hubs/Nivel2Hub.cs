using MicroondasDigital.Domain.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Domain.Extensions.Hubs
{
    public class Nivel2Hub : Hub
    {
        public async Task SendMessage(string stringAquecimento, string alimento, string tempo, string potencia, string instrucoes)
        {

            await Clients.All.SendAsync("SendMessage", stringAquecimento, alimento, tempo, potencia, instrucoes);
        }
    }
}

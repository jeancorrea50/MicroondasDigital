using MicroondasDigital.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Domain.Interfaces.Repository
{
    public interface INivel4Repository
    {
        bool Registrar(string senha);
        string Login(string senha);
     }
}

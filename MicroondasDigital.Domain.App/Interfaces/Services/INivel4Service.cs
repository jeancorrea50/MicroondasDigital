using MicroondasDigital.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Domain.Interfaces.Services
{
    public interface INivel4Service
    {
        string Registrar(RegistroDto registro);
        string Login(LoginDto registro);
    }
}

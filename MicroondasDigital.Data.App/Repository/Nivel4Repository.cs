using MicroondasDigital.Domain.Dtos;
using MicroondasDigital.Domain.Interfaces.Repository;
using MicroondasDigital.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Data.Repository
{
    public class Nivel4Repository : INivel4Repository
    {
        private readonly IAutenticacaoRepository _AutenticacaoRepository;

        public Nivel4Repository(IAutenticacaoRepository AutenticacaoRepository)
        {
            _AutenticacaoRepository = AutenticacaoRepository;
        }

        public bool Registrar(string senha)
        {
            return _AutenticacaoRepository.SalvarNovoUsuario(senha);
        }

        public string Login(string senha)
        {
            return _AutenticacaoRepository.Login(senha);
        }
    }
}

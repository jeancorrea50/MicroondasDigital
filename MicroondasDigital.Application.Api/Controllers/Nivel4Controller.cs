using MicroondasDigital.Domain.Dtos;
using MicroondasDigital.Domain.Extensions.Exceptions;
using MicroondasDigital.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Reflection;

namespace MicroondasDigital.Application.Api.Controllers
{
    [Route("api/v1/nivel4")]
    public class Nivel4Controller : ControllerBase
    {
        private readonly INivel4Service _nivel4Service;

        public Nivel4Controller(INivel4Service nivel1Service)
        {
            _nivel4Service = nivel1Service;
        }

        [HttpPost("registro")]
        [AllowAnonymous]
        public IActionResult Registro(RegistroDto registro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resultado = _nivel4Service.Registrar(registro);

                    return string.IsNullOrEmpty(resultado) ? BadRequest() : Created("", resultado);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Tempo", ex.Message);
                SalvarExceptions.CriarArquivoTexto($" {ex.Message}, {ex.InnerException}, {ex.StackTrace}");
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDto login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resultado = _nivel4Service.Login(login);

                    return string.IsNullOrEmpty(resultado) ? Unauthorized() : Ok(resultado);
                }
            }
            catch (PadraoException ex)
            {
                throw new PadraoException(ex.Message, ex);
            }

            return BadRequest(ModelState);
        }
    }
}

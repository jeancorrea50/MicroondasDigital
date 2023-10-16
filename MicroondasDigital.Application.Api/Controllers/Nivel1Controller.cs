using MicroondasDigital.Domain.Dtos;
using MicroondasDigital.Domain.Extensions.Exceptions;
using MicroondasDigital.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Timers;

namespace MicroondasDigital.Application.Api.Controllers
{
    [Route("api/v1/nivel1")]
    [Authorize]
    public class Nivel1Controller : ControllerBase
    {
        private readonly INivel1Service _nivel1Service;

        public Nivel1Controller(INivel1Service nivel1Service)
        {
            _nivel1Service = nivel1Service;
        }

        [HttpPost("iniciar-aquecimento")]
        [Authorize]

        public IActionResult Aquecimento(MicroondasNivel1Dto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   var resultado = _nivel1Service.IniciarAquecimento(model);

                    return string.IsNullOrWhiteSpace(resultado) ? BadRequest(resultado) : Ok(resultado);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Tempo", ex.Message);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("pausar-aquecimento")]
        [Authorize]

        public IActionResult PausarAquecimento()
        {
            try
            {
                var resultado = _nivel1Service.PausarAquecimento();

                return string.IsNullOrWhiteSpace(resultado) ? BadRequest(resultado) : Ok(resultado);
            }
            catch (PadraoException ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                                         $"{ex.Message}");
            }
        }
    }
}

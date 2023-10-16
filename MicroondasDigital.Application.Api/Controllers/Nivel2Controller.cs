using MicroondasDigital.Domain.Interfaces.Services;
using MicroondasDigital.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using MicroondasDigital.Service.App;
using Microsoft.AspNetCore.Authorization;
using MicroondasDigital.Domain.Extensions.Exceptions;

namespace MicroondasDigital.Application.Api.Controllers
{
    [Route("api/v1/nivel2")]
    [Authorize]

    public class Nivel2Controller : ControllerBase
    {
        private readonly INivel2Service _nivel2Service;

        public Nivel2Controller(INivel2Service nivel2Service)
        {
            _nivel2Service = nivel2Service;
        }

        [HttpGet("obter-pre-definidos")]
        [Authorize]

        public IActionResult Aquecimento()
        {
            var resultado = _nivel2Service.CarregaListaPreDefinida();

            return resultado.Count() == 0 ? NoContent() : Ok(resultado);
        }

        [HttpPost("iniciar-aquecimento")]
        [Authorize]
        public IActionResult Aquecimento(Microonda model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resultado = _nivel2Service.IniciarAquecimento(model);

                    return Ok(resultado);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Source,ex.Message);
                SalvarExceptions.CriarArquivoTexto($" {ex.Message}, {ex.InnerException}, {ex.StackTrace}");
            }

            return BadRequest(ModelState);
        }

        [HttpPut("pausar-aquecimento")]
        [Authorize]
        public IActionResult PausarAquecimento()
        {
           var resultado = _nivel2Service.PausarAquecimento();

           return Ok(resultado);
        }
    }
}

using MicroondasDigital.Domain.Interfaces.Services;
using MicroondasDigital.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using MicroondasDigital.Domain.Extensions.Exceptions;
using Microsoft.AspNetCore.Http;

namespace MicroondasDigital.Application.Api.Controllers
{
    [Route("api/v1/nivel3")]
    [Authorize]
    public class Nivel3Controller : ControllerBase
    {
        private readonly INivel3Service _nivel3Service;

        public Nivel3Controller(INivel3Service nivel3Service)
        {
            _nivel3Service = nivel3Service;
        }

        [HttpPost("criar-pre-aquecimento-personalizado")]
        [Authorize]
        public ActionResult CriarPreAquecimento([FromBody]Microonda model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resultado = _nivel3Service.SalvarPreAquecimento(model);

                    return Ok(resultado);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (PadraoException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                                            $"{ex.Message}");

                throw new PadraoException(ex.Message, ex);
            }

            catch (FalhaServidorException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                           $"{ex.Message}");

                throw new FalhaServidorException(ex.Message, ex);
            }

        }

        [HttpPost("iniciar-aquecimento")]
        [Authorize]
        public IActionResult Aquecimento([FromBody] Microonda model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resultado = _nivel3Service.IniciarAquecimento(model);

                    return string.IsNullOrEmpty(resultado) ? BadRequest(resultado) : Ok(resultado);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (PadraoException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                                            $"{ex.Message}");

                throw new PadraoException(ex.Message, ex);
            }

            catch (FalhaServidorException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                           $"{ex.Message}");

                throw new FalhaServidorException(ex.Message, ex);
            }
        }

        [HttpPut("pausar-aquecimento")]
        [Authorize]
        public IActionResult PausarAquecimento()
        {
            try
            {
                var resultado = _nivel3Service.PausarAquecimento();

                return string.IsNullOrEmpty(resultado) ? BadRequest(ModelState) : Ok(resultado);
            }
            catch (PadraoException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                                            $"{ex.Message}");

                throw new PadraoException(ex.Message, ex);
            }

            catch (FalhaServidorException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                           $"{ex.Message}");

                throw new FalhaServidorException(ex.Message, ex);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MicroondasDigital.Domain.Models;
using System;
using System.Net;
using MicroondasDigital.Domain.Interfaces.Services;
using MicroondasDigital.Domain.Dtos;
using MicroondasDigital.Domain.Extensions.Exceptions;

namespace MicroondasDigital.Application.App.Controllers
{
    public class Nivel1Controller : ControllerBase
    {
        private readonly INivel1Service _nivel1Service;

        public Nivel1Controller(INivel1Service nivel1Service)
        {
            _nivel1Service = nivel1Service;
        }

        public ActionResult Index()
        {
            MicroondasNivel1Dto model = new MicroondasNivel1Dto();

            return View(model);
        }

        [HttpPost]
        public IActionResult Aquecimento(MicroondasNivel1Dto model)
        {
           try
           {
                if (ModelState.IsValid)
                {
                    _nivel1Service.IniciarAquecimento(model);
                }

                return View("Index");
            }

            catch (ModelStateException ex)
            {
                ModelState.AddModelError(ex.ModelStateKey, ex.Message);

                return View("Index");
            }
        }

        [HttpPost]
        public IActionResult PausarAquecimento()
       {
            _nivel1Service.PausarAquecimento();

            return View("Index");
        }
    }
}

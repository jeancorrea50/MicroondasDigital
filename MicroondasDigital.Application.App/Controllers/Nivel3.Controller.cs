using MicroondasDigital.Domain.Dtos;
using MicroondasDigital.Domain.Extensions.Exceptions;
using MicroondasDigital.Domain.Interfaces.Services;
using MicroondasDigital.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace MicroondasDigital.Application.App.Controllers
{
    public class Nivel3Controller : ControllerBase
    {
        private readonly INivel3Service _nivel3Service;

        public Nivel3Controller(INivel3Service nivel3Service)
        {
            _nivel3Service = nivel3Service;
        }

        public ActionResult Index()
        {
            ViewBag.PreAquecimento = new SelectList(_nivel3Service.CarregaListaProgramas(), "Id", "Nome", "");
            Microonda microondas = new Microonda();

            return View(microondas);
        }

        public ActionResult Create()
        {
            Microonda microondas = new Microonda();

            return View(microondas);
        }

        [HttpPost]
        public ActionResult Create(Microonda model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _nivel3Service.SalvarPreAquecimento(model);

                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Create", model);
                }
            }
            catch (ModelStateException ex)
            {
                ModelState.AddModelError(ex.ModelStateKey, ex.Message);
            }

            return View("Create");
        }

        [HttpPost]
        public IActionResult Aquecimento(Microonda model)
        {
            ViewBag.PreAquecimento = new SelectList(_nivel3Service.CarregaListaProgramas(), "Id", "Nome");
            _nivel3Service.IniciarAquecimento(model);

            return View("Index");
        }

        [HttpPost]
        public IActionResult PausarAquecimento()
        {
            _nivel3Service.PausarAquecimento();
            
            return RedirectToAction("Index");
        }
    }
}

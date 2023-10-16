using MicroondasDigital.Domain.Dtos;
using MicroondasDigital.Domain.Interfaces.Services;
using MicroondasDigital.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace MicroondasDigital.Application.App.Controllers
{
    public class Nivel2Controller : ControllerBase
    {
        private readonly INivel2Service _nivel2Service;

        public Nivel2Controller(INivel2Service nivel2Service)
        {
            _nivel2Service = nivel2Service;
        }

        public ActionResult Index()
        {
            Microonda microondas = new Microonda();

            ViewBag.PreAquecimento = new SelectList(_nivel2Service.CarregaListaPreDefinida(), "Id", "Nome", "");

            return View(microondas);
        }

        [HttpPost]
        public IActionResult Aquecimento(Microonda model)
        {
            ViewBag.PreAquecimento = new SelectList(_nivel2Service.CarregaListaPreDefinida(), "Id", "Nome");

            _nivel2Service.IniciarAquecimento(model);

           return View("Index");
        }

        [HttpPost]
        public IActionResult PausarAquecimento()
        {
            _nivel2Service.PausarAquecimento();
            
             return RedirectToAction("Index"); 
        }
    }
}

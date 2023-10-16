using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace MicroondasDigital.Application.App.Controllers
{
    public abstract class ControllerBase : Controller
    {
        public enum FlashMessageType
        {
            None, Info, Success, Error
        }
        protected void SetFlashMessage(FlashMessageType type, string text)
        {
            TempData["msgType"] = type;
            TempData["msgText"] = text;
        }

       
    }
}

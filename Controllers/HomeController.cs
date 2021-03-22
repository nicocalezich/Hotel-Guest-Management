using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoHotelNT1.Models;

namespace ProyectoHotelNT1.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {                     
            return View();      
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Ingresar(String contra)
        {
            if (contra.Equals(RecuperarPassword()))
            {
                return RedirectToAction("Index", "Habitaciones");
            }
            else
            {
               return RedirectToAction(nameof(IndexInvalido));              
            }
           
        }

        private String RecuperarPassword()
        {
            OceanHillDBContext context = new OceanHillDBContext();

            return context.Credenciales.Find(1).Pass;
        }

        public IActionResult IndexInvalido()
        {          
            return View();
        }


    }

   
}

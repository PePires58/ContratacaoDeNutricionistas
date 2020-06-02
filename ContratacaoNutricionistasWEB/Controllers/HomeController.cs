#region Histórico de manutenção
/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Implementação inicial.
*/
#endregion

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContratacaoNutricionistasWEB.Models;
using Microsoft.AspNetCore.Authorization;

namespace ContratacaoNutricionistasWEB.Controllers
{
    [Authorize(Policy ="Paciente")]
    [Authorize(Policy ="Nutricionista")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

#region Histórico de manutenção
/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Implementação inicial.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Ajustando atributo de autorização.
*/

/*
Data: 16/06/2020
Programador: Pedro Henrique Pires
Descrição: Removendo métodos.
*/
#endregion

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContratacaoNutricionistasWEB.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace ContratacaoNutricionistasWEB.Controllers
{

    public class HomeController : Controller
    {
        [Authorize(Policy = "UsuarioLogado")]
        public IActionResult Index()
        {
             return RedirectToAction("ConsultasAgendadas", "Contrato");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

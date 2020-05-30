#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial do controlador de login
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContratacaoNutricionistasWEB.Models.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace ContratacaoNutricionistasWEB.Controllers
{
    /// <summary>
    /// Controlador de login
    /// </summary>
    public class LoginController : Controller
    {
        /// <summary>
        /// Esse método retorna a página Index.cshtml da pasta Login
        /// </summary>
        /// <param name="pMensagemSucesso">Mensagem de cadastrado com sucesso (Essa mensagem vem do redirecionamento 
        /// feito a partir dos controllers de Paciente ou Nutricionista após realizarem seu cadastro na plataforma)</param>
        /// <returns>A pagina Index.cshtml da pasta Login</returns>
        [HttpGet]
        public IActionResult Index(string pMensagemSucesso)
        {
            /*ViewData é um objeto para passar dados para a .cshtml*/
            ViewData[Constantes.ViewDataMensagemRetorno] = string.IsNullOrEmpty(pMensagemSucesso) ? null : pMensagemSucesso;
            return View(new UsuarioLoginVM());
        }

        /// <summary>
        /// Método que irá realizar o Login de fato, verificando login e senha
        /// </summary>
        /// <param name="pModel">Objeto de Login</param>
        /// <returns>Retorna para a página inicial após realizar o login, se feito com sucesso</returns>
        [HttpPost]
        public IActionResult Index(UsuarioLoginVM pModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(pModel);

                /*Realizar Login*/

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewData[Constantes.ViewDataMensagemErro] = ex.Message;
                return View(pModel);
            }
        }
    }
}
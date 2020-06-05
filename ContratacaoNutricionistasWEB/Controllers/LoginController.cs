#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial do controlador de login
 */

/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Implementação de autenticação para Login.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Inclusão de método de logout.
*/
#endregion

using System;
using System.Threading.Tasks;
using ContratacaoNutricionistas.Domain.Enumerados.Usuario;
using ContratacaoNutricionistas.Domain.Interfaces.Usuario;
using ContratacaoNutricionistasWEB.Models.Usuario;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContratacaoNutricionistasWEB.Controllers
{
    /// <summary>
    /// Controlador de login
    /// </summary>
    public class LoginController : Controller
    {
        #region Propriedades
        /// <summary>
        /// Serviços referente ao usuário
        /// </summary>
        private readonly IServiceUsuario _ServiceUsuario;
        #endregion

        #region Construtor
        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="pServiceUsuario">Serviço de usuário</param>
        public LoginController(IServiceUsuario pServiceUsuario)
        {
            _ServiceUsuario = pServiceUsuario;
        }
        #endregion

        /// <summary>
        /// Esse método retorna a página Index.cshtml da pasta Login
        /// </summary>
        /// <param name="pMensagemSucesso">Mensagem de cadastrado com sucesso (Essa mensagem vem do redirecionamento 
        /// feito a partir dos controllers de Paciente ou Nutricionista após realizarem seu cadastro na plataforma)</param>
        /// <returns>A pagina Index.cshtml da pasta Login</returns>
        [HttpGet]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<IActionResult> Index(UsuarioLoginVM pModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(pModel);

                await HttpContext.SignInAsync(_ServiceUsuario.RetornaAutenticacaoUsuario(pModel.Login, pModel.Senha));

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewData[Constantes.ViewDataMensagemErro] = ex.Message;
                return View(pModel);
            }
        }

        /// <summary>
        /// Método para realizar logout
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            if (User.HasClaim(c => c.Type != TipoUsuarioEnum.NaoDefinido.ToString()))
                await HttpContext.SignOutAsync();

            return RedirectToAction("Index","Login");
        }
    }
}
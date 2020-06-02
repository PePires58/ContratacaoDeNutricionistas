#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial do controlador de nutricionista
 */

/*
* Programador: Pedro Henrique Pires
* Data: 30/05/2020
* Implementação: Implementação Inicial de método para alterar os dados do nutricionista
*/

/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação de métodos de alterar os dados do nutricionista.
 */

/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Ajuste nos métodos de alteração e consulta e inclusão do serviço de usuário.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Ajuste na mensagem de erro.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Implementação de restrição de usuários logados.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Implementando restrição de alteração para somente os dados do usuário logado.
*/
#endregion

using System;
using System.Linq;
using System.Threading.Tasks;
using ContratacaoNutricionistas.Domain.Entidades.Nutricionista;
using ContratacaoNutricionistas.Domain.Entidades.Usuario;
using ContratacaoNutricionistas.Domain.Enumerados.Usuario;
using ContratacaoNutricionistas.Domain.Interfaces.Nutricionista;
using ContratacaoNutricionistas.Domain.Interfaces.Usuario;
using ContratacaoNutricionistasWEB.Models.Nutricionista;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContratacaoNutricionistasWEB.Controllers
{
    /// <summary>
    /// Controlador de nutricionista
    /// </summary>
    [Authorize(Policy ="Nutricionista")]
    public class NutricionistaController : Controller
    {
        #region Propriedades
        /// <summary>
        /// Serviços referente ao Nutricionista
        /// </summary>
        private readonly IServiceNutricionista _ServiceNutricionista;
        /// <summary>
        /// Serviços referente ao usuário
        /// </summary>
        private readonly IServiceUsuario _ServiceUsuario;
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="pServiceNutricionista">Serviços referênte ao nutricionista</param>
        /// <param name="pServiceUsuario">Serviço de usuário</param>
        public NutricionistaController(IServiceNutricionista pServiceNutricionista, IServiceUsuario pServiceUsuario)
        {
            _ServiceNutricionista = pServiceNutricionista;
            _ServiceUsuario = pServiceUsuario;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Essa tela retorna a página de Cadastro.cshtml da pasta Nutricionista
        /// </summary>
        /// <returns>Cadastro.cshtml da pasta Nutricionista</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Cadastro()
        {
            if (User.HasClaim(c => c.Type != TipoUsuarioEnum.NaoDefinido.ToString()))
                await HttpContext.SignOutAsync();

            return View(new NutricionistaCadastroVM());
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Cadastro(NutricionistaCadastroVM pModel)
        {
            try
            {
                ViewData[Constantes.ViewDataMensagemErro] = ViewData[Constantes.ViewDataMensagemRetorno] = null;

                /*Verifica se o modelo é valido, de acordo com os atributos da classe passado no parâmetro*/
                if (!ModelState.IsValid)
                    return View(pModel);


                /*Valida se já existe login cadastrado*/
                if (_ServiceUsuario.LoginExiste(pModel.Login))
                    throw new Exception($"O login: {pModel.Login}, já existe!");

                /*Cadastro o nutricionista*/
                _ServiceNutricionista.CadastrarNutricionista(new NutricionistaCadastro
                (
                    pModel.Nome,
                    pModel.Telefone,
                    Convert.ToInt32(pModel.CRN),
                    pModel.Login,
                    pModel.Senha,
                    new CPF(pModel.CPF, false)
                 ));

                /*Escreve uma mensagem de retorno para a tela de Login*/
                ViewData[Constantes.ViewDataMensagemRetorno] = $"Usuário {pModel.Login} cadastrado com sucesso";
                /*Redireciona para a página Index.cshtml da pasta Login*/
                return RedirectToAction("Index", "Login", new { pMensagemSucesso = ViewData[Constantes.ViewDataMensagemRetorno] });
            }
            catch (Exception ex)
            {
                /*Escreve a mensagem no objeto de ViewData para ser exibida em tela.*/
                ViewData[Constantes.ViewDataMensagemErro] = ex.Message;
                ModelState.ClearValidationState(nameof(pModel.Login));
                pModel.Login = string.Empty;
                return View(pModel);
            }
        }

        [HttpGet]
        public IActionResult AlterarDados(int ID)
        {
            if (ID == 0 || ID < 0)
                return BadRequest();

            /*Se o usuário logado tenta alterar os dados de outro usuário*/
            if (Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == Constantes.IDUsuarioLogado).ValueType)
                != ID)
                return RedirectToAction("Index", "Home");

            NutricionistaAlteracaoVM nutricionistaAlteracaoVM = null;

            /*Buscar do banco*/
            NutricionistaAlteracao nutricionistaAlteracao = _ServiceNutricionista.ConsultarNutricionistaPorID(ID);

            if (nutricionistaAlteracao != null)
            {
                nutricionistaAlteracaoVM = new NutricionistaAlteracaoVM()
                {
                    ID = nutricionistaAlteracao.ID,
                    CRN = nutricionistaAlteracao.CRN.ToString(),
                    Login = nutricionistaAlteracao.Login,
                    Nome = nutricionistaAlteracao.Nome,
                    Senha = nutricionistaAlteracao.Senha,
                    SenhaConfirmacao = nutricionistaAlteracao.Senha,
                    Telefone = nutricionistaAlteracao.Telefone
                };
            }

            if (nutricionistaAlteracaoVM == null)
                return NoContent();

            return View(nutricionistaAlteracaoVM);
        }

        /// <summary>
        /// Altera os dados do nutricionista
        /// </summary>
        /// <param name="pModel">Modelo para alterar</param>
        /// <returns>Retorna para a própria tela com mensagem de sucesso ou erro</returns>
        [HttpPost]
        public IActionResult AlterarDados(NutricionistaAlteracaoVM pModel)
        {
            try
            {
                ViewData[Constantes.ViewDataMensagemErro] = ViewData[Constantes.ViewDataMensagemRetorno] = null;
                /*Verifica se o modelo é valido, de acordo com os atributos da classe passado no parâmetro*/
                if (!ModelState.IsValid)
                    return View(pModel);

                /*Buscar senha e confirmação de senha*/
                NutricionistaAlteracao nutricionistaAlteracao = _ServiceNutricionista.ConsultarNutricionistaPorID(pModel.ID);

                if (nutricionistaAlteracao == null)
                    return NoContent();
                if (!nutricionistaAlteracao.Senha.Equals(pModel.Senha))
                    throw new Exception(Constantes.MensagemErroSenhaNaoLocalizada);

                /*Alterar os dados*/
                _ServiceNutricionista.AlterarDadosNutricionista(new NutricionistaAlteracao(
                    pModel.ID,
                    pModel.Nome,
                    pModel.Telefone,
                    Convert.ToInt32(pModel.CRN),
                    pModel.Login,
                    pModel.Senha,
                    nutricionistaAlteracao.CpfObjeto
                    ));

                ViewData[Constantes.ViewDataMensagemRetorno] = "Dados do nutricionista alterados com sucesso";

                /*Redireciona para a página Index.cshtml da pasta Login*/
                return View(pModel);
            }
            catch (Exception ex)
            {
                ViewData[Constantes.ViewDataMensagemErro] = ex.Message;
                pModel.Senha = pModel.SenhaConfirmacao = string.Empty;
                return View(pModel);
            }
        }
        #endregion

    }
}
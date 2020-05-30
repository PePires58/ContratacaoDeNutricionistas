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
#endregion

using System;
using ContratacaoNutricionistas.Domain.Interfaces.Nutricionista;
using ContratacaoNutricionistasWEB.Models.Nutricionista;
using Microsoft.AspNetCore.Mvc;

namespace ContratacaoNutricionistasWEB.Controllers
{
    /// <summary>
    /// Controlador de nutricionista
    /// </summary>
    public class NutricionistaController : Controller
    {
        #region Propriedades
        /// <summary>
        /// Serviços referente ao Nutricionista
        /// </summary>
        private readonly IServiceNutricionista _ServiceNutricionista;
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="pServiceNutricionista">Serviços referênte ao nutricionista</param>
        public NutricionistaController(IServiceNutricionista pServiceNutricionista)
        {
            _ServiceNutricionista = pServiceNutricionista;
        }
        #endregion


        #region Métodos
        /// <summary>
        /// Essa tela retorna a página de Cadastro.cshtml da pasta Nutricionista
        /// </summary>
        /// <returns>Cadastro.cshtml da pasta Nutricionista</returns>
        [HttpGet]
        public IActionResult Cadastro()
        {


            return View(new NutricionistaCadastroVM());
        }

        [HttpPost]
        public IActionResult Cadastro(NutricionistaCadastroVM pModel)
        {

            try
            {
                ViewData[Constantes.ViewDataMensagemErro] = ViewData[Constantes.ViewDataMensagemRetorno] = null;

                /*Verifica se o modelo é valido, de acordo com os atributos da classe passado no parâmetro*/
                if (!ModelState.IsValid)
                    return View(pModel);


                /*Valida se já existe login cadastrado*/
                //if (_ServicePaciente.LoginExistente(pModel.Login))
                //{
                //    ViewData[ViewDataMensagemErro] = $"O login: {pModel}, já existe!";
                //    ModelState.ClearValidationState(nameof(pModel.Login));
                //    pModel.Login = string.Empty;
                //    return View(pModel);
                //}

                /*Cadastro o nutricionista*/
                //_ServiceNutricionista.CadastrarNutricionista(new ContratacaoNutricionistas.Domain.Entidades.Nutricionista.NutricionistaCadastro
                //(
                //    pModel.Nome,
                //    pModel.CRM,
                //    pModel.Telefone,
                //    pModel.Login,
                //    pModel.Senha,
                //    new CPF(pModel.CPF, false)
                // ));

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
            /*Buscar do banco*/
            NutricionistaAlteracaoVM nutricionistaAlteracaoVM = new NutricionistaAlteracaoVM()
            {
                ID = 1,
                Login = "Nutricionista",
                CRM = "1234",
                Nome = "Nutricionista",
                Senha = "123",
                SenhaConfirmacao = "123",
                Telefone = "(011)4242-2517"
            };

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

                /*Verificar se o usuário preencheu a senha, se preencheu, substitui, se não, mantem a senha do banco*/

                /*Alterar os dados*/

                ViewData[Constantes.ViewDataMensagemRetorno] = "Dados do paciente alterado com sucesso";

                /*Redireciona para a página Index.cshtml da pasta Login*/
                return View(pModel);
            }
            catch (Exception ex)
            {
                ViewData[Constantes.ViewDataMensagemErro] = ex.Message;
                return View(pModel);
            }
        }
        #endregion

    }
}
#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial do controlador de nutricionista
 */

/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação para alteração de nutricionista
 */
#endregion

using ContratacaoNutricionistas.Domain.Interfaces.Paciente;
using ContratacaoNutricionistasWEB.Models.Paciente;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ContratacaoNutricionistasWEB.Controllers
{
    /// <summary>
    /// Controlador de assuntos relacionados à Paciente
    /// </summary>
    public class PacienteController : Controller
    {

        #region Construtor
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="pIServicePaciente">Serviço de paciente</param>
        public PacienteController(IServicePaciente pIServicePaciente)
        {
            _ServicePaciente = pIServicePaciente;
        }
        #endregion

        #region Propriedades
        /// <summary>
        /// Serviços referente ao paciente
        /// </summary>
        private readonly IServicePaciente _ServicePaciente;
        #endregion

        /// <summary>
        /// View para cadastro, retorna a view Cadastro.cshtml da pasta Paciente
        /// </summary>
        /// <returns>Cadastro.cshtml da pasta Paciente</returns>
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        /// <summary>
        /// View para envio do cadastro, realiza o cadastro do Paciente.
        /// </summary>
        /// <param name="pModel">Modelo</param>
        /// <returns>Retorna para a tela de Login quando realizado com sucesso.</returns>
        [HttpPost]
        public IActionResult Cadastro(PacienteCadastroVM pModel)
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

                ///*Cadastro o paciente*/
                //_ServicePaciente.Cadastra(new ContratacaoNutricionistas.Domain.Entidades.Paciente.PacienteCadastro
                //(
                //    pModel.Nome,
                //    pModel.Telefone,
                //    pModel.Login,
                //    pModel.Senha,
                //    new CPF(pModel.CPF, false)
                // ));
                /*Escreve uma mensagem de retorno para a tela de Login*/
                ViewData[Constantes.ViewDataMensagemRetorno] = $"Usuário {pModel.Login} cadastrado com sucesso";
                /*Redireciona para a página Index.cshtml da pasta Login*/
                return RedirectToAction("Index","Login", new { pMensagemSucesso = ViewData[Constantes.ViewDataMensagemRetorno] });
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

        /// <summary>
        /// Método que altera os dados do paciente
        /// </summary>
        /// <param name="ID">ID do paciente</param>
        /// <returns>Retorna a tela AlterarDados.cshtml da pasta Paciente</returns>
        [HttpGet]
        public IActionResult AlterarDados(int ID)
        {
            /*Buscar os dados do banco*/
            PacienteAlteracaoVM pacienteAlteracaoVM = new PacienteAlteracaoVM()
            {
                ID = 1,
                Login = "Pedro",
                Telefone = "(011)4242-2517",
                Nome = "Pedro Pires",
                Senha = "123",
                SenhaConfirmacao = "123"
            };

            return View(pacienteAlteracaoVM);
        }

        /// <summary>
        /// Método que realiza a alteração dos dados do paciente
        /// </summary>
        /// <param name="pModel">Paciente a ser alterado</param>
        /// <returns>Retorna para a mesma tela (AlterarDados.cshtml) com a mensagem de sucesso ou de erro.</returns>
        [HttpPost]
        public IActionResult AlterarDados(PacienteAlteracaoVM pModel)
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
    }
}
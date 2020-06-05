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
 * Data: 03/06/2020
 * Implementação: Implementação de restrição por usuário
 */

/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Restrição por CPF e tipo de usuário.
*/
#endregion

using ContratacaoNutricionistas.Domain.Entidades.Paciente;
using ContratacaoNutricionistas.Domain.Entidades.Usuario;
using ContratacaoNutricionistas.Domain.Enumerados.Usuario;
using ContratacaoNutricionistas.Domain.Interfaces.Paciente;
using ContratacaoNutricionistas.Domain.Interfaces.Usuario;
using ContratacaoNutricionistasWEB.Models.Paciente;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModulosHelper.Extensions;
using System;
using System.Linq;

namespace ContratacaoNutricionistasWEB.Controllers
{
    /// <summary>
    /// Controlador de assuntos relacionados à Paciente
    /// </summary>
    [Authorize(Policy ="Paciente")]
    public class PacienteController : Controller
    {

        #region Construtor
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="pIServicePaciente">Serviço de paciente</param>
        /// <param name="pServiceUsuario">Serviço de usuário</param>
        public PacienteController(IServicePaciente pIServicePaciente, IServiceUsuario pServiceUsuario)
        {
            _ServicePaciente = pIServicePaciente;
            _ServiceUsuario = pServiceUsuario;
        }
        #endregion

        #region Propriedades
        /// <summary>
        /// Serviços referente ao paciente
        /// </summary>
        private readonly IServicePaciente _ServicePaciente;

        /// <summary>
        /// Serviços referente ao usuário
        /// </summary>
        private readonly IServiceUsuario _ServiceUsuario;
        #endregion

        /// <summary>
        /// View para cadastro, retorna a view Cadastro.cshtml da pasta Paciente
        /// </summary>
        /// <returns>Cadastro.cshtml da pasta Paciente</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Cadastro()
        {
            if (User.HasClaim(c => c.Type != TipoUsuarioEnum.NaoDefinido.ToString()))
                HttpContext.SignOutAsync();
            return View();
        }

        /// <summary>
        /// View para envio do cadastro, realiza o cadastro do Paciente.
        /// </summary>
        /// <param name="pModel">Modelo</param>
        /// <returns>Retorna para a tela de Login quando realizado com sucesso.</returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Cadastro(PacienteCadastroVM pModel)
        {
            try
            {
                ViewData[Constantes.ViewDataMensagemErro] = ViewData[Constantes.ViewDataMensagemRetorno] = null;
                /*Verifica se o modelo é valido, de acordo com os atributos da classe passado no parâmetro*/
                if (!ModelState.IsValid)
                    return View(pModel);


                /*Valida se já existe login cadastrado*/
                if (_ServiceUsuario.LoginExiste(pModel.Login, pModel.CPF, pModel.TipoUsuario.GetDefaultValue()))
                    throw new Exception($"O login: {pModel.Login}, já existe!");

                ///*Cadastro o paciente*/
                _ServicePaciente.Cadastra(new PacienteCadastro
                (
                    pModel.Nome,
                    pModel.Telefone,
                    pModel.Login,
                    pModel.Senha,
                    new CPF(pModel.CPF, false)
                 ));

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
            if (ID == 0 || ID < 0)
                return BadRequest();

            /*Se o usuário logado tenta alterar os dados de outro usuário*/
            if (Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == Constantes.IDUsuarioLogado).ValueType)
                != ID)
                return RedirectToAction("Index", "Home");

            PacienteAlteracaoVM pacienteAlteracaoVM = null;

            PacienteAlteracao pacienteAlteracao = _ServicePaciente.ConsultarPacientePorID(ID);

            if (pacienteAlteracao != null)
            {
                pacienteAlteracaoVM = new PacienteAlteracaoVM()
                {
                    ID = pacienteAlteracao.ID,
                    Login = pacienteAlteracao.Login,
                    Nome = pacienteAlteracao.Nome,
                    Senha = pacienteAlteracao.Senha,
                    SenhaConfirmacao = pacienteAlteracao.Senha,
                    Telefone = pacienteAlteracao.Telefone
                };
            }

            if (pacienteAlteracaoVM == null)
                return NoContent();

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

                PacienteAlteracao pacienteAlteracao = _ServicePaciente.ConsultarPacientePorID(pModel.ID);

                if (pacienteAlteracao == null)
                    return NoContent();
                else if (!pacienteAlteracao.Senha.Equals(pModel.Senha))
                    throw new Exception(Constantes.MensagemErroSenhaNaoLocalizada);

                _ServicePaciente.AlterarDados(new PacienteAlteracao(pModel.ID,
                    pModel.Nome,
                    pModel.Telefone,
                    pModel.Login,
                    pModel.Senha,
                    pacienteAlteracao.CpfObjeto));

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
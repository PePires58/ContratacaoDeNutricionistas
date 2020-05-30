#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial do controlador de nutricionista
 */
#endregion

using ContratacaoNutricionistas.Domain.Interfaces.Paciente;
using ContratacaoNutricionistasWEB.Models.Paciente;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModulosHelper.Extensions;
using System;
using System.Collections.Generic;

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

        #region Constantes
        

        /// <summary>
        /// Lista de tipo de pessoa
        /// </summary>
        private List<SelectListItem> ListaTipoPessoa => new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Text = ContratacaoNutricionistas.Domain.Enumerados.Usuario.TipoPessoaEnum.NaoDefinido.GetDescription(),
                Value = ContratacaoNutricionistas.Domain.Enumerados.Usuario.TipoPessoaEnum.NaoDefinido.GetDefaultValue()
            },
            new SelectListItem()
            {
                Text = ContratacaoNutricionistas.Domain.Enumerados.Usuario.TipoPessoaEnum.Fisica.GetDescription(),
                Value = ContratacaoNutricionistas.Domain.Enumerados.Usuario.TipoPessoaEnum.Fisica.GetDefaultValue()
            }
        };
        #endregion

        /// <summary>
        /// View para cadastro, retorna a view Cadastro.cshtml da pasta Paciente
        /// </summary>
        /// <returns>Cadastro.cshtml da pasta Paciente</returns>
        public IActionResult Cadastro()
        {
            /*Utilizada para montar o combo de tipo de pessoa*/
            ViewData[Constantes.ViewDataListaTipoPessoa] = ListaTipoPessoa;

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
            ViewData[Constantes.ViewDataListaTipoPessoa] = ListaTipoPessoa;

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
    }
}
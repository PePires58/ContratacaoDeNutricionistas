#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial do controlador de nutricionista
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContratacaoNutricionistas.Domain.Entidades.Paciente.Usuario;
using ContratacaoNutricionistas.Domain.Interfaces.Nutricionista;
using ContratacaoNutricionistasWEB.Models.Nutricionista;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModulosHelper.Extensions;

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


        #region Métodos
        /// <summary>
        /// Essa tela retorna a página de Cadastro.cshtml da pasta Nutricionista
        /// </summary>
        /// <returns>Cadastro.cshtml da pasta Nutricionista</returns>
        [HttpGet]
        public IActionResult Cadastro()
        {
            /*Lista de tipo de pessoas, passada para montar o combo em tela*/
            ViewData[Constantes.ViewDataListaTipoPessoa] = ListaTipoPessoa;

            return View(new NutricionistaCadastroVM());
        }

        [HttpPost]
        public IActionResult Cadastro(NutricionistaCadastroVM pModel)
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
        #endregion

    }
}
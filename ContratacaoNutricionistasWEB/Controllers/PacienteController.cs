using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ContratacaoNutricionistas.Domain.Interfaces.Paciente;
using ContratacaoNutricionistasWEB.Models.Paciente;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModulosHelper.Extensions;

namespace ContratacaoNutricionistasWEB.Controllers
{
    /// <summary>
    /// Controlador de assuntos relacionados à Paciente
    /// </summary>
    public class PacienteController : Controller
    {

        #region Propriedades
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
        /// Constante para acesso da ViewData
        /// </summary>
        private const string ViewDataMensagemRetorno = "MensagemRetorno";

        /// <summary>
        /// Constante para acesso da ViewData
        /// </summary>
        private const string ViewDataMensagemErro = "MensagemErro";

        /// <summary>
        /// Constante para lista do tipo de pessoa
        /// </summary>
        private const string ViewDataListaTipoPessoa = "ListaTipoPessoa";

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
            },
            new SelectListItem()
            {
                Text = ContratacaoNutricionistas.Domain.Enumerados.Usuario.TipoPessoaEnum.Juridica.GetDescription(),
                Value = ContratacaoNutricionistas.Domain.Enumerados.Usuario.TipoPessoaEnum.Juridica.GetDefaultValue()
            }
        };
        #endregion

        /// <summary>
        /// View para cadastro
        /// </summary>
        /// <returns></returns>
        public IActionResult Cadastro()
        {
            ViewData[ViewDataListaTipoPessoa] = ListaTipoPessoa;

            return View();
        }

        /// <summary>
        /// View para envio do cadastro
        /// </summary>
        /// <param name="pModel">Modelo</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Cadastro(PacienteCadastroVM pModel)
        {
            ViewData[ViewDataListaTipoPessoa] = ListaTipoPessoa;

            try
            {
                ViewData[ViewDataMensagemRetorno] = string.Empty;
                ViewData[ViewDataMensagemErro] = string.Empty;

#warning validar CPF e CNPJ a partir do tipo de pessoa


                if (!ModelState.IsValid)
                {
                    return View(pModel);
                }


                /*Valida se já existe login cadastrado*/
                if (_ServicePaciente.LoginExistente(pModel.Login))
                {
                    ViewData[ViewDataMensagemErro] = $"O login: {pModel}, já existe!";
                    ModelState.ClearValidationState(nameof(pModel.Login));
                    pModel.Login = string.Empty;

                    return View(pModel);
                }

                /*Cadastro o paciente*/
                _ServicePaciente.Cadastra(new ContratacaoNutricionistas.Domain.Entidades.Paciente.PacienteCadastro()
                {
                    Login = pModel.Login,
                    Nome = pModel.Nome,
                    Senha = pModel.Senha,
                    Telefone = pModel.Telefone,
                    TipoPessoa = pModel.TipoPessoa,
                    TipoUsuario = pModel.TipoUsuario,
                    CpfCnpj = pModel.TipoPessoa == ContratacaoNutricionistas.Domain.Enumerados.Usuario.TipoPessoaEnum.Fisica ? pModel.CPF : pModel.CNPJ
                });

                ViewData[ViewDataMensagemRetorno] = $"Usuário {pModel.Login} cadastrado com sucesso";

                return RedirectToActionPermanent("Index", "Home");
            }
            catch
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError));
            }
        }
    }
}
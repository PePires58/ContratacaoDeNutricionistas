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

/*
 * Programador: Pedro Henrique Pires
 * Data: 03/06/2020
 * Implementação: Implementação de cadastro/alteração de endereço e consulta de CEP
 */

/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Ajustando erro ao acessar a tela.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Inclusão de serviço de endereço
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContratacaoNutricionistas.Domain.Entidades.Nutricionista;
using ContratacaoNutricionistas.Domain.Entidades.Usuario;
using ContratacaoNutricionistas.Domain.Enumerados.Gerais;
using ContratacaoNutricionistas.Domain.Enumerados.Usuario;
using ContratacaoNutricionistas.Domain.Interfaces.Endereco;
using ContratacaoNutricionistas.Domain.Interfaces.Nutricionista;
using ContratacaoNutricionistas.Domain.Interfaces.Usuario;
using ContratacaoNutricionistasWEB.Models.Nutricionista;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModulosHelper.Extensions;

namespace ContratacaoNutricionistasWEB.Controllers
{
    /// <summary>
    /// Controlador de nutricionista
    /// </summary>
    [Authorize(Policy = "Nutricionista")]
    public class NutricionistaController : Controller
    {
        #region Propriedades
        /// <summary>
        /// Serviços referente ao Nutricionista
        /// </summary>
        private readonly IServiceNutricionista _ServiceNutricionista;

        /// <summary>
        /// Serviços referente ao Nutricionista
        /// </summary>
        private readonly IServiceEndereco _ServiceEndereco;
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
        /// <param name="pServiceEndereco">Serviço para endereço</param>
        public NutricionistaController(IServiceNutricionista pServiceNutricionista, IServiceUsuario pServiceUsuario, IServiceEndereco pServiceEndereco)
        {
            _ServiceNutricionista = pServiceNutricionista;
            _ServiceUsuario = pServiceUsuario;
            _ServiceEndereco = pServiceEndereco;
        }
        #endregion

        #region Constantes
        List<SelectListItem> ListaUF => Enum.GetValues(typeof(UnidadeFederacaoEnum)).Cast<UnidadeFederacaoEnum>().Select(v => new SelectListItem
        {
            Text = v.GetDescription(),
            Value = v.GetDefaultValue()
        }).ToList();
        #endregion

        #region Métodos

        #region Cadastro
        /// <summary>
        /// Essa tela retorna a página de Cadastro.cshtml da pasta Nutricionista
        /// </summary>
        /// <returns>Cadastro.cshtml da pasta Nutricionista</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Cadastro()
        {
            if (User.HasClaim(c => c.Type != TipoUsuarioEnum.NaoDefinido.ToString()))
                return RedirectToAction("Logout", "Login");
            return View();
        }

        /// <summary>
        /// Cadastra um nutricionista
        /// </summary>
        /// <param name="pModel">Nutricionista a ser cadastrado</param>
        /// <returns>Tela de login caso sucesso ou tela de cadastro quando erro</returns>
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
                if (_ServiceUsuario.LoginExiste(pModel.Login,pModel.CPF,pModel.TipoUsuario.GetDefaultValue()))
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
        #endregion

        #region Alteração de dados
        /// <summary>
        /// Tela inicial para alteração de dados
        /// </summary>
        /// <param name="ID">ID do nutricionista</param>
        /// <returns>AlterarDados.cshtml da pasta Nutricionista</returns>
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

        #region Cadastro de endereço
        /// <summary>
        /// Retorna a tela para cadastrar os endereços de trabalho
        /// </summary>
        /// <returns>CadastrarEndereco.cshtml da pasta Nutricionista</returns>
        [HttpGet]
        public IActionResult CadastrarEndereco()
        {
            return View();
        }

        /// <summary>
        /// Cadastra o endereço do nutricionista
        /// </summary>
        /// <param name="pModel">Modelo a ser validado</param>
        /// <returns>Mensagem de sucesso ou erro</returns>
        [HttpPost]
        public IActionResult CadastrarEndereco(EnderecoVM pModel)
        {
            ViewData[Constantes.ViewDataMensagemErro] = ViewData[Constantes.ViewDataMensagemRetorno] = null;

            try
            {
                if (!ModelState.IsValid)
                    return View(pModel);

                UnidadeFederacaoEnum unidadeFeracao = Enum.GetValues(typeof(UnidadeFederacaoEnum))
                    .Cast<UnidadeFederacaoEnum>()
                    .FirstOrDefault(v => v.GetDescription().Equals(pModel.UF));

                int IdUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == Constantes.IDUsuarioLogado).ValueType);

                _ServiceEndereco.CadastrarEndereco(new Endereco(
                    IdUsuario,
                    pModel.Logradouro,
                    pModel.Bairro,
                    pModel.Cidade,
                    pModel.CEP,
                    unidadeFeracao
                    )
                {
                    Numero = pModel?.Numero,
                    Complemento = pModel.Complemento == null ? string.Empty : pModel.Complemento
                });

                ViewData[Constantes.ViewDataMensagemRetorno] = $"Endereço {pModel.Logradouro}, {pModel.Numero}. {pModel.Cidade} - {unidadeFeracao.GetDefaultValue()}.{Environment.NewLine}Cadastrado com sucesso!";

                ModelState.Clear();

                return View();
            }
            catch (Exception ex)
            {
                ViewData[Constantes.ViewDataMensagemErro] = ex.Message;
                return View();
            }
        }

        #region JsonResults
        [HttpGet]
        public JsonResult ConsultarEnderecoCEP(string pCEP)
        {
            ViewData[Constantes.ViewDataMensagemErro] = ViewData[Constantes.ViewDataMensagemRetorno] = null;

            try
            {
                Endereco endereco = _ServiceEndereco.ConsultarEnderecoPorCEP(pCEP,
                    Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == Constantes.IDUsuarioLogado).ValueType));

                return Json(new
                {
                    status = "OK",
                    data = new EnderecoVM()
                    {
                        Bairro = endereco.Bairro,
                        CEP = endereco.CEP,
                        Cidade = endereco.Cidade,
                        Complemento = endereco.Complemento,
                        Logradouro = endereco.Logradouro,
                        UF = endereco.UF.GetDescription(),
                        Numero = endereco.Numero
                    }
                });
            }
            catch (Exception ex)
            {
                ViewData[Constantes.ViewDataMensagemErro] = ex.Message;
                return Json(new
                {
                    status = "ERROR"
                });
            }
        }
        #endregion

        #endregion

        #region Endereços cadastrados

        /// <summary>
        /// Lista de endereços cadastrados
        /// </summary>
        /// <param name="pIndiceInicial">Indice inicial</param>
        /// <param name="pRua">Rua</param>
        /// <param name="pCidade">Cidade</param>
        /// <param name="pBairro">Bairro</param>
        /// <param name="pCEP">CEP</param>
        /// <param name="pUF">UF</param>
        /// <returns>Uma lista de endereços</returns>
        [HttpGet]
        public IActionResult EnderecosCadastrados(int pIndiceInicial, string pRua, string pCidade, string pBairro, string pCEP, string pUF)
        {
            List<Endereco> enderecos = _ServiceEndereco.EnderecosCadastrados(
                Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == Constantes.IDUsuarioLogado).ValueType),
                pRua,
                pCidade,
                pBairro,
                pCEP,
                pUF);

            List<EnderecoAlteracaoVM> enderecosVm = new List<EnderecoAlteracaoVM>();

            if (enderecos.Any())
            {

                enderecosVm = enderecos.Select(c => new EnderecoAlteracaoVM()
                {
                    ID = c.IdEndereco,
                    Bairro = c.Bairro,
                    CEP = c.CEP,
                    Cidade = c.Cidade,
                    Complemento = c.Complemento,
                    Logradouro = c.Logradouro,
                    Numero = c.Numero,
                    UF = c.UF.GetDescription()
                }).ToList();
            }

            return View(enderecosVm.Skip(pIndiceInicial).Take(Constantes.QuantidadeRegistrosPorPagina));
        }

        #endregion

        #region Editar endereço
        /// <summary>
        /// Método que altera os dados do endereço
        /// </summary>
        /// <param name="ID">ID do endereço</param>
        /// <returns>Endereço para ser alterado</returns>
        [HttpGet]
        public IActionResult EditarEndereco(int ID)
        {
            ViewData[Constantes.ViewDataMensagemErro] = ViewData[Constantes.ViewDataMensagemRetorno] = null;
            if (ID <= 0)
                return RedirectToAction("EnderecosCadastrados", "Nutricionista");

            EnderecoAlteracaoVM enderecoAlteracaoVM = null;

            Endereco endereco = _ServiceEndereco.ConsultarEnderecoNutricionistaPorID(ID,
                Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == Constantes.IDUsuarioLogado).ValueType)
                );

            if (endereco != null)
            {
                enderecoAlteracaoVM = new EnderecoAlteracaoVM()
                {
                    ID = endereco.IdEndereco,
                    Bairro = endereco.Bairro,
                    CEP = endereco.CEP,
                    Cidade = endereco.Cidade,
                    Complemento = endereco.Complemento,
                    Logradouro = endereco.Logradouro,
                    Numero = endereco.Numero,
                    UF = endereco.UF.GetDefaultValue()
                };
            }

            if (enderecoAlteracaoVM == null)
                return RedirectToAction("EnderecosCadastrados", "Nutricionista");

            ViewData[Constantes.ViewDataUnidadesFeracao] = ListaUF;

            return View(enderecoAlteracaoVM);
        }

        /// <summary>
        /// Edita os dados de um endereço
        /// </summary>
        /// <param name="pModel">Endereço a ser alterado</param>
        /// <returns>Mensagem de sucesso ou erro</returns>
        [HttpPost]
        public IActionResult EditarEndereco(EnderecoAlteracaoVM pModel)
        {
            ViewData[Constantes.ViewDataUnidadesFeracao] = ListaUF;

            try
            {
                if (!ModelState.IsValid)
                    return View(pModel);

                UnidadeFederacaoEnum unidadeFeracao = Enum.GetValues(typeof(UnidadeFederacaoEnum))
                    .Cast<UnidadeFederacaoEnum>()
                    .FirstOrDefault(v => v.GetDescription().Equals(pModel.UF));

                int IdUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == Constantes.IDUsuarioLogado).ValueType);

                _ServiceEndereco.AlterarDadosEndereco(new Endereco(
                    IdUsuario,
                    pModel.Logradouro,
                    pModel.Bairro,
                    pModel.Cidade,
                    pModel.CEP,
                    unidadeFeracao
                    )
                {
                    Numero = pModel.Numero,
                    Complemento = pModel.Complemento,
                    IdEndereco = pModel.ID
                });

                ViewData[Constantes.ViewDataMensagemRetorno] = $"Endereço {pModel.Logradouro}, {pModel.Numero}. {pModel.Cidade} - {unidadeFeracao.GetDefaultValue()}.{Environment.NewLine}Alterado com sucesso!";

                return View(pModel);
            }
            catch (Exception ex)
            {
                ViewData[Constantes.ViewDataMensagemErro] = ex.Message;
                return View(pModel);
            }
        }
        #endregion

        #endregion
    }
}
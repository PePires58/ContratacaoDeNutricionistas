#region Histórico de manutenção
/*
Data: 08/06/2020
Programador: Pedro Henrique Pires
Descrição: Implementação inicial
*/

/*
Data: 08/06/2020
Programador: Pedro Henrique Pires
Descrição: Ajuste para mostrar os endereços de todos os nutricionistas
*/

/*
Data: 10/06/2020
Programador: Pedro Henrique Pires
Descrição: Realizando a contratação.
*/

/*
Data: 13/06/2020
Programador: Pedro Henrique Pires
Descrição: Listando contratos disponíveis.
*/

/*
Data: 15/06/2020
Programador: Pedro Henrique Pires
Descrição: Incluindo método de buscar contrato e alterar status.
*/

/*
Data: 19/06/2020
Programador: Pedro Henrique Pires
Descrição: Removendo autorize da classe e colocando nos métodos.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using ContratacaoNutricionistas.Domain.Entidades.Agenda;
using ContratacaoNutricionistas.Domain.Entidades.Contrato;
using ContratacaoNutricionistas.Domain.Entidades.Nutricionista;
using ContratacaoNutricionistas.Domain.Enumerados.Agenda;
using ContratacaoNutricionistas.Domain.Enumerados.Contrato;
using ContratacaoNutricionistas.Domain.Enumerados.Gerais;
using ContratacaoNutricionistas.Domain.Interfaces.Agenda;
using ContratacaoNutricionistas.Domain.Interfaces.Contrato;
using ContratacaoNutricionistas.Domain.Interfaces.Endereco;
using ContratacaoNutricionistas.Domain.Interfaces.Nutricionista;
using ContratacaoNutricionistas.Domain.Interfaces.Paciente;
using ContratacaoNutricionistasWEB.Models.Agenda;
using ContratacaoNutricionistasWEB.Models.Contrato;
using ContratacaoNutricionistasWEB.Models.Nutricionista;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModulosHelper.Extensions;

namespace ContratacaoNutricionistasWEB.Controllers
{
    public class ContratoController : Controller
    {
        #region Propriedades
        /// <summary>
        /// Serviços referente ao Nutricionista
        /// </summary>
        private readonly IServiceEndereco _ServiceEndereco;

        /// <summary>
        /// Serviços de agenda
        /// </summary>
        private readonly IServiceAgenda _ServiceAgenda;

        /// <summary>
        /// Serviços de contrato
        /// </summary>
        private readonly IServiceContrato _ServiceContrato;

        /// <summary>
        /// Serviços de paciente
        /// </summary>
        private readonly IServicePaciente _ServicePaciente;

        /// <summary>
        /// Serviços de nutricionista
        /// </summary>
        private readonly IServiceNutricionista _ServiceNutricionista;
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="ServiceEndereco">Serviços de endereço</param>
        /// <param name="ServiceAgenda">Serviços de agenda</param>
        /// <param name="ServiceContrato">Serviços de contrato</param>
        public ContratoController(IServiceEndereco ServiceEndereco, IServiceAgenda ServiceAgenda, IServiceContrato ServiceContrato, IServicePaciente ServicePaciente, IServiceNutricionista ServiceNutricionista)
        {
            _ServiceEndereco = ServiceEndereco;
            _ServiceAgenda = ServiceAgenda;
            _ServiceContrato = ServiceContrato;
            _ServiceNutricionista = ServiceNutricionista;
            _ServicePaciente = ServicePaciente;
        }
        #endregion

        #region Métodos

        #region Agendar consulta

        [HttpGet]
        [Authorize(Policy = "Paciente")]
        public IActionResult FiltroLocalizarNutricionista()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "Paciente")]
        public IActionResult FiltroLocalizarNutricionista(FiltroLocalizarNutricionistaVM pFiltro)
        {
            return RedirectToAction("LocalizarNutricionista", "Contrato",
                new
                {
                    pIndiceInicial = 0,
                    pRua = pFiltro.Logradouro,
                    pCidade = pFiltro.Cidade,
                    pBairro = pFiltro.Bairro,
                    pCEP = pFiltro.CEP,
                    pUF = pFiltro.UF,
                    pDataInicio = pFiltro.DataAgendaInicio,
                    pDataFim = pFiltro.DataAgendaFim,
                    pNomeNutricionista = pFiltro.Nome
                });
        }


        /// <summary>
        /// Método que localiza o endereço
        /// </summary>
        /// <param name="pIndiceInicial">Indice inicial</param>
        /// <param name="pRua">Rua</param>
        /// <param name="pCidade">Cidade</param>
        /// <param name="pBairro">Biarro</param>
        /// <param name="pCEP">CEP</param>
        /// <param name="pUF">UF</param>
        /// <param name="pDataInicio">Data início</param>
        /// <param name="pDataFim">Data de término</param>
        /// <param name="mensagem">Mensagem</param>
        /// <returns>Tela de localizar o nutricionsita</returns>
        [HttpGet]
        [Authorize(Policy = "Paciente")]
        public IActionResult LocalizarNutricionista(int pIndiceInicial, string pRua, string pCidade, string pBairro, string pCEP, string pUF, DateTime pDataInicio, DateTime pDataFim, string pNomeNutricionista, string mensagem)
        {
            ViewData[Constantes.ViewDataMensagemRetorno] = mensagem;
            ViewData[Constantes.ViewDataActionFiltro] = "FiltroLocalizarNutricionista";
            ViewData[Constantes.ViewDataControllerFiltro] = "Contrato";

            List<Agenda> agendas = _ServiceAgenda.AgendasCadastradas(pDataInicio, pDataFim, 0, true);
            List<int> nutricionistas = new List<int>();

            if (agendas.Any(d => d.DataInicio > Constantes.DateTimeNow()))
            {
                agendas = agendas.Where(d => d.DataInicio > Constantes.DateTimeNow() && _ServiceContrato.AgendaDisponivelParaContratar(d.IdAgenda)).ToList();
                nutricionistas = agendas
                    .Where(d => d.DataInicio > Constantes.DateTimeNow())
                    .Select(c => c.IdUsuario)
                    .Distinct()
                    .ToList();
            }
            else
                agendas = new List<Agenda>();

            List<Endereco> enderecos = new List<Endereco>();

            foreach (int nutricionista in nutricionistas)
            {
                enderecos.AddRange(_ServiceEndereco.EnderecosCadastrados(
                nutricionista,
                pRua,
                pCidade,
                pBairro,
                pCEP,
                pUF));
            }

            List<EnderecoAlteracaoVM> enderecosVm = new List<EnderecoAlteracaoVM>();

            if (enderecos.Any())
            {

                enderecosVm = enderecos.Distinct().Select(c => new EnderecoAlteracaoVM()
                {
                    ID = c.IdEndereco,
                    Bairro = c.Bairro,
                    CEP = c.CEP,
                    Cidade = c.Cidade,
                    Complemento = c.Complemento,
                    Logradouro = c.Logradouro,
                    Numero = c.Numero,
                    UF = c.UF.GetDefaultValue()
                }).ToList();
            }

            List<AgendaListaContratoVM> listaAgendas = new List<AgendaListaContratoVM>();

            if (agendas.Any(c => _ServiceContrato.AgendaDisponivelParaContratar(c.IdAgenda)) && enderecosVm.Any())
            {
                listaAgendas = agendas.Join(enderecosVm,
                             a => a.IdEndereco,
                             e => e.ID,
                             (agenda, endereco) => new AgendaListaContratoVM()
                             {
                                 IdAgenda = agenda.IdAgenda,
                                 IdUsuario = agenda.IdUsuario,
                                 DataFim = agenda.DataFim,
                                 DataInicio = agenda.DataInicio,
                                 StatusDaAgenda = StatusAgendaEnum.Ativa,
                                 Endereco = endereco
                             }).ToList();
            }

            return View(listaAgendas.Skip(pIndiceInicial).Take(Constantes.QuantidadeRegistrosPorPagina));
        }

        /// <summary>
        /// Contrata um nutricionista
        /// </summary>
        /// <param name="pIdAgenda">Agenda</param>
        /// <param name="pIdNutricionista">Nutricionsita</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "Paciente")]
        public IActionResult ContratarNutricionista(int pIdAgenda, int pIdNutricionista)
        {
            List<Agenda> agendas = _ServiceAgenda.AgendasCadastradas(DateTime.MinValue, DateTime.MinValue, pIdNutricionista, true);
            List<Endereco> enderecos = _ServiceEndereco.EnderecosCadastrados(
                pIdNutricionista,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty);

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
                    UF = c.UF.GetDefaultValue()
                }).ToList();
            }

            AgendaListaContratoVM agendaContrato = null;

            if (agendas.Any(id => id.IdAgenda == pIdAgenda &&
                             id.IdUsuario == pIdNutricionista) && enderecosVm.Any()
                             && _ServiceContrato.AgendaDisponivelParaContratar(pIdAgenda))
            {
                agendaContrato = agendas.Join(enderecosVm,
                             a => a.IdEndereco,
                             e => e.ID,
                             (agenda, endereco) => new AgendaListaContratoVM()
                             {
                                 IdAgenda = agenda.IdAgenda,
                                 IdUsuario = agenda.IdUsuario,
                                 DataFim = agenda.DataFim,
                                 DataInicio = agenda.DataInicio,
                                 StatusDaAgenda = StatusAgendaEnum.Ativa,
                                 Endereco = endereco
                             }).FirstOrDefault(id => id.IdAgenda == pIdAgenda &&
                             id.IdUsuario == pIdNutricionista && _ServiceContrato.AgendaDisponivelParaContratar(id.IdAgenda));
            }

            if (agendaContrato == null)
                return RedirectToAction("LocalizarNutricionista", "Contrato");

            return View(agendaContrato);
        }

        /// <summary>
        /// Contrata um nutricionista
        /// </summary>
        /// <param name="pModel">Contrato</param>
        /// <returns>Sucesso ou erro</returns>
        [HttpPost]
        [Authorize(Policy = "Paciente")]
        public IActionResult ContratarNutricionista(AgendaListaContratoVM pModel)
        {
            try
            {
                int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == Constantes.IDUsuarioLogado).ValueType);

                if (!ModelState.IsValid)
                    return View(pModel);

                _ServiceContrato.ContratarNutricionista(
                    new Contrato(
                        pIdUsuario: idUsuario,
                        pIdNutricionista: pModel.IdUsuario,
                        pLogradouro: pModel.Endereco.Logradouro,
                        complemento: pModel.Endereco?.Complemento,
                        numero: pModel.Endereco?.Numero,
                        pBairro: pModel.Endereco.Bairro,
                        pCidade: pModel.Endereco.Cidade,
                        pUF: Enum.GetValues(typeof(UnidadeFederacaoEnum)).Cast<UnidadeFederacaoEnum>().FirstOrDefault(c => c.GetDefaultValue().Equals(pModel.Endereco.UF)),
                        pCEP: pModel.Endereco.CEP,
                        dataInicio: pModel.DataInicio,
                        dataTermino: pModel.DataFim,
                        status: ContratacaoNutricionistas.Domain.Enumerados.Contrato.StatusContratoEnum.PendenteAceitacaoNutricionista
                        )
                    );

                ViewData[Constantes.ViewDataMensagemRetorno] = $"Sua consulta foi agendada com sucesso!";

                return RedirectToAction("ConsultasAgendadas", "Contrato", new { mensagem = ViewData[Constantes.ViewDataMensagemRetorno] });
            }
            catch (Exception ex)
            {
                ViewData[Constantes.ViewDataMensagemErro] = ex.Message;
                return View(pModel);
            }
        }

        #endregion

        #region Consultas agendadas
        [HttpGet]
        [Authorize(Policy = "UsuarioLogado")]
        public IActionResult ConsultasAgendadas(int pIndiceInicial, string pRua, string pCidade, string pBairro, string pCEP, string pUF, DateTime pDataInicio, DateTime pDataFim, string mensagem)
        {
            ViewData[Constantes.ViewDataMensagemRetorno] = mensagem;
            ViewData[Constantes.ViewDataActionFiltro] = "FiltroConsultasAgendadas";
            ViewData[Constantes.ViewDataControllerFiltro] = "Contrato";

            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == Constantes.IDUsuarioLogado).ValueType);

            List<Contrato> contratos = _ServiceContrato.ListaContratos(
                pRua,
                pCidade,
                pBairro,
                pCEP,
                pUF,
                pDataInicio,
                pDataFim,
                idUsuario
                );

            List<ContratoVM> contratosVM = new List<ContratoVM>();

            if (contratos.Any())
            {
                contratosVM = contratos.
                    Select((c, g) => new ContratoVM()
                    {
                        Index = g,
                        IdContrato = c.IdContrato,
                        DataFim = c.DataTermino,
                        DataInicio = c.DataInicio,
                        Endereco = new EnderecoVM()
                        {
                            Bairro = c.Bairro,
                            CEP = c.CEP,
                            Cidade = c.Cidade,
                            Complemento = c?.Complemento,
                            Logradouro = c.Logradouro,
                            Numero = c?.Numero,
                            UF = c.UF.GetDefaultValue()
                        },
                        NomeNutricionista = _ServiceNutricionista.ConsultarNutricionistaPorID(c.IdNutricionista).Nome,
                        NomePaciente = _ServicePaciente.ConsultarPacientePorID(c.IdUsuario).Nome,
                        IdUsuario = idUsuario,
                        Status = c.Status
                    }).ToList();
            }

            return View(contratosVM.Skip(pIndiceInicial).Take(Constantes.QuantidadeRegistrosPorPagina));
        }
        #endregion

        #region Cancelar consulta
        /// <summary>
        /// Cancela a consulta
        /// </summary>
        /// <param name="ID">ID do contrato</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "UsuarioLogado")]
        public IActionResult CancelarConsulta(int ID)
        {
            try
            {
                Contrato contrato = _ServiceContrato.BuscaContratoPorID(ID);

                if (contrato == null)
                    return RedirectToAction("ConsultasAgendadas", "Contrato");

                ContratoVM contratoVM = new ContratoVM()
                {
                    DataFim = contrato.DataTermino,
                    DataInicio = contrato.DataInicio,
                    Endereco = new EnderecoVM()
                    {
                        Bairro = contrato.Bairro,
                        CEP = contrato.CEP,
                        Cidade = contrato.Cidade,
                        Complemento = contrato?.Complemento,
                        Logradouro = contrato.Logradouro,
                        Numero = contrato?.Numero,
                        UF = contrato.UF.GetDefaultValue()
                    },
                    IdContrato = contrato.IdContrato,
                    IdUsuario = contrato.IdUsuario,
                    NomeNutricionista = _ServiceNutricionista.ConsultarNutricionistaPorID(contrato.IdNutricionista).Nome,
                    NomePaciente = _ServicePaciente.ConsultarPacientePorID(contrato.IdUsuario).Nome
                };

                return View(contratoVM);
            }
            catch
            {
                return RedirectToAction("ConsultasAgendadas", "Contrato");
            }
        }

        /// <summary>
        /// Cancelaa consulta
        /// </summary>
        /// <param name="pModel">Contrato a ser cancelado</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "UsuarioLogado")]
        public IActionResult CancelarConsulta(ContratoVM pModel)
        {
            try
            {
                if (pModel.IdContrato == 0)
                    return View(pModel);

                StatusContratoEnum statusContratoEnum = StatusContratoEnum.Agendada;

                if (User.Claims.Any(c => c.Type == Constantes.NutricionistaLogado))
                    statusContratoEnum = StatusContratoEnum.CanceladaNutricionista;
                else
                    statusContratoEnum = StatusContratoEnum.CanceladaPaciente;

                _ServiceContrato.AlterarStatusContrato(pModel.IdContrato,
                    statusContratoEnum);

                return RedirectToAction("ConsultasAgendadas", "Contrato", new { mensagem = "Consulta cancelada com sucesso." });
            }
            catch (Exception ex)
            {
                ViewData[Constantes.ViewDataMensagemErro] = ex.Message;
                return View(pModel);
            }
        }

        #endregion

        #region Aceitar contrato
        /// <summary>
        /// Aceita o contrato
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "Nutricionista")]
        public IActionResult AceitarContrato(int ID)
        {
            Contrato contrato = _ServiceContrato.BuscaContratoPorID(ID);

            if (contrato == null)
                return RedirectToAction("ConsultasAgendadas", "Contrato");

            ContratoVM contratoVM = new ContratoVM()
            {
                DataFim = contrato.DataTermino,
                DataInicio = contrato.DataInicio,
                Endereco = new EnderecoVM()
                {
                    Bairro = contrato.Bairro,
                    CEP = contrato.CEP,
                    Cidade = contrato.Cidade,
                    Complemento = contrato?.Complemento,
                    Logradouro = contrato.Logradouro,
                    Numero = contrato?.Numero,
                    UF = contrato.UF.GetDefaultValue()
                },
                IdContrato = contrato.IdContrato,
                IdUsuario = contrato.IdUsuario,
                NomeNutricionista = _ServiceNutricionista.ConsultarNutricionistaPorID(contrato.IdNutricionista).Nome,
                NomePaciente = _ServicePaciente.ConsultarPacientePorID(contrato.IdUsuario).Nome
            };

            return View(contratoVM);
        }

        [HttpPost]
        [Authorize(Policy = "Nutricionista")]
        public IActionResult AceitarContrato(ContratoVM pModel)
        {
            try
            {
                if (pModel.IdContrato == 0)
                    return ViewBag(pModel);

                _ServiceContrato.AlterarStatusContrato(pModel.IdContrato,
                    StatusContratoEnum.Agendada);

                return RedirectToAction("ConsultasAgendadas", "Contrato", new { mensagem = "Consulta agendada com sucesso." });
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
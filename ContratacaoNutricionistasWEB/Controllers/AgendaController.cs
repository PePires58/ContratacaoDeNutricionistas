#region Histórico de manutenções
/*
 Data: 06/05/2020
 Programador: Pedro Henrique Pires
 Descrição: Implementação Inicial da classe.
 */

/*
Data: 08/06/2020
Programador: Pedro Henrique Pires
Descrição: Ajuste para escrita do método
*/

/*
Data: 27/06/2020
Programador: Pedro Henrique Pires
Descrição: Inativar agenda.
*/

#endregion
using ContratacaoNutricionistas.Domain.Entidades.Agenda;
using ContratacaoNutricionistas.Domain.Entidades.Nutricionista;
using ContratacaoNutricionistas.Domain.Enumerados.Agenda;
using ContratacaoNutricionistas.Domain.Interfaces.Agenda;
using ContratacaoNutricionistas.Domain.Interfaces.Endereco;
using ContratacaoNutricionistasWEB.Models.Agenda;
using ContratacaoNutricionistasWEB.Models.Nutricionista;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModulosHelper.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContratacaoNutricionistasWEB.Controllers
{
    [Authorize(Policy = "Nutricionista")]
    public class AgendaController : Controller
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
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="ServiceEndereco">Serviço de endereço</param>
        /// <param name="ServiceAgenda">Serviço de agenda</param>
        public AgendaController(IServiceEndereco ServiceEndereco, IServiceAgenda ServiceAgenda)
        {
            _ServiceEndereco = ServiceEndereco;
            _ServiceAgenda = ServiceAgenda;
        }
        #endregion

        #region Métodos
        #region Cadastrar agenda
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
        public IActionResult AgendaEndereco(int pIndiceInicial, string pRua, string pCidade, string pBairro, string pCEP, string pUF)
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

        /// <summary>
        /// Cadastrar a agenda
        /// </summary>
        /// <param name="ID">ID do endereço selecionado</param>
        /// <returns>Tela de agenda</returns>
        [HttpGet]
        public IActionResult AgendaCadastro(int ID)
        {
            try
            {
                if (_ServiceEndereco.ConsultarEnderecoNutricionistaPorID(ID,
                    Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == Constantes.IDUsuarioLogado).ValueType)) == null)
                    return RedirectToAction("AgendaEndereco", "Agenda");

                return View(new AgendaCadastroDataHoraVM() { IdEndereco = ID });
            }
            catch
            {
                return RedirectToAction("AgendaEndereco", "Agenda");
            }
        }

        /// <summary>
        /// Cadastra a agenda
        /// </summary>
        /// <param name="pModel">Agenda a ser cadastrada</param>
        /// <returns>Sucesso ou erro ao cadastrar</returns>
        [HttpPost]
        public IActionResult AgendaCadastro(AgendaCadastroDataHoraVM pModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(pModel);

                DateTime dataInicio = Convert.ToDateTime(pModel.DataAgendaInicio);
                TimeSpan horaInicio = (TimeSpan)pModel.HoraInicio;

                DateTime dataFim = Convert.ToDateTime(pModel.DataAgendaFim);
                TimeSpan horaFim = (TimeSpan)pModel.HoraFim;

                DateTime dataHoraInicio = new DateTime(
                    dataInicio.Year,
                    dataInicio.Month,
                    dataInicio.Day,
                    horaInicio.Hours,
                    horaInicio.Minutes,
                    horaInicio.Seconds
                    );

                DateTime dataHoraFim = new DateTime(
                    dataFim.Year,
                    dataFim.Month,
                    dataFim.Day,
                    horaFim.Hours,
                    horaFim.Minutes,
                    horaFim.Seconds
                    );

                _ServiceAgenda.CadastrarAgenda(new Agenda(
                    Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == Constantes.IDUsuarioLogado).ValueType),
                    pModel.IdEndereco,
                    dataHoraInicio,
                    dataHoraFim
                    ));

                ViewData[Constantes.ViewDataMensagemRetorno] = $"Agenda com início no dia {dataInicio.ToString(Constantes.MascaraData)} e término no dia {dataFim.ToString(Constantes.MascaraData)}.{Environment.NewLine}Horário: {dataHoraInicio.ToString(Constantes.MascaraHoraMinuto)} às {dataHoraFim.ToString(Constantes.MascaraHoraMinuto)}.{Environment.NewLine}Cadastrado com sucesso!";
                return RedirectToAction("Index", "Agenda", new { mensagem = ViewData[Constantes.ViewDataMensagemRetorno].ToString() });
            }
            catch (Exception ex)
            {
                ViewData[Constantes.ViewDataMensagemErro] = ex.Message;
                return View(pModel);
            }
        }
        #endregion

        #region Lista Agenda
        /// <summary>
        /// Lista de agendas
        /// </summary>
        /// <param name="pIndiceInicial"></param>
        /// <param name="pRua"></param>
        /// <param name="pCidade"></param>
        /// <param name="pBairro"></param>
        /// <param name="pCEP"></param>
        /// <param name="pUF"></param>
        /// <param name="pDataInicio"></param>
        /// <param name="pDataFim"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(int pIndiceInicial, string pRua, string pCidade, string pBairro, string pCEP, string pUF, DateTime pDataInicio, DateTime pDataFim, string mensagem)
        {
            ViewData[Constantes.ViewDataMensagemRetorno] = mensagem;

            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == Constantes.IDUsuarioLogado).ValueType);
            List<Agenda> agendas = _ServiceAgenda.AgendasCadastradas(pDataInicio, pDataFim, idUsuario, false);
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

            List<AgendaListaContratoVM> listaAgendas = new List<AgendaListaContratoVM>();

            if (agendas.Any() && enderecosVm.Any())
            {
                listaAgendas = agendas.Join(enderecosVm,
                             a => a.IdEndereco,
                             e => e.ID,
                             (agenda, endereco) => new AgendaListaContratoVM()
                             {
                                 DataFim = agenda.DataFim,
                                 DataInicio = agenda.DataInicio,
                                 StatusDaAgenda = StatusAgendaEnum.Ativa,
                                 Endereco = endereco,
                                 IdAgenda = agenda.IdAgenda,
                                 IdUsuario = agenda.IdUsuario
                             }).ToList();
            }

            return View(listaAgendas.Skip(pIndiceInicial).Take(Constantes.QuantidadeRegistrosPorPagina));
        }
        #endregion

        #region Excluir agenda
        [HttpGet]
        public IActionResult ExcluirAgenda(int ID)
        {
            List<Agenda> agendas = _ServiceAgenda.AgendasCadastradas(DateTime.MinValue, DateTime.MinValue,
                Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == Constantes.IDUsuarioLogado).ValueType)
                , false);

            Agenda agenda = agendas.FirstOrDefault(c => c.IdAgenda == ID);

            if (agenda == null)
                return RedirectToAction("Index", "Agenda");

            Endereco endereco = _ServiceEndereco.ConsultarEnderecoNutricionistaPorID(agenda.IdEndereco, agenda.IdUsuario);

            AgendaListaContratoVM agendaVM = new AgendaListaContratoVM()
            {
                DataFim = agenda.DataFim,
                DataInicio = agenda.DataInicio,
                IdAgenda = agenda.IdAgenda,
                IdUsuario = agenda.IdUsuario,
                StatusDaAgenda = agenda.StatusAgenda,
                Endereco = new EnderecoVM()
                {
                    Bairro = endereco.Bairro,
                    CEP = endereco.CEP,
                    Cidade = endereco.Cidade,
                    Complemento = endereco?.Complemento,
                    Logradouro = endereco?.Logradouro,
                    Numero = endereco?.Numero,
                    UF = endereco.UF.GetDefaultValue()
                }
            };

            return View(agendaVM);
        }

        [HttpPost]
        public IActionResult ExcluirAgenda(AgendaListaContratoVM pModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(pModel);

                _ServiceAgenda.InativarAgenda(pModel.IdAgenda);

                return RedirectToAction("Index", "Agenda", new { mensagem = "Agenda excluída com sucesso!" });
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

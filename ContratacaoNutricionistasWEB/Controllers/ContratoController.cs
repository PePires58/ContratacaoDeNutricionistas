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
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using ContratacaoNutricionistas.Domain.Entidades.Agenda;
using ContratacaoNutricionistas.Domain.Entidades.Nutricionista;
using ContratacaoNutricionistas.Domain.Enumerados.Agenda;
using ContratacaoNutricionistas.Domain.Enumerados.Gerais;
using ContratacaoNutricionistas.Domain.Interfaces.Agenda;
using ContratacaoNutricionistas.Domain.Interfaces.Contrato;
using ContratacaoNutricionistas.Domain.Interfaces.Endereco;
using ContratacaoNutricionistasWEB.Models.Agenda;
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
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="ServiceEndereco">Serviços de endereço</param>
        /// <param name="ServiceAgenda">Serviços de agenda</param>
        /// <param name="ServiceContrato">Serviços de contrato</param>
        public ContratoController(IServiceEndereco ServiceEndereco, IServiceAgenda ServiceAgenda, IServiceContrato ServiceContrato)
        {
            _ServiceEndereco = ServiceEndereco;
            _ServiceAgenda = ServiceAgenda;
            _ServiceContrato = ServiceContrato;
        }
        #endregion

        #region Métodos

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
        public IActionResult LocalizarNutricionista(int pIndiceInicial, string pRua, string pCidade, string pBairro, string pCEP, string pUF, DateTime pDataInicio, DateTime pDataFim, string mensagem)
        {
            ViewData[Constantes.ViewDataMensagemRetorno] = mensagem;

            List<Agenda> agendas = _ServiceAgenda.AgendasCadastradas(pDataInicio, pDataFim, 0, true);
            List<int> nutricionistas = agendas.Select(c => c.IdUsuario).Distinct().ToList();
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
                    UF = c.UF.GetDescription()
                }).ToList();
            }

            AgendaListaContratoVM agendaContrato = new AgendaListaContratoVM();

            if (agendas.Any() && enderecosVm.Any())
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
                             id.IdUsuario == pIdNutricionista);
            }

            return View(agendaContrato);
        }

        [HttpPost]
        public IActionResult ContratarNutricionista(AgendaListaContratoVM pModel)
        {
            try
            {
                int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == Constantes.IDUsuarioLogado).ValueType);

                if (!ModelState.IsValid)
                    return View(pModel);

                _ServiceContrato.ContratarNutricionista(
                    new ContratacaoNutricionistas.Domain.Entidades.Contrato.Contrato(
                        pIdUsuario: idUsuario,
                        pIdNutricionista: pModel.IdUsuario,
                        pLogradouro: pModel.Endereco.Logradouro,
                        complemento: pModel.Endereco.Complemento,
                        numero: pModel.Endereco.Numero,
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
                return View();
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
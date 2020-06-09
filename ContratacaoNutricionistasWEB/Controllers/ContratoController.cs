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
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
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
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="ServiceEndereco">Serviços de endereço</param>
        /// <param name="ServiceAgenda">Serviços de agenda</param>
        public ContratoController(IServiceEndereco ServiceEndereco, IServiceAgenda ServiceAgenda)
        {
            _ServiceEndereco = ServiceEndereco;
            _ServiceAgenda = ServiceAgenda;
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
        #endregion

    }
}
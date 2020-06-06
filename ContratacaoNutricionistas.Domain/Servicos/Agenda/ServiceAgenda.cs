#region Histórico de manutenções
/*
 Data: 06/05/2020
 Programador: Pedro Henrique Pires
 Descrição: Implementação Inicial da classe.
 */
#endregion
using ContratacaoNutricionistas.Domain.Interfaces.Agenda;
using System;
using System.Collections.Generic;

namespace ContratacaoNutricionistas.Domain.Servicos.Agenda
{
    /// <summary>
    /// Serviço de agenda
    /// </summary>
    public class ServiceAgenda : IServiceAgenda
    {
        #region Propriedades

        /// <summary>
        /// Interface que faz os comandos com o banco de dados para nutricionista
        /// </summary>
        private readonly IAgendaRepository _AgendaRepository;
        #endregion

        #region Construtores
        /// <summary>
        /// Serviço de agenda
        /// </summary>
        /// <param name="AgendaRepository">Repositório de agenda</param>
        public ServiceAgenda(IAgendaRepository AgendaRepository)
        {
            _AgendaRepository = AgendaRepository;
        }

        #endregion

        #region Métodos
        /// <summary>
        /// Cadastra uma agenda
        /// </summary>
        /// <param name="agendaCadastro">Cadastro de agenda</param>
        public void CadastrarAgenda(Entidades.Agenda.Agenda agendaCadastro)
        {
            if (agendaCadastro == null)
                throw new ArgumentException("Os dados da agenda são obrigatórios.");
            if (agendaCadastro.DataInicio < DateTime.Now)
                throw new ArgumentException("Data de início não pode ser menor que o dia atual");
            if (agendaCadastro.DataFim < DateTime.Now)
                throw new ArgumentException("Data de término não pode ser menor que o dia atual");
            if (agendaCadastro.DataInicio > agendaCadastro.DataFim)
                throw new ArgumentException("A data de início não pode ser maior que a data de término");
            if (agendaCadastro.DataInicio.TimeOfDay > agendaCadastro.DataFim.TimeOfDay)
                throw new ArgumentException("O horário de início não pode ser maior que o horário de término");

            if (_AgendaRepository.AgendaJaCadastrada(
                agendaCadastro.DataInicio,
                agendaCadastro.DataFim,
                agendaCadastro.IdEndereco,
                agendaCadastro.IdUsuario)
            )
                throw new Exception("Já existe uma agenda cadastrada para a(s) data(s) e horário(s) informado(s).");

            _AgendaRepository.CadastrarAgenda(agendaCadastro);
        }

        /// <summary>
        /// Agendas cadastradas
        /// </summary>
        /// <param name="pDataInicio">Data de início</param>
        /// <param name="pDataFim">Data fim</param>
        /// <param name="pIdUsuario">Id do usuário</param>
        /// <returns>Uma lista de agenda</returns>
        public List<Entidades.Agenda.Agenda> AgendasCadastradas(DateTime pDataInicio, DateTime pDataFim, int pIdUsuario)
        {
            if (pIdUsuario <= 0)
                throw new ArgumentException("Usuário náo identificado");
            return _AgendaRepository.AgendasCadastradas(pDataInicio, pDataFim, pIdUsuario);
        }
        #endregion

    }
}

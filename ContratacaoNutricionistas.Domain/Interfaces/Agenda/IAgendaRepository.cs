#region Histórico de manutenções
/*
 Data: 06/05/2020
 Programador: Pedro Henrique Pires
 Descrição: Implementação Inicial da classe.
 */
#endregion
using System;
using System.Collections.Generic;

namespace ContratacaoNutricionistas.Domain.Interfaces.Agenda
{
    /// <summary>
    /// Repositório de comandos de agenda no banco de dados
    /// </summary>
    public interface IAgendaRepository
    {
        /// <summary>
        /// Cadastar a agenda
        /// </summary>
        /// <param name="agendaCadastro">Agenda</param>
        void CadastrarAgenda(Entidades.Agenda.Agenda agendaCadastro);

        /// <summary>
        /// Desativar a agenda por endereço excluído
        /// </summary>
        /// <param name="pIdUsuario">Id do usuário</param>
        /// <param name="pIdEndereco">Id do endereço</param>
        void DesativarAgendaPorEnderecoExcluido(int pIdUsuario, int pIdEndereco);

        /// <summary>
        /// Informa se a agenda já foi cadastrada para o endereço informado
        /// para as datas e horas informadas
        /// </summary>
        /// <param name="pDataHoraInicio">Data Hora início</param>
        /// <param name="pDataHoraFim">Data Hora fim</param>
        /// <param name="pIdEndereco">ID do endereço</param>
        /// <param name="pIdUsuario">ID do usuário</param>
        /// <returns></returns>
        bool AgendaJaCadastrada(DateTime pDataHoraInicio, DateTime pDataHoraFim, int pIdEndereco, int pIdUsuario);

        /// <summary>
        /// Agendas cadastradas
        /// </summary>
        /// <param name="pDataInicio">Data início</param>
        /// <param name="pDataFim">Data fim</param>
        /// <param name="pIdUsuario">Id do usuário</param>
        /// <returns>Lista de agendas</returns>
        List<Entidades.Agenda.Agenda> AgendasCadastradas(DateTime pDataInicio, DateTime pDataFim, int pIdUsuario);
    }
}

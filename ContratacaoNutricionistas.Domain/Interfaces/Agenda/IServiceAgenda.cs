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
    /// Serviço de agenda
    /// </summary>
    public interface IServiceAgenda
    {
        /// <summary>
        /// Cadastar a agenda
        /// </summary>
        /// <param name="agendaCadastro">Agenda</param>
        void CadastrarAgenda(Entidades.Agenda.Agenda agendaCadastro);

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

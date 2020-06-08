#region Histórico de manutenções
/*
 Data: 06/05/2020
 Programador: Pedro Henrique Pires
 Descrição: Implementação Inicial da classe.
 */

/*
Data: 08/06/2020
Programador: Pedro Henrique Pires
Descrição: Inclusão para permitir paciente consultar as agendas.
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
        /// <param name="pIsPaciente">Paciente consultando</param>
        /// <returns>Lista de agendas</returns>
        List<Entidades.Agenda.Agenda> AgendasCadastradas(DateTime pDataInicio, DateTime pDataFim, int pIdUsuario, bool pIsPaciente);
    }
}

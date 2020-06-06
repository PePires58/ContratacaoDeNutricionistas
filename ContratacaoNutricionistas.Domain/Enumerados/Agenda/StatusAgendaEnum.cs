#region Histórico de manutenções
/*
 Data: 06/05/2020
 Programador: Pedro Henrique Pires
 Descrição: Implementação Inicial da classe.
 */
#endregion
using System.ComponentModel;

namespace ContratacaoNutricionistas.Domain.Enumerados.Agenda
{
    /// <summary>
    /// Status da agenda
    /// </summary>
    public enum StatusAgendaEnum
    {
        /// <summary>
        /// Ativa
        /// </summary>
        [Description("Ativa"),DefaultValue("A")]
        Ativa,

        /// <summary>
        /// Desativada
        /// </summary>
        [Description("Desativada"),DefaultValue("D")]
        Desativada,

        /// <summary>
        /// Endereço excluído
        /// </summary>
        [Description("Endereço excluído"),DefaultValue("EE")]
        EnderecoExcluido
    }
}

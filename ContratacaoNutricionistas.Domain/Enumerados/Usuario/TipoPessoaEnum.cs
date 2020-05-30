#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial do enumerado de tipo de pessoa
 */
#endregion

using System.ComponentModel;

namespace ContratacaoNutricionistas.Domain.Enumerados.Usuario
{
    /// <summary>
    /// Enumerado para tipos de pessoa
    /// </summary>
    public enum TipoPessoaEnum
    {
        /// <summary>
        /// Não definido
        /// </summary>
        [Description(""),DefaultValue("")]
        NaoDefinido = -1,

        /// <summary>
        /// Pessoa física
        /// </summary>
        [Description("Física"), DefaultValue("0")]
        Fisica = 0
    }
}

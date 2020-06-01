#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da interface de comandos de banco para nutricionista
 */
#endregion

namespace ContratacaoNutricionistas.Domain.Interfaces.Nutricionista
{
    /// <summary>
    /// Interface da classe de comandos com o banco de dados para nutricionista
    /// </summary>
    public interface INutricionistaRepository
    {
        /// <summary>
        /// Método que cadastra um nutricionista
        /// </summary>
        /// <param name="pNutricionistaCadastro">Nutricionista a ser cadastrado</param>
        void CadastrarNutricionista(Entidades.Nutricionista.NutricionistaCadastro pNutricionistaCadastro);

        /// <summary>
        /// Método que retorna um nutricionista para alteração
        /// </summary>
        /// <param name="pID">ID do nutricionsta</param>
        /// <returns>Nutricionista ou NULL</returns>
        Entidades.Nutricionista.NutricionistaAlteracao  ConsultarNutricionistaPorID(int pID);

        /// <summary>
        /// Altera os dados do nutricionista
        /// </summary>
        /// <param name="pNutricionistaAlteracao">Nutricionista a ser alterado</param>
        void AlterarDadosNutricionista(Entidades.Nutricionista.NutricionistaAlteracao pNutricionistaAlteracao);
    }
}

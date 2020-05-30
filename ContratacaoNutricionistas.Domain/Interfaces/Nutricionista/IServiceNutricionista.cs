#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da Interface que faz os serviços para o nutricionista
 */

/*
* Programador: Pedro Henrique Pires
* Data: 30/05/2020
* Implementação: Inclusão de métodos de alteração e consulta
*/
#endregion


namespace ContratacaoNutricionistas.Domain.Interfaces.Nutricionista
{
    /// <summary>
    /// Interface que faz os serviços para o nutricionista
    /// </summary>
    public interface IServiceNutricionista
    {
        /// <summary>
        /// Método que cadastra um nutricionista
        /// </summary>
        /// <param name="pNutricionistaCadastro">Nutricionista a ser cadastrado</param>
        void CadastrarNutricionista(Entidades.Nutricionista.NutricionistaCadastro pNutricionistaCadastro);

        /// <summary>
        /// Método que consulta o nutricionista
        /// </summary>
        /// <param name="pID">ID do nutricionista</param>
        /// <returns>Retorna o nutricionista ou NULL</returns>
        Entidades.Nutricionista.NutricionistaAlteracao ConsultarNutricionistaPorID(int pID);

        /// <summary>
        /// Altera os dados do nutricionista
        /// </summary>
        /// <param name="pNutricionistaAlteracao">Nutricionista para ser alterado</param>
        void AlterarDadosNutricionista(Entidades.Nutricionista.NutricionistaAlteracao pNutricionistaAlteracao);
    }
}

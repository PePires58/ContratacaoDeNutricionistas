#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da interface de comandos de banco para nutricionista
 */

/*
* Programador: Pedro Henrique Pires
* Data: 03/06/2020
* Implementação: Implementação de cadastro/alteração de endereço e busca de endereço por CEP
*/

/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Migrando para classe de endereço
*/
#endregion

using ContratacaoNutricionistas.Domain.Entidades.Nutricionista;

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
        void CadastrarNutricionista(NutricionistaCadastro pNutricionistaCadastro);

        /// <summary>
        /// Método que retorna um nutricionista para alteração
        /// </summary>
        /// <param name="pID">ID do nutricionsta</param>
        /// <returns>Nutricionista ou NULL</returns>
        NutricionistaAlteracao ConsultarNutricionistaPorID(int pID);

        /// <summary>
        /// Altera os dados do nutricionista
        /// </summary>
        /// <param name="pNutricionistaAlteracao">Nutricionista a ser alterado</param>
        void AlterarDadosNutricionista(NutricionistaAlteracao pNutricionistaAlteracao);

    }
}

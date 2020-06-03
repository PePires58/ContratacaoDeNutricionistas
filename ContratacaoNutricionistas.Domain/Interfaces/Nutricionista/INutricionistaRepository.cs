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
#endregion

using System.Collections.Generic;
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

        /// <summary>
        /// Método que cadastra um endereço
        /// </summary>
        /// <param name="pEndereco">Endereço a ser cadastrado</param>
        void CadastrarEndereco(Entidades.Nutricionista.Endereco pEndereco);

        /// <summary>
        /// Retorna uma lista de endereços
        /// </summary>
        /// <param name="pIdUsuario">ID do usuário</param>
        /// <param name="pRua">Rua</param>
        /// <param name="pCidade">Cidade</param>
        /// <param name="pBairro">Bairro</param>
        /// <param name="pCEP">CEP</param>
        /// <param name="pUF">UF</param>
        /// <returns>Uma lista de endereços</returns>
        List<Endereco> EnderecosCadastrados(int pIdUsuario, string pRua, string pCidade, string pBairro, string pCEP, string pUF);

        /// <summary>
        /// Consulta o endereço do nutricionista pelo ID
        /// </summary>
        /// <param name="pID">ID do nutricionista</param>
        /// <param name="pIDUsuario">ID do usuário</param>
        /// <returns>Endereço ou null</returns>
        Endereco ConsultarEnderecoNutricionistaPorID(int pID, int pIDUsuario);

        /// <summary>
        /// Altera os dados do endereço
        /// </summary>
        /// <param name="pEndereco">Endereço a ser alterado</param>
        void AlterarDadosEndereco(Endereco pEndereco);
    }
}

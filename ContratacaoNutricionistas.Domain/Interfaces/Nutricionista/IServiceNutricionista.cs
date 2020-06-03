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

/*
 * Programador: Pedro Henrique Pires
 * Data: 03/06/2020
 * Implementação: Implementação de cadastro/alteração de endereço e consulta por CEP
 */
#endregion


using System.Collections.Generic;
using ContratacaoNutricionistas.Domain.Entidades.Nutricionista;

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

        /// <summary>
        /// Método que cadastra um endereço
        /// </summary>
        /// <param name="pEndereco">Endereço a ser cadastrado</param>
        void CadastrarEndereco(Entidades.Nutricionista.Endereco pEndereco);

        /// <summary>
        /// Consulta um endereço a partir do CEP
        /// </summary>
        /// <param name="pCEP">CEP</param>
        /// <returns>Endereço</returns>
        Entidades.Nutricionista.Endereco ConsultarEnderecoPorCEP(string pCEP, int pIdUsuario);

        /// <summary>
        /// Retorna uma lista de endereços cadastrados
        /// </summary>
        /// <param name="pIdUsuario">ID do usuário</param>
        /// <param name="pRua">Rua</param>
        /// <param name="pCidade">Cidade</param>
        /// <param name="pBairro">Bairro</param>
        /// <param name="pCEP">CEP</param>
        /// <param name="pUF">UF</param>
        /// <returns>Lista de endereços</returns>
        List<Entidades.Nutricionista.Endereco> EnderecosCadastrados(int pIdUsuario, string pRua, string pCidade, string pBairro, string pCEP, string pUF);

        /// <summary>
        /// Consulta um endereço pelo ID
        /// </summary>
        /// <param name="pID">ID do endereço</param>
        /// <param name="pIDUsuario">ID do usuário</param>
        /// <returns>Endereço ou null</returns>
        Endereco ConsultarEnderecoNutricionistaPorID(int pID, int pIDUsuario);

        /// <summary>
        /// Altera os dados do endereço
        /// </summary>
        /// <param name="endereco">Endereço a ser alterado</param>
        void AlterarDadosEndereco(Endereco endereco);
    }
}

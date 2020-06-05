#region Histórico de manutenção
/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Implementação inicial.
*/
#endregion
using System.Collections.Generic;

namespace ContratacaoNutricionistas.Domain.Interfaces.Endereco
{
    /// <summary>
    /// Interface para o repositório de endereço
    /// </summary>
    public interface IEnderecoRepository
    {
        /// <summary>
        /// Método que cadastra um endereço
        /// </summary>
        /// <param name="pEndereco">Endereço a ser cadastrado</param>
        void CadastrarEndereco(Entidades.Nutricionista.Endereco pEndereco);

        /// <summary>
        /// Altera os dados do endereço
        /// </summary>
        /// <param name="pEndereco">Endereço a ser alterado</param>
        void AlterarDadosEndereco(Entidades.Nutricionista.Endereco pEndereco);

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
        List<Entidades.Nutricionista.Endereco> EnderecosCadastrados(int pIdUsuario, string pRua, string pCidade, string pBairro, string pCEP, string pUF);

        /// <summary>
        /// Consulta um endereço pelo ID
        /// </summary>
        /// <param name="pID">ID do endereço</param>
        /// <param name="pIDUsuario">ID do usuário</param>
        /// <returns>Endereço ou null</returns>
        Entidades.Nutricionista.Endereco ConsultarEnderecoNutricionistaPorID(int pID, int pIDUsuario);
    }
}

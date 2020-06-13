#region Histórico de manutenção
/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Implementação inicial.
*/

/*
Data: 13/06/2020
Programador: Pedro Henrique Pires
Descrição: Excluir endereço
*/

#endregion
using System.Collections.Generic;

namespace ContratacaoNutricionistas.Domain.Interfaces.Endereco
{
    /// <summary>
    /// Interface de serviço para endereço
    /// </summary>
    public interface IServiceEndereco
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

        /// <summary>
        /// Consulta um endereço a partir do CEP
        /// </summary>
        /// <param name="pCEP">CEP</param>
        /// <returns>Endereço</returns>
        Entidades.Nutricionista.Endereco ConsultarEnderecoPorCEP(string pCEP, int pIdUsuario);

        /// <summary>
        /// Exclui um endereço
        /// </summary>
        /// <param name="pIdUsuario">ID do usuário</param>
        /// <param name="pIdEndereco">ID do endereço</param>
        void ExcluirEndereco(int pIdUsuario, int pIdEndereco);
    }
}

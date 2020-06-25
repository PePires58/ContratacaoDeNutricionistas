#region Histórico de manutenção
/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Implementação inicial.
*/
/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Implementação do método de consulta de usuário para autenticação.
*/
/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Implementação de restrição por CPF e tipo de usuário.
*/
#endregion
using System.Security.Claims;
using ContratacaoNutricionistas.Domain.Interfaces.Repository;

namespace ContratacaoNutricionistas.Domain.Interfaces.Usuario
{
    /// <summary>
    /// Serviços para o usuário
    /// </summary>
    public interface IServiceUsuario : IRepositoryBase
    {
        /// <summary>
        /// Verifica se o login existe
        /// </summary>
        /// <param name="pLogin">Login existe</param>
        /// <param name="pCPF">CPF</param>
        /// <param name="pTipoUsuario">Tipo de usuário</param>
        /// <returns></returns>
        bool LoginExiste(string pLogin, string pCPF, string pTipoUsuario);

        /// <summary>
        /// Retorna o objeto de autenticação
        /// </summary>
        /// <param name="pLogin">Login</param>
        /// <param name="pSenha">Senha</param>
        /// <returns></returns>
        ClaimsPrincipal RetornaAutenticacaoUsuario(string pLogin, string pSenha);
    }
}

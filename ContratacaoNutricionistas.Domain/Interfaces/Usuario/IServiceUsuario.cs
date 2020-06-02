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
#endregion
using System.Security.Claims;

namespace ContratacaoNutricionistas.Domain.Interfaces.Usuario
{
    /// <summary>
    /// Serviços para o usuário
    /// </summary>
    public interface IServiceUsuario
    {
        /// <summary>
        /// Verifica se o login existe
        /// </summary>
        /// <param name="pLogin">Login existe</param>
        /// <returns></returns>
        bool LoginExiste(string pLogin);

        /// <summary>
        /// Retorna o objeto de autenticação
        /// </summary>
        /// <param name="pLogin">Login</param>
        /// <param name="pSenha">Senha</param>
        /// <returns></returns>
        ClaimsPrincipal RetornaAutenticacaoUsuario(string pLogin, string pSenha);
    }
}

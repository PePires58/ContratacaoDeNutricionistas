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
* Implementação: Restrição pelo CPF e tipo de usuário.
*/

/*
Data: 26/06/2020
Programador: Tatiane
Descrição: Herdando da interface.
*/
#endregion
using ContratacaoNutricionistas.Domain.Entidades.Usuario;
using System;
using System.Collections.Generic;
using System.Text;
using ContratacaoNutricionistas.Domain.Interfaces.Repository;

namespace ContratacaoNutricionistas.Domain.Interfaces.Paciente
{
    /// <summary>
    /// Classe que realiza os comandos no banco para usuários
    /// </summary>
    public interface IUsuarioRepository : IRepositoryBase
    {
        /// <summary>
        /// Login já existe
        /// </summary>
        /// <param name="pLogin">Login</param>
        /// <param name="pCPF">CPF</param>
        /// <param name="pTipoUsuario">Tipo de usuário</param>
        /// <returns>Verifica se o login já existe ou não</returns>
        bool LoginExiste(string pLogin, string pCPF, string pTipoUsuario);

        /// <summary>
        /// Consulta um usuário para autenticação
        /// </summary>
        /// <param name="pLogin">Login</param>
        /// <param name="pSenha">Senha</param>
        /// <returns>Usuário para autenticação ou null</returns>
        UsuarioAutenticacao ConsultarUsuarioAutenticacao(string pLogin, string pSenha);
    }
}

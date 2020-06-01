#region Histórico de manutenção
 /*
 * Programador: Pedro Henrique Pires
 * Data: 01/06/2020
 * Implementação: Implementação inicial.
 */
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace ContratacaoNutricionistas.Domain.Interfaces.Paciente
{
    /// <summary>
    /// Classe que realiza os comandos no banco para usuários
    /// </summary>
    public interface IUsuarioRepository 
    {
        /// <summary>
        /// Login já existe
        /// </summary>
        /// <param name="pLogin">Login</param>
        /// <returns>Verifica se o login já existe ou não</returns>
        bool LoginExiste(string pLogin);
    }
}

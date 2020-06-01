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
    }
}

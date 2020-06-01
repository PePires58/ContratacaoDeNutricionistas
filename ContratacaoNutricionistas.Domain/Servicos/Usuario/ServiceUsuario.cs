#region Histórico de manutenção
 /*
 * Programador: Pedro Henrique Pires
 * Data: 01/06/2020
 * Implementação: Implementação inicial.
 */
#endregion
using ContratacaoNutricionistas.Domain.Interfaces.Paciente;
using ContratacaoNutricionistas.Domain.Interfaces.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContratacaoNutricionistas.Domain.Servicos.Usuario
{
    /// <summary>
    /// Serviços para usuário
    /// </summary>
    public class ServiceUsuario : IServiceUsuario
    {
        #region Constantes
        /// <summary>
        /// Tamanho máximo para o campo login
        /// </summary>
        private const int TamanhoMaximoLogin = 20;
        #endregion

        #region Propriedades
        /// <summary>
        /// Interface que faz os comandos com o banco de dados para nutricionista
        /// </summary>
        private readonly IUsuarioRepository _UsuarioRepository;
        #endregion

        #region Construtor
        /// <summary>
        /// Construtor da classe de serviço
        /// </summary>
        /// <param name="pUsuarioRepository">Interface que faz os comandos com o banco de dados para nutricionista</param>
        public ServiceUsuario(IUsuarioRepository pUsuarioRepository)
        {
            _UsuarioRepository = pUsuarioRepository;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Verifica se o login já existe
        /// </summary>
        /// <param name="pLogin">Login já existe</param>
        /// <returns>Returna se o usuário existe ou não</returns>
        public bool LoginExiste(string pLogin)
        {
            if (string.IsNullOrEmpty(pLogin))
                throw new ArgumentException("O login é obrigatório.");
            else if (pLogin.Length > TamanhoMaximoLogin)
                throw new ArgumentException(string.Format("O tamanho máximo do campo Login é de {0} caracteres.", TamanhoMaximoLogin));

            return _UsuarioRepository.LoginExiste(pLogin);
        }
        #endregion

    }
}

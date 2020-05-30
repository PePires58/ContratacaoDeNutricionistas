#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de login do usuário
 */
#endregion

using ContratacaoNutricionistas.Domain.Enumerados.Usuario;

namespace ContratacaoNutricionistas.Domain.Entidades.Paciente.Usuario
{
    /// <summary>
    /// View Model para login de usuários
    /// </summary>
    public abstract class UsuarioLogin
    {
        /// <summary>
        /// Login
        /// </summary>
        public abstract string Login { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        public abstract string Senha { get; set; }

        /// <summary>
        /// Tipo de usuário
        /// </summary>
        public abstract TipoUsuarioEnum TipoUsuario { get; }
    }
}

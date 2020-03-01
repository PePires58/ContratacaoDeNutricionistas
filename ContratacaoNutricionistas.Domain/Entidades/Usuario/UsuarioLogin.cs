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
        public abstract TipoUsuarioEnum TipoUsuario { get; set; }
    }
}

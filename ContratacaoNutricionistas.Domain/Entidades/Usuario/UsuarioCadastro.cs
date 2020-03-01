using ContratacaoNutricionistas.Domain.Enumerados.Usuario;

namespace ContratacaoNutricionistas.Domain.Entidades.Paciente.Usuario
{
    /// <summary>
    /// Classe para cadastro de usuários
    /// </summary>
    public abstract class UsuarioCadastro : UsuarioCadastroAlteracao
    {
        /// <summary>
        /// Tipo de pessoa
        /// </summary>
        public abstract TipoPessoaEnum TipoPessoa { get; set; }
    }
}

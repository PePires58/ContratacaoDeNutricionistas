
namespace ContratacaoNutricionistas.Domain.Entidades.Paciente.Usuario
{
    /// <summary>
    /// Classe abstrata para cadastro e alteração
    /// </summary>
    public abstract class UsuarioCadastroAlteracao : UsuarioLogin
    {
        /// <summary>
        /// Nome
        /// </summary>
        public abstract string Nome { get; set; }

        /// <summary>
        /// Telefone
        /// </summary>
        public abstract string Telefone { get; set; }



    }
}

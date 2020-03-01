
namespace ContratacaoNutricionistas.Domain.Entidades.Paciente.Usuario
{
    /// <summary>
    /// Classe para alteração
    /// </summary>
    public abstract class UsuarioAlteracao : UsuarioCadastroAlteracao
    {
        /// <summary>
        /// ID
        /// </summary>
        public abstract int ID { get; set; }     
    }
}

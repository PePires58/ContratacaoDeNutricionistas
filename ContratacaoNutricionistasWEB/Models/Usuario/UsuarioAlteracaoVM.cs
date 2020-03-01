using System.ComponentModel.DataAnnotations;

namespace ContratacaoNutricionistasWEB.Models.Usuario
{
    /// <summary>
    /// Classe para alteração
    /// </summary>
    public abstract class UsuarioAlteracaoVM : UsuarioCadastroAlteracaoVM
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage ="O ID é obrigatório")]
        public int ID { get; set; }     
    }
}

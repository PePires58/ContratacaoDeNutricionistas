using ContratacaoNutricionistas.Domain.Enumerados.Usuario;
using System.ComponentModel.DataAnnotations;

namespace ContratacaoNutricionistasWEB.Models.Usuario
{
    /// <summary>
    /// View Model para login de usuários
    /// </summary>
    public class UsuarioLoginVM
    {
        /// <summary>
        /// Login
        /// </summary>
        [Display(Name = "Login", Prompt ="Ex.: FulanoPaciente")]
        [MaxLength(50, ErrorMessage ="O tamanho máximo do campo Login é 50 caracteres")]
        [Required(ErrorMessage = "O campo Login é obrigatório")]
        public string Login { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        [Display(Name = "Senha", Prompt ="Digite sua senha")]
        [DataType(DataType.Password)]
        [MaxLength(20, ErrorMessage = "O tamanho máximo do campo Senha é 20 caracteres")]
        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string Senha { get; set; }

        /// <summary>
        /// Tipo de usuário
        /// </summary>
        [Display(Name ="Tipo de usuário")]
        [Required(ErrorMessage ="O tipo de usuário é obrigatório")]
        public TipoUsuarioEnum TipoUsuario { get; set; }
    }
}

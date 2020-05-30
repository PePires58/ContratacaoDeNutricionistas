#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de cadastro ou alteração de usuários
 */
#endregion

using System.ComponentModel.DataAnnotations;

namespace ContratacaoNutricionistasWEB.Models.Usuario
{
    /// <summary>
    /// Classe abstrata para cadastro e alteração
    /// </summary>
    public abstract class UsuarioCadastroAlteracaoVM : UsuarioLoginVM
    {
        /// <summary>
        /// Nome
        /// </summary>
        [Display(Name = "Nome completo",Prompt ="Ex.: Fulano de tal")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo do campo Nome é 50 caracteres")]
        [Required(ErrorMessage = "O campo Nome completo é obrigatório")]
        public string Nome { get; set; }

        /// <summary>
        /// Telefone
        /// </summary>
        [Display(Name = "Telefone", Prompt = "Ex.: (011) 91242-1234")]
        [RegularExpression(@"^\(\d{3}\)\d{4}\-\d{4}$", ErrorMessage ="Telefone deve estar no formato (011)91242-1234")]
        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        public string Telefone { get; set; }

        /// <summary>
        /// Campo de confirmação de senha
        /// </summary>
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo Conformar senha é obrigatório.")]
        [Display(Name = "Confirmar senha",Prompt ="Confirme sua senha")]
        [Compare(nameof(Senha), ErrorMessage = "As senhas não conferem")]
        [MaxLength(20, ErrorMessage = "O tamanho máximo do campo Confirmar senha é 20 caracteres")]
        public string SenhaConfirmacao { get; set; }
    }
}

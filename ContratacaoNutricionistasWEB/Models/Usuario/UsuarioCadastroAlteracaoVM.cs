#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de cadastro ou alteração de usuários
 */

/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Arrumando mensagem e mascara de data.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Arrumando mensagem e mascara de data.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 04/07/2020
* Implementação: Arrumando prompt para usar css do materialize.
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
        [Display(Name = "Nome completo")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo do campo Nome é 50 caracteres")]
        [Required(ErrorMessage = "O campo Nome completo é obrigatório")]
        public string Nome { get; set; }

        /// <summary>
        /// Telefone
        /// </summary>
        [Display(Name = "Telefone")]
        [RegularExpression(@"^\([1-9]{2}\) (9)?[0-9]{4}\-[0-9]{4}$", ErrorMessage = "Telefone deve estar no formato (11) 91242-1234 ou (11) 1242-1234")]
        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        public string Telefone { get; set; }

        /// <summary>
        /// Campo de confirmação de senha
        /// </summary>
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo Confirmar senha é obrigatório.")]
        [Display(Name = "Confirmar senha",Prompt ="Confirme sua senha")]
        [Compare(nameof(Senha), ErrorMessage = "As senhas não conferem")]
        [MaxLength(20, ErrorMessage = "O tamanho máximo do campo Confirmar senha é 20 caracteres")]
        public string SenhaConfirmacao { get; set; }
    }
}

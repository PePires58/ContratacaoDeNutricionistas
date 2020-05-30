#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe cadastro de usuários
 */
#endregion

using ContratacaoNutricionistas.Domain.Enumerados.Usuario;
using System.ComponentModel.DataAnnotations;

namespace ContratacaoNutricionistasWEB.Models.Usuario
{
    /// <summary>
    /// Classe para cadastro de usuários
    /// </summary>
    public abstract class UsuarioCadastroVM : UsuarioCadastroAlteracaoVM
    {
        /// <summary>
        /// CPF 
        /// </summary>
        [Display(Name ="CPF",Prompt ="Ex.: 123.123.123-42")]
        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$", ErrorMessage ="O campo CPF deve estar no formato 000.000.000-00")]
        public string CPF { get; set; }

    }
}

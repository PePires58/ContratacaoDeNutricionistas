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
        /// Tipo de pessoa
        /// </summary>
        [Display(Name = "Tipo de pessoa")]
        [Required(ErrorMessage ="O tipo de pessoa é obrigatório")]
        public TipoPessoaEnum TipoPessoa { get; set; }

        /// <summary>
        /// CPF 
        /// </summary>
        [Display(Name ="CPF",Prompt ="Ex.: 123.123.123-42")]
        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$", ErrorMessage ="O campo CPF deve estar no formato 000.000.000-00")]
        public string CPF { get; set; }

        /// <summary>
        /// CNPJ
        /// </summary>
        [Display(Name ="CNPJ",Prompt = "Ex.: 14.123.1234/0001-88")]
        [Required(ErrorMessage = "O campo CNPJ é obrigatório")]
        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$", ErrorMessage ="O campo CNPJ deve estar no formato 00.000.000/0000-00")]
        public string CNPJ { get; set; }
    }
}

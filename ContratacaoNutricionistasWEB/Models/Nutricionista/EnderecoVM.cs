#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 03/06/2020
 * Implementação: Implementação Inicial da classe de endereço
 */

/*
* Programador: Pedro Henrique Pires
* Data: 05/06/2020
* Implementação: Ajuste nas propriedades.
*/

/*
Data: 08/06/2020
Programador: Pedro Henrique Pires
Descrição: Incluindo validação para não dar erro ao cadastrar um endereço sem preencher os dados
*/
#endregion

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ContratacaoNutricionistasWEB.Models.Nutricionista
{
    /// <summary>
    /// Classe de endereço
    /// </summary>
    public class EnderecoVM
    {
        #region Propriedades

        [Display(Name = "Bairro", Prompt = "Bairro preenchido automáticamente a partir do CEP")]
        [Required(ErrorMessage = "O campo Endereço é obrigatório")]
        [StringLength(50, ErrorMessage = "O tamanho máximo do campo Bairro é de 50 caracteres")]
        public string Bairro { get; set; }

        [Display(Name = "Cidade", Prompt = "Cidade preenchida automáticamente a partir do CEP")]
        [Required(ErrorMessage = "A campo Cidade é obrigatória")]
        [StringLength(30, ErrorMessage = "O tamanho máximo do campo Cidade é de 30 caracteres")]
        public string Cidade { get; set; }

        [Display(Name = "CEP", Prompt = "00000-000")]
        [Required(ErrorMessage = "O campo CEP é obrigatório")]
        [RegularExpression(@"^\d{5}\-\d{3}$", ErrorMessage = "O campo CEP deve estar no formato 00000-000")]
        [StringLength(9, ErrorMessage = "O tamanho máximo do campo CEP é de 9 caracteres")]
        public string CEP { get; set; }

        [Display(Name = "Complemento", Prompt = "Ex.: Apto - Bloco B")]
        [StringLength(255, ErrorMessage = "O tamanho máximo do campo Complemento é de 255 caracteres")]
        public string Complemento { get; set; }

        [Display(Name = "Logradouro", Prompt = "Logradouro preenchido automáticamente a partir do CEP", AutoGenerateField = true)]
        [Required(ErrorMessage = "O campo Logradouro é obrigatório")]
        [StringLength(100, ErrorMessage = "O tamanho máximo do campo Logradouro é de 100 caracteres")]
        public string Logradouro { get; set; }

        [Display(Name = "UF", Prompt = "UF preenchida automáticamente a partir do CEP")]
        [Required(ErrorMessage = "O campo UF é obrigatório")]
        [StringLength(100, ErrorMessage = "O tamanho máximo do campo UF é de 100 caracteres")]
        public string UF { get; set; }

        [Display(Name = "Número", Prompt = "Ex.: 2020")]
        public uint? Numero { get; set; }

        public string EnderecoCompleto => string.Concat(Logradouro?.ToString(), " - ", (string.IsNullOrEmpty(Numero?.ToString()) ? "SEM NÚMERO" : "Nº. " + Numero.ToString())
                    , (string.IsNullOrEmpty(Complemento) ? "" : " - " + Complemento), ". ", Cidade, ", ", Bairro);

        #endregion

        #region Métodos privados
        private void ValidarDados(string pLogradouro, string pCidade, string pBairro, string pCEP)
        {
            if (string.IsNullOrEmpty(pLogradouro))
                throw new ArgumentException($"O {nameof(Logradouro)} é obrigatório.");
            if (string.IsNullOrEmpty(pBairro))
                throw new ArgumentException($"O {nameof(Bairro)} é obrigatório.");
            if (string.IsNullOrEmpty(pCEP))
                throw new ArgumentException($"O {nameof(CEP)} é obrigatório.");
            if (!new Regex(@"^\d{5}\-\d{3}$").IsMatch(pCEP))
                throw new Exception($"O {nameof(CEP)} deve estar no formato 99999-999");
            if (string.IsNullOrEmpty(pCidade))
                throw new ArgumentException($"A {nameof(Cidade)} é obrigatória.");
        }
        #endregion
    }
}

#region Histórico de manutenção
/*
Data: 19/06/2020
Programador: Pedro Henrique Pires
Descrição: Implementação inicial.
*/
#endregion
using System;
using System.ComponentModel.DataAnnotations;

namespace ContratacaoNutricionistasWEB.Models.Contrato
{
    public class FiltroLocalizarNutricionistaVM
    {

        [Display(Name = "Logradouro", Prompt = "Logradouro preenchido automáticamente a partir do CEP", AutoGenerateField = true)]
        [Required(ErrorMessage = "O campo Logradouro é obrigatório")]
        [StringLength(100, ErrorMessage = "O tamanho máximo do campo Logradouro é de 100 caracteres")]
        public string Logradouro { get; set; }

        [Display(Name = "Cidade", Prompt = "Cidade preenchida automáticamente a partir do CEP")]
        [StringLength(30, ErrorMessage = "O tamanho máximo do campo Cidade é de 30 caracteres")]
        public string Cidade { get; set; }

        [Display(Name = "Bairro", Prompt = "Bairro preenchido automáticamente a partir do CEP")]
        [StringLength(50, ErrorMessage = "O tamanho máximo do campo Bairro é de 50 caracteres")]
        public string Bairro { get; set; }

        [Display(Name = "CEP", Prompt = "00000-000")]
        [RegularExpression(@"^\d{5}\-\d{3}$", ErrorMessage = "O campo CEP deve estar no formato 00000-000")]
        [StringLength(9, ErrorMessage = "O tamanho máximo do campo CEP é de 9 caracteres")]
        public string CEP { get; set; }

        [Display(Name = "UF", Prompt = "UF preenchida automáticamente a partir do CEP")]
        [StringLength(100, ErrorMessage = "O tamanho máximo do campo UF é de 100 caracteres")]
        public string UF { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de início", Description = "Data da agenda (início)", Prompt = "Ex.: 01/01/2021")]
        public DateTime DataAgendaInicio { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de término", Description = "Data da agenda (fim)", Prompt = "Ex.: 01/01/2021")]
        public DateTime DataAgendaFim { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Display(Name = "Nome do nutricionista", Prompt = "Ex.: Fulano de tal")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo do campo Nome é 50 caracteres")]
        public string Nome { get; set; }
    }
}

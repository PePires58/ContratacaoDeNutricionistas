#region Histórico de manutenção
/*
Data: 19/06/2020
Programador: Pedro Henrique Pires
Descrição: Implementação inicial.
*/

/*
Data: 19/06/2020
Programador: Pedro Henrique Pires
Descrição: Ajustando descrição dos filtros.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 04/07/2020
* Implementação: Removendo prompt para usar o css do materialize.
*/
#endregion
using System;
using System.ComponentModel.DataAnnotations;

namespace ContratacaoNutricionistasWEB.Models.Contrato
{
    public class FiltroLocalizarNutricionistaVM
    {

        [Display(Name = "Logradouro")]
        [StringLength(100, ErrorMessage = "O tamanho máximo do campo Logradouro é de 100 caracteres")]
        public string Logradouro { get; set; }

        [Display(Name = "Cidade")]
        [StringLength(30, ErrorMessage = "O tamanho máximo do campo Cidade é de 30 caracteres")]
        public string Cidade { get; set; }

        [Display(Name = "Bairro")]
        [StringLength(50, ErrorMessage = "O tamanho máximo do campo Bairro é de 50 caracteres")]
        public string Bairro { get; set; }

        [Display(Name = "CEP")]
        [RegularExpression(@"^\d{5}\-\d{3}$", ErrorMessage = "O campo CEP deve estar no formato 00000-000")]
        [StringLength(9, ErrorMessage = "O tamanho máximo do campo CEP é de 9 caracteres")]
        public string CEP { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "UF", Prompt = "UF")]
        [StringLength(100, ErrorMessage = "O tamanho máximo do campo UF é de 100 caracteres")]
        public string UF { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Data de início", Description = "Data da agenda (início)")]
        public DateTime? DataAgendaInicio { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Data de término", Description = "Data da agenda (fim)")]
        public DateTime? DataAgendaFim { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Display(Name = "Nome do nutricionista")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo do campo Nome é 50 caracteres")]
        public string Nome { get; set; }
    }
}

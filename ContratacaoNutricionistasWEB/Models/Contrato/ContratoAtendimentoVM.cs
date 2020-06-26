using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContratacaoNutricionistasWEB.Models.Contrato
{
    public class ContratoAtendimentoVM : ContratoVM
    {
        [Display(Name = "Receita", Prompt = "Digite a receita para o paciente")]
        [Required(ErrorMessage = "A receita é obrigatoria")]
        [StringLength(255, ErrorMessage = "A receita pode ter no máximo 255 caracteres.")]
        public string MensagemAtendimento { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}

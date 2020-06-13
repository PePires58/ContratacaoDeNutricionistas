#region Histórico de manutenção
/*
Data: 13/06/2020
Programador: Pedro Henrique Pires
Descrição: Implementação inicial.
*/
#endregion

using ContratacaoNutricionistasWEB.Models.Nutricionista;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContratacaoNutricionistasWEB.Models.Contrato
{
    public class ContratoVM
    {
        public ContratoVM()
        {
            Endereco = new EnderecoVM();
        }

        public int IdUsuario { get; set; }

        [Display(Name ="Paciente")]
        public string NomePaciente { get; set; }

        [Display(Name = "Nutricionista")]
        public string NomeNutricionista { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data de início")]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data de término")]
        public DateTime DataFim { get; set; }

        public EnderecoVM Endereco { get; set; }
    }
}

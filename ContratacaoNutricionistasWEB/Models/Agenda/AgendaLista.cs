#region Histórico de manutenções
/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Implementação inicial.
*/
#endregion
using ContratacaoNutricionistas.Domain.Enumerados.Agenda;
using ContratacaoNutricionistasWEB.Models.Nutricionista;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContratacaoNutricionistasWEB.Models.Agenda
{
    /// <summary>
    /// Classe para lista de agenda
    /// </summary>
    public class AgendaLista
    {
        public AgendaLista()
        {
            Endereco = new EnderecoVM();
        }

        [DataType(DataType.Date)]
        [Display(Name = "Data de início")]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de término")]
        public DateTime DataFim { get; set; }

        [Display(Name = "Status da agênda")]
        public StatusAgendaEnum StatusDaAgenda { get; set; }
        public EnderecoVM Endereco { get; set; }
    }
}

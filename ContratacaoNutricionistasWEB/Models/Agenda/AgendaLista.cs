#region Histórico de manutenções
/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Implementação inicial.
*/

/*
Data: 10/06/2020
Programador: Pedro Henrique Pires
Descrição: Ajustando os campos para ter horas.
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

        [DataType(DataType.DateTime)]
        [Display(Name = "Data de início")]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data de término")]
        public DateTime DataFim { get; set; }

        [Display(Name = "Status da agênda")]
        public StatusAgendaEnum StatusDaAgenda { get; set; }
        public EnderecoVM Endereco { get; set; }
    }
}

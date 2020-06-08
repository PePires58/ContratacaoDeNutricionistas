#region Histórico de manutenções
/*
* Programador: Pedro Henrique Pires
* Data: 05/06/2020
* Implementação: Implementação inicial.
*/

/*
Data: 08/06/2020
Programador: Pedro Henrique Pires
Descrição: Ajuste na busca de data
*/
#endregion
using System;
using System.ComponentModel.DataAnnotations;

namespace ContratacaoNutricionistasWEB.Models.Agenda
{
    /// <summary>
    /// Classe para cadastrar uma agenda
    /// </summary>
    public class AgendaCadastroDataHoraVM
    {
        public AgendaCadastroDataHoraVM()
        {
            DataAgendaInicio = DataAgendaFim = Constantes.DateTimeNow();
            HoraInicio = Constantes.DateTimeNow().TimeOfDay;
            HoraFim = Constantes.DateTimeNow().TimeOfDay.Add(new TimeSpan(0, 30, 0));
        }
        [Required(ErrorMessage = "Endereço é obrigatório")]
        public int IdEndereco { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de início", Description = "Data da agenda (início)", Prompt = "Ex.: 01/01/2021")]
        [Required(ErrorMessage = "A data de início agenda é obrigatória")]
        public DateTime? DataAgendaInicio { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de término", Description = "Data da agenda (fim)", Prompt = "Ex.: 01/01/2021")]
        [Required(ErrorMessage = "A data de término da agenda é obrigatória")]
        public DateTime? DataAgendaFim { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Horário de início", Description = "Horário de início", Prompt = "Ex.: 09:00")]
        [Required(ErrorMessage = "O horário de início é obrigatório")]
        public TimeSpan? HoraInicio { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Horário de término", Description = "Horário de término", Prompt = "Ex.: 11:00")]
        [Required(ErrorMessage = "O horário de término é obrigatório")]
        public TimeSpan? HoraFim { get; set; }
    }
}

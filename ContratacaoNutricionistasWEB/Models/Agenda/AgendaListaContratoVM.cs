#region Histórico de manutenção
/*
Data: 08/06/2020
Programador: Pedro Henrique Pires
Descrição: Implementação inicial
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContratacaoNutricionistasWEB.Models.Agenda
{
    public class AgendaListaContratoVM : AgendaLista
    {
        public int IdAgenda { get; set; }
        public int IdUsuario { get; set; }
    }
}

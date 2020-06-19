#region Histórico de manutenções
/*
Data: 10/06/2020
Programador: Pedro Henrique Pires
Descrição: Implementação inicial.
*/

/*
Data: 19/06/2020
Programador: Pedro Henrique Pires
Descrição: Ajustando descrição.
*/

#endregion
using System.ComponentModel;

namespace ContratacaoNutricionistas.Domain.Enumerados.Contrato
{
    public enum StatusContratoEnum
    {
        [Description("Pendente de aceitação do nutricionista"),DefaultValue("PN")]
        PendenteAceitacaoNutricionista,

        [Description("Agendado"),DefaultValue("A")]
        Agendada,

        [Description("Cancelado pelo nutricionista"),DefaultValue("CN")]
        CanceladaNutricionista,

        [Description("Cancelado pelo paciente"),DefaultValue("CP")]
        CanceladaPaciente
    }
}

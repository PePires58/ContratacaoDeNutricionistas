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

/*
Data: 20/06/2020
Programador: Pedro Henrique Pires
Descrição: Ajustando valor default.
*/

/*
Data: 26/06/2020
Programador: Pedro Henrique Pires
Descrição: Consulta realizada.
*/

#endregion
using System.ComponentModel;

namespace ContratacaoNutricionistas.Domain.Enumerados.Contrato
{
    public enum StatusContratoEnum
    {
        [Description("Pendente de aceitação do nutricionista"), DefaultValue("PN")]
        PendenteAceitacaoNutricionista,

        [Description("Agendado"), DefaultValue("AC")]
        Agendada,

        [Description("Cancelado pelo nutricionista"), DefaultValue("CN")]
        CanceladaNutricionista,

        [Description("Cancelado pelo paciente"), DefaultValue("CP")]
        CanceladaPaciente,

        [Description("Consulta realizada"),DefaultValue("CR")]
        ConsultaRealizada,
    }
}

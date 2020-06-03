#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial do enumerado de estados
 */

/*
* Programador: Pedro Henrique Pires
* Data: 03/06/2020
* Implementação: Incluindo valor padrão
*/
#endregion

using System.ComponentModel;

namespace ContratacaoNutricionistas.Domain.Enumerados.Gerais
{
    public enum UnidadeFederacaoEnum
    {
        [Description(""), DefaultValue("")]
        NaoDefinido,
        [Description("Acre"), DefaultValue("AC")]
        AC,
        [Description("Alagoas"), DefaultValue("AL")]
        AL,
        [Description("Amapá"), DefaultValue("AP")]
        AP,
        [Description("Amazonas"), DefaultValue("AM")]
        AM,
        [Description("Bahia"), DefaultValue("BA")]
        BA,
        [Description("Ceará"), DefaultValue("CE")]
        CE,
        [Description("Distrito Federal"), DefaultValue("DF")]
        DF,
        [Description("Espirito Santo"), DefaultValue("ES")]
        ES,
        [Description("Goiás"), DefaultValue("GO")]
        GO,
        [Description("Maranhão"), DefaultValue("MA")]
        MA,
        [Description("Mato Grosso"), DefaultValue("MT")]
        MT,
        [Description("Mato Grosso do Sul"), DefaultValue("MS")]
        MS,
        [Description("Minas Gerais"), DefaultValue("MG")]
        MG,
        [Description("Pará"), DefaultValue("PA")]
        PA,
        [Description("Paraiba"), DefaultValue("PB")]
        PB,
        [Description("Paraná"), DefaultValue("PR")]
        PR,
        [Description("Pernambuco"), DefaultValue("PE")]
        PE,
        [Description("Piauí"), DefaultValue("PI")]
        PI,
        [Description("Rio de Janeiro"), DefaultValue("RJ")]
        RJ,
        [Description("Rio Grande do Norte"), DefaultValue("RN")]
        RN,
        [Description("Rio Grande do Sul"), DefaultValue("RS")]
        RS,
        [Description("Rondônia"), DefaultValue("RO")]
        RO,
        [Description("Roraima"), DefaultValue("RR")]
        RR,
        [Description("Santa Catarina"), DefaultValue("SC")]
        SC,
        [Description("São Paulo"), DefaultValue("SP")]
        SP,
        [Description("Sergipe"), DefaultValue("SE")]
        SE,
        [Description("Tocantis"), DefaultValue("TO")]
        TO
    }
}

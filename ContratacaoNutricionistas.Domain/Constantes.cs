#region Histórico de manutenção
/*
Data: 08/06/2020
Programador: Pedro Henrique Pires
Descrição: Implementação Inicial
*/

/*
Data: 10/06/2020
Programador: Pedro Henrique Pires
Descrição: Ajuste na máscara.
*/
#endregion
using System;

namespace ContratacaoNutricionistas.Domain
{
    /// <summary>
    /// Constantes
    /// </summary>
    public static class Constantes
    {
        /// <summary>
        /// Mascara de data/hora
        /// </summary>
        public const string MascaraDataHora = @"dd/MM/yyyy HH:mm";

        /// <summary>
        /// Mascara de data/hora do sql
        /// </summary>
        public const string MascaraDataHoraSegundoSql = @"yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Mascara de data
        /// </summary>
        public const string MascaraData = @"dd/MM/yyyy";

        public static DateTime DateTimeNow()
        {
            DateTime agora = DateTime.UtcNow;
            TimeZoneInfo horaBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(agora, horaBrasilia);
        }
    }
}

#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de constantes
 */

/*
 * Programador: Pedro Henrique Pires
 * Data: 01/06/2020
 * Implementação: Mensagem de erro para senha.
 */

/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Inclusão de constantes para erro.
*/
/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Inclusão de constantes para o usuário logado.
*/

/*
 * Programador: Pedro Henrique Pires
 * Data: 03/06/2020
 * Implementação: Implementação de constante de registro por página
 */

/*
Data: 08/06/2020
Programador: Pedro Henrique Pires
Descrição: Inclusão para mascaras
*/
/*
Data: 10/06/2020
Programador: Pedro Henrique Pires
Descrição: Ajuste nas máscaras.
*/

/*
Data: 19/06/2020
Programador: Pedro Henrique Pires
Descrição: Constante para tela de filtro.
*/

#endregion

using System;

namespace ContratacaoNutricionistasWEB
{
    public static class Constantes
    {
        /// <summary>
        /// Constante para acesso da ViewData
        /// </summary>
        public const string ViewDataMensagemRetorno = "MensagemRetorno";

        /// <summary>
        /// Constante para acesso da ViewData
        /// </summary>
        public const string ViewDataMensagemErro = "MensagemErro";

        /// <summary>
        /// Constantes para acesso da ViewData
        /// </summary>
        public const string ViewDataUnidadesFeracao = "UnidadesFederacao";

        /// <summary>
        /// Nome da constante de viewdata para nome do controller de filtro
        /// </summary>
        public const string ViewDataControllerFiltro = "ControllerFiltro";

        /// <summary>
        /// Nome da tela de filtro
        /// </summary>
        public const string ViewDataActionFiltro = "ActionFiltro";

        /// <summary>
        /// Mensagem para senha de alteração de dados for inválida
        /// </summary>
        public const string MensagemErroSenhaInvalidaAlteracaoDados = "A senha para alteração dos dados é inválida";

        /// <summary>
        /// Mensagem para caso não encontre a senha no banco
        /// </summary>
        public const string MensagemErroSenhaNaoLocalizada = "Erro ao localizar a senha, favor entrar em contato conosco!";

        /// <summary>
        /// Nutricionista logado
        /// </summary>
        public const string NutricionistaLogado = "Nutricionista";

        /// <summary>
        /// Paciente logado
        /// </summary>
        public const string PacienteLogado = "Paciente";

        /// <summary>
        /// Nome do tipo da claim para usuário logado
        /// </summary>
        public const string IDUsuarioLogado = "ID";

        /// <summary>
        /// Quantidade de registros por página
        /// </summary>
        public const int QuantidadeRegistrosPorPagina = 10;

        /// <summary>
        /// Mascara de data/hora
        /// </summary>
        public const string MascaraDataHora = @"dd/MM/yyyy HH:mm";

        /// <summary>
        /// Mascara de data
        /// </summary>
        public const string MascaraData = @"dd/MM/yyyy";

        /// <summary>
        /// Máscara de hora/minuto
        /// </summary>
        public const string MascaraHoraMinuto = "HH:mm";


        public static DateTime DateTimeNow()
        {
            DateTime agora = DateTime.UtcNow;
            TimeZoneInfo horaBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(agora, horaBrasilia);
        }
    }
}

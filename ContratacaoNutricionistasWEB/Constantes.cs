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
#endregion

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
    }
}

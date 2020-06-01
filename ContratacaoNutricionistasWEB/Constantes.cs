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
        /// Constante para lista do tipo de pessoa
        /// </summary>
        public const string ViewDataListaTipoPessoa = "ListaTipoPessoa";

        /// <summary>
        /// Mensagem para senha de alteração de dados for inválida
        /// </summary>
        public const string MensagemErroSenhaInvalidaAlteracaoDados = "A senha para alteração dos dados é inválida";

        /// <summary>
        /// Mensagem para caso não encontre a senha no banco
        /// </summary>
        public const string MensagemErroSenhaNaoLocalizada = "Erro ao localizar a senha, favor entrar em contato conosco!";
    }
}

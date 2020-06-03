#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 03/06/2020
 * Implementação: Implementação Inicial da classe de endereço
 */
#endregion
using System.ComponentModel.DataAnnotations;

namespace ContratacaoNutricionistasWEB.Models.Nutricionista
{
    /// <summary>
    /// Endereço para alteração
    /// </summary>
    public class EnderecoAlteracaoVM : EnderecoVM
    {
        /// <summary>
        /// ID do endereço
        /// </summary>
        [Required(ErrorMessage = "O campo ID é obrigatório")]
        public int ID { get; set; }
    }
}

#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial do controlador de nutricionista
 */

   /*
 * Programador: Pedro Henrique Pires
 * Data: 01/06/2020
 * Implementação: CRM -> CRN.
 */
#endregion
using System.ComponentModel.DataAnnotations;

namespace ContratacaoNutricionistasWEB.Models.Nutricionista
{
    /// <summary>
    /// Classe de alteração de nutricionista em tela
    /// </summary>
    public class NutricionistaAlteracaoVM : Usuario.UsuarioAlteracaoVM
    {
        #region Construtores
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public NutricionistaAlteracaoVM()
        {
            TipoUsuario = ContratacaoNutricionistas.Domain.Enumerados.Usuario.TipoUsuarioEnum.Nutricionista;
        }
        #endregion

        /// <summary>
        /// CRN do nutricionista
        /// </summary>
        [Display(Name = "CRN", Prompt = "Ex.: 12345")]
        [Required(ErrorMessage = "O número do CRN é obrigatório")]
        [MaxLength(5, ErrorMessage = "O tamanho máximo do campo CRN é 5 caracteres")]
        public string CRN { get; set; }
    }
}

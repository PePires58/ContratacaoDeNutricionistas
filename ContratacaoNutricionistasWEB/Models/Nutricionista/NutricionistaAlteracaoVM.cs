#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial do controlador de nutricionista
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
        /// CRM do nutricionista
        /// </summary>
        [Display(Name = "CRM", Prompt = "Ex.: 123123123")]
        [Required(ErrorMessage = "O número do CRM é obrigatório")]
        [MaxLength(15, ErrorMessage = "O tamanho máximo do campo CRM é 15 caracteres")]
        public string CRM { get; set; }
    }
}

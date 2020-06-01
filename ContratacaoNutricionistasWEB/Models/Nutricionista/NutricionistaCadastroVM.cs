#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de cadastro de nutricionista para a tela.
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
    public class NutricionistaCadastroVM : Usuario.UsuarioCadastroVM
    {
        #region Construtor
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public NutricionistaCadastroVM()
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

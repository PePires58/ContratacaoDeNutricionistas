#region MyRegion
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de cadastro de nutricionista para a tela.
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
        /// CRM do nutricionista
        /// </summary>
        [Display(Name = "CRM", Prompt = "Ex.: 123123123")]
        [Required(ErrorMessage = "O número do CRM é obrigatório")]
        [MaxLength(15, ErrorMessage = "O tamanho máximo do campo CRM é 15 caracteres")]
        public string CRM { get; set; }
    }
}

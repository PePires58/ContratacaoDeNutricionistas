#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de Paciente
 */
#endregion

namespace ContratacaoNutricionistasWEB.Models.Paciente
{
    /// <summary>
    /// Classe para alteração de dados do paciente
    /// </summary>
    public class PacienteAlteracaoVM : Usuario.UsuarioAlteracaoVM
    {
        #region Construtor
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public PacienteAlteracaoVM()
        {
            TipoUsuario = ContratacaoNutricionistas.Domain.Enumerados.Usuario.TipoUsuarioEnum.Paciente;
        }
        #endregion
        
    }
}

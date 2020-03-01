using ContratacaoNutricionistasWEB.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContratacaoNutricionistasWEB.Models.Paciente
{
    /// <summary>
    /// Classe para cadastro de paciente
    /// </summary>
    public class PacienteCadastroVM : UsuarioCadastroVM
    {
        #region Construtor
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public PacienteCadastroVM()
        {
            TipoUsuario = ContratacaoNutricionistas.Domain.Enumerados.Usuario.TipoUsuarioEnum.Paciente;
        }
        #endregion
    }
}

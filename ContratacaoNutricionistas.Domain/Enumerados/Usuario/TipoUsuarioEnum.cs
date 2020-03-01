using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ContratacaoNutricionistas.Domain.Enumerados.Usuario
{
    /// <summary>
    /// Tipo de usuário
    /// </summary>
    public enum TipoUsuarioEnum
    {
        /// <summary>
        /// Não definido
        /// </summary>
        [Description(""), DefaultValue("")]
        NaoDefinido = -1,

        /// <summary>
        /// Paciente
        /// </summary>
        [Description("Paciente"),DefaultValue("0")]
        Paciente = 0,

        /// <summary>
        /// Nutricionista
        /// </summary>
        [Description("Nutricionista"),DefaultValue("1")]
        Nutricionista = 1
    }
}

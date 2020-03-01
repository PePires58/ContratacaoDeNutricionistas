using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ContratacaoNutricionistas.Domain.Enumerados.Usuario
{
    /// <summary>
    /// Enumerado para tipos de pessoa
    /// </summary>
    public enum TipoPessoaEnum
    {
        /// <summary>
        /// Não definido
        /// </summary>
        [Description(""),DefaultValue("")]
        NaoDefinido = -1,

        /// <summary>
        /// Pessoa física
        /// </summary>
        [Description("Física"), DefaultValue("0")]
        Fisica = 0,

        /// <summary>
        /// Pessoa jurídica
        /// </summary>
        [Description("Jurídica"),DefaultValue("1")]
        Juridica = 1
    }
}

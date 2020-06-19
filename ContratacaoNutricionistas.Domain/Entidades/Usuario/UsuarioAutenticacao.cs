using ContratacaoNutricionistas.Domain.Enumerados.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContratacaoNutricionistas.Domain.Entidades.Usuario
{
    public class UsuarioAutenticacao
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        /// Tipo de usuário
        /// </summary>
        public TipoUsuarioEnum TipoUsuario { get; set; }
    }
}

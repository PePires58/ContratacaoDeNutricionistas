#region Histórico de manutenção
/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Implementação inicial.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Implementação do método de consulta de usuário para autenticação.
*/

#endregion
using ContratacaoNutricionistas.Domain.Entidades.Usuario;
using ContratacaoNutricionistas.Domain.Interfaces.Paciente;
using ContratacaoNutricionistas.Domain.Interfaces.Usuario;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ContratacaoNutricionistas.Domain.Servicos.Usuario
{
    /// <summary>
    /// Serviços para usuário
    /// </summary>
    public class ServiceUsuario : IServiceUsuario
    {
        #region Constantes
        /// <summary>
        /// Tamanho máximo para o campo login
        /// </summary>
        private const int TamanhoMaximoLogin = 20;
        #endregion

        #region Propriedades
        /// <summary>
        /// Interface que faz os comandos com o banco de dados para nutricionista
        /// </summary>
        private readonly IUsuarioRepository _UsuarioRepository;
        #endregion

        #region Construtor
        /// <summary>
        /// Construtor da classe de serviço
        /// </summary>
        /// <param name="pUsuarioRepository">Interface que faz os comandos com o banco de dados para nutricionista</param>
        public ServiceUsuario(IUsuarioRepository pUsuarioRepository)
        {
            _UsuarioRepository = pUsuarioRepository;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Verifica se o login já existe
        /// </summary>
        /// <param name="pLogin">Login já existe</param>
        /// <returns>Returna se o usuário existe ou não</returns>
        public bool LoginExiste(string pLogin)
        {
            if (string.IsNullOrEmpty(pLogin))
                throw new ArgumentException("O login é obrigatório.");
            else if (pLogin.Length > TamanhoMaximoLogin)
                throw new ArgumentException(string.Format("O tamanho máximo do campo Login é de {0} caracteres.", TamanhoMaximoLogin));

            return _UsuarioRepository.LoginExiste(pLogin);
        }

        public ClaimsPrincipal RetornaAutenticacaoUsuario(string pLogin, string pSenha)
        {
            UsuarioAutenticacao usuarioAutenticacao = ConsultarUsuarioAutenticacao(pLogin, pSenha);

            List<Claim> listaCliams = new List<Claim>();

            switch (usuarioAutenticacao.TipoUsuario)
            {
                case Enumerados.Usuario.TipoUsuarioEnum.NaoDefinido:
                    throw new Exception("Tipo de usuário não definido.");
                case Enumerados.Usuario.TipoUsuarioEnum.Paciente:
                case Enumerados.Usuario.TipoUsuarioEnum.Nutricionista:
                    listaCliams.Add(new Claim(usuarioAutenticacao.TipoUsuario.ToString(), ClaimTypes.Name, usuarioAutenticacao.Login));
                    break;
                default:
                    throw new Exception("Tipo de usuário não implementado");
            }

            ClaimsIdentity identity = new ClaimsIdentity(listaCliams, "Usuario Identity");

            return new ClaimsPrincipal(new[] { identity }); ;
        }

        #endregion

        #region Métodos privados
        /// <summary>
        /// Consultar o usuário para realizar a autenticação
        /// </summary>
        /// <param name="pLogin">Login</param>
        /// <param name="pSenha">Senha</param>
        /// <returns></returns>
        private UsuarioAutenticacao ConsultarUsuarioAutenticacao(string pLogin, string pSenha)
        {
            if (string.IsNullOrEmpty(pLogin))
                throw new ArgumentException("O login é obrigatório");
            if (string.IsNullOrEmpty(pSenha))
                throw new ArgumentException("A senha é obrigatória");

            UsuarioAutenticacao usuario = _UsuarioRepository.ConsultarUsuarioAutenticacao(pLogin, pSenha);
            if (usuario == null)
                throw new Exception("Usuário ou senha inválido");
            return usuario;
        }
        #endregion
    }
}

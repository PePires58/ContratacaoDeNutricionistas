#region Histórico de manutenção
/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Implementação Inicial.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Implementação do método de consulta de usuário para autenticação.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Incluindo ID do usuário.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Herdando do repositório base.
*/
#endregion
using ContratacaoNutricionistas.Domain.Entidades.Usuario;
using ContratacaoNutricionistas.Domain.Enumerados.Usuario;
using ContratacaoNutricionistas.Domain.Interfaces.Paciente;
using ContratacaoNutricionistas.Domain.Repository.Repository;
using DataBaseHelper.Interfaces;
using System;
using System.Data;
using System.Text;

namespace ContratacaoNutricionistas.Domain.Repository.Usuario
{
    public class UsuarioRepository : RepositoryBase, IUsuarioRepository
    {
        #region Construtor
        /// <summary>
        /// Unidade de trabalho
        /// </summary>
        /// <param name="pIUnitOfWork"></param>
        public UsuarioRepository(IUnitOfWork pIUnitOfWork):base(pIUnitOfWork)
        {
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Login já existe
        /// </summary>
        /// <param name="pLogin">Login</param>
        /// <param name="pCPF">CPF</param>
        /// <param name="pTipoUsuario">Tipo de usuário</param>
        /// <returns>Login já existe ou não</returns>
        public bool LoginExiste(string pLogin, string pCPF, string pTipoUsuario)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"DECLARE @LOGIN VARCHAR(20),");
            stringBuilder.AppendLine($"@CPF VARCHAR(14),");
            stringBuilder.AppendLine($"@TP_USUARIO CHAR(1)");
            stringBuilder.AppendLine($"SET @LOGIN = '{pLogin}'");
            stringBuilder.AppendLine($"SET @CPF = '{pCPF}'");
            stringBuilder.AppendLine($"SET @TP_USUARIO = '{pTipoUsuario}'");
            stringBuilder.AppendLine($"SELECT TOP 1 1 FROM USUARIO_TB TB WITH(NOLOCK) WHERE TB.LOGIN = @LOGIN OR (TB.CPF = @CPF AND TB.TP_USUARIO = @TP_USUARIO)");

            DataSet ds = _UnitOfWork.Consulta(stringBuilder.ToString());

            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
        }

        /// <summary>
        /// Retorna um usuário de autenticação ou null
        /// </summary>
        /// <param name="pLogin">Login</param>
        /// <param name="pSenha">Senha</param>
        /// <returns>Usuário ou NULL</returns>
        public UsuarioAutenticacao ConsultarUsuarioAutenticacao(string pLogin, string pSenha)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE");
            stringBuilder.AppendLine("	@LOGIN VARCHAR(20),");
            stringBuilder.AppendLine("	@SENHA VARCHAR(8)");
            stringBuilder.AppendLine($"SET @LOGIN = '{pLogin}'");
            stringBuilder.AppendLine($"SET @SENHA = '{pSenha}'");
            stringBuilder.AppendLine("SELECT");
            stringBuilder.AppendLine("  TB.ID_USUARIO,");
            stringBuilder.AppendLine("  TB.LOGIN,");
            stringBuilder.AppendLine("	TB.SENHA,");
            stringBuilder.AppendLine("	TB.TP_USUARIO");
            stringBuilder.AppendLine("FROM USUARIO_TB TB WITH(NOLOCK)");
            stringBuilder.AppendLine("WHERE TB.LOGIN = @LOGIN AND TB.SENHA = @SENHA");

            DataSet ds = _UnitOfWork.Consulta(stringBuilder.ToString());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                UsuarioAutenticacao usuarioAutenticacao = usuarioAutenticacao = new UsuarioAutenticacao();
                if (ds.Tables[0].Rows[0]["ID_USUARIO"] != DBNull.Value)
                    usuarioAutenticacao.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID_USUARIO"]);
                if (ds.Tables[0].Rows[0]["LOGIN"] != DBNull.Value)
                    usuarioAutenticacao.Login = ds.Tables[0].Rows[0]["LOGIN"].ToString();
                if (ds.Tables[0].Rows[0]["SENHA"] != DBNull.Value)
                    usuarioAutenticacao.Senha = ds.Tables[0].Rows[0]["SENHA"].ToString();
                if (ds.Tables[0].Rows[0]["TP_USUARIO"] != DBNull.Value)
                    usuarioAutenticacao.TipoUsuario = (TipoUsuarioEnum)Convert.ToInt32(ds.Tables[0].Rows[0]["TP_USUARIO"]);

                return usuarioAutenticacao;
            }
            return null;
        }
        #endregion
    }
}

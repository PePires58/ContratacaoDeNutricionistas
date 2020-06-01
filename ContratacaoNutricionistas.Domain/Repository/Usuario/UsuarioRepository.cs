#region Histórico de manutenção
 /*
 * Programador: Pedro Henrique Pires
 * Data: 01/06/2020
 * Implementação: Implementação Inicial.
 */
#endregion
using ContratacaoNutricionistas.Domain.Interfaces.Paciente;
using DataBaseHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ContratacaoNutricionistas.Domain.Repository.Usuario
{
    public class UsuarioRepository : IUsuarioRepository
    {
        #region Propriedades
        /// <summary>
        /// Unidade de conexão e execução com banco de dados
        /// </summary>
        private readonly IUnitOfWork _UnitOfWork;
        #endregion

        #region Construtor
        /// <summary>
        /// Unidade de trabalho
        /// </summary>
        /// <param name="pIUnitOfWork"></param>
        public UsuarioRepository(IUnitOfWork pIUnitOfWork)
        {
            _UnitOfWork = pIUnitOfWork;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Login já existe
        /// </summary>
        /// <param name="pLogin">Login</param>
        /// <returns>Login já existe ou não</returns>
        public bool LoginExiste(string pLogin)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"DECLARE @LOGIN VARCHAR(20)");
            stringBuilder.AppendLine($"SET @LOGIN = '{pLogin}'");
            stringBuilder.AppendLine($"SELECT TOP 1 1 FROM USUARIO_TB WITH(NOLOCK) WHERE LOGIN = @LOGIN");

            DataSet ds = _UnitOfWork.Consulta(stringBuilder.ToString());

            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
        }
        #endregion
    }
}

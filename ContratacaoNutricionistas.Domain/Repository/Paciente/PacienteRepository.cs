#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de paciente repository
 */
#endregion

using ContratacaoNutricionistas.Domain.Interfaces.Paciente;
using DataBaseHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ContratacaoNutricionistas.Domain.Repository.Paciente
{
    /// <summary>
    /// Classe que faz as conexões com o banco quando o objeto é um paciente
    /// </summary>
    public class PacienteRepository : IPacienteRepository
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
        public PacienteRepository(IUnitOfWork pIUnitOfWork)
        {
            _UnitOfWork = pIUnitOfWork;
        }
        #endregion

        /// <summary>
        /// Cadastra um paciente
        /// </summary>
        /// <param name="pPacienteCadastro">Paciente a ser cadastrado</param>
        public void CadastrarPaciente(Entidades.Paciente.PacienteCadastro pPacienteCadastro)
        {
            /*Executa o comando passado por parâmetro.*/
            _UnitOfWork.Executar(
                _UnitOfWork.MontaInsertPorAttributo(pPacienteCadastro).ToString() /*Monta o comando SQL a partir dos atributos da classe*/
             );
        }

        public bool LoginExiste(string pLogin)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"DECLARE @LOGIN VARCHAR(25)");
            stringBuilder.AppendLine($"SET @LOGIN = {pLogin}");
            stringBuilder.AppendLine($"SELECT TOP 1 1 FROM PACIENTE_TB WITH(NOLOCK) WHERE LOGIN = @LOGIN");
    
            DataSet ds = _UnitOfWork.Consulta(stringBuilder.ToString());

            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
        }
    }
}

#region Histórico de manutenção
/*
Data: 10/06/2020
Programador: Pedro Henrique Pires
Descrição: Implementação inicial.
*/
#endregion
using ContratacaoNutricionistas.Domain.Interfaces.Contrato;
using ContratacaoNutricionistas.Domain.Repository.Repository;
using DataBaseHelper.Interfaces;
using System;
using System.Data;
using System.Text;

namespace ContratacaoNutricionistas.Domain.Repository.Contrato
{
    public class ContratoRepository : RepositoryBase, IContratoRepository
    {
        #region Construtor
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="UnitOfWork">Unit of work</param>
        public ContratoRepository(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }
        #endregion

        /// <summary>
        /// Contrata um nutricionista
        /// </summary>
        /// <param name="pContrato">Contrato a ser cadastrado</param>
        public void ContratarNutricionista(Entidades.Contrato.Contrato pContrato)
        {
            _UnitOfWork.Executar(_UnitOfWork.MontaInsertPorAttributo(pContrato).ToString());
        }

        /// <summary>
        /// Verifica se um paciente já tem um contrato no periodo informado
        /// </summary>
        /// <param name="pDataInicio">Data de início</param>
        /// <param name="pDataTermino">Data de término</param>
        /// <param name="pIdUsuario">Id so usuário</param>
        /// <returns></returns>
        public bool VerificarContratoExistenteNaData(DateTime pDataInicio, DateTime pDataTermino, int pIdUsuario)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @DT_INICIO DATETIME,");
            stringBuilder.AppendLine("	@DT_FIM DATETIME,");
            stringBuilder.AppendLine("    @ID_USUARIO INT");
            stringBuilder.AppendLine($"SET @DT_INICIO = '{pDataInicio.ToString(Constantes.MascaraDataHoraSegundoSql)}'");
            stringBuilder.AppendLine($"SET @DT_FIM = '{pDataTermino.ToString(Constantes.MascaraDataHoraSegundoSql)}'");
            stringBuilder.AppendLine($"SET @ID_USUARIO = {pIdUsuario}");
            stringBuilder.AppendLine("SELECT TOP 1 1");
            stringBuilder.AppendLine("FROM CONTRATO_TB TB WITH(NOLOCK)");
            stringBuilder.AppendLine("    WHERE TB.ID_USUARIO = @ID_USUARIO");
            stringBuilder.AppendLine("    AND(TB.DT_INICIO BETWEEN @DT_INICIO AND @DT_FIM");
            stringBuilder.AppendLine("        OR TB.DT_FIM BETWEEN @DT_INICIO AND @DT_FIM)");

            DataSet ds = _UnitOfWork.Consulta(stringBuilder.ToString());

            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
        }
    }
}

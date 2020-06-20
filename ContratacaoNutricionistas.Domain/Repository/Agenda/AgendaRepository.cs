#region Histórico de manutenções
/*
 Data: 06/05/2020
 Programador: Pedro Henrique Pires
 Descrição: Implementação Inicial da classe.
 */

/*
Data: 08/06/2020
Programador: Pedro Henrique Pires
Descrição: Inclusão de campos.
*/

/*
Data: 10/06/2020
Programador: Pedro Henrique Pires
Descrição: Ajustando nome da classe.
*/
/*
Data: 13/06/2020
Programador: Pedro Henrique Pires
Descrição: Desativar a agenda quando excluir o endereço.
*/

/*
Data: 19/06/2020
Programador: Pedro Henrique Pires
Descrição: Método para inativar as agendas.
*/

/*
Data: 20/06/2020
Programador: Pedro Henrique Pires
Descrição: Abrindo transação para não dar erro ao executar comandos simultaneos em aparelhos diferentes.
*/

#endregion
using ContratacaoNutricionistas.Domain.Interfaces.Agenda;
using ContratacaoNutricionistas.Domain.Repository.Repository;
using DataBaseHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ContratacaoNutricionistas.Domain.Repository.Agenda
{
    /// <summary>
    /// Repositório de agência
    /// </summary>
    public class AgendaRepository : RepositoryBase, IAgendaRepository
    {
        #region Construtores
        /// <summary>
        /// Repositório de agenda
        /// </summary>
        /// <param name="UnitOfWork"></param>
        public AgendaRepository(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Verifica se já existe agenda cadastrada
        /// </summary>
        /// <param name="pDataHoraInicio">Data e hora de início</param>
        /// <param name="pDataHoraFim">Data e hora de término</param>
        /// <param name="pIdEndereco">Id do endereço</param>
        /// <param name="pIdUsuario">Id do usuário</param>
        /// <returns></returns>
        public bool AgendaJaCadastrada(DateTime pDataHoraInicio, DateTime pDataHoraFim, int pIdEndereco, int pIdUsuario)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @ID_USUARIO INT,");
            stringBuilder.AppendLine("	@ID_ENDERECO INT,");
            stringBuilder.AppendLine("  @DT_INICIO DATETIME,");
            stringBuilder.AppendLine("	@DT_FIM DATETIME");
            stringBuilder.AppendLine($"SET @ID_USUARIO = {pIdUsuario}");
            stringBuilder.AppendLine($"SET @DT_INICIO = '{(pDataHoraInicio == DateTime.MinValue ? "" : pDataHoraInicio.ToString(Constantes.MascaraDataHoraSegundoSql))}'");
            stringBuilder.AppendLine($"SET @DT_FIM = '{(pDataHoraFim == DateTime.MinValue ? "" : pDataHoraFim.ToString(Constantes.MascaraDataHoraSegundoSql))}'");
            stringBuilder.AppendLine("SELECT TOP 1 1 FROM AGENDA_TB AG WITH(NOLOCK)");
            stringBuilder.AppendLine("WHERE AG.ID_USUARIO = @ID_USUARIO");
            stringBuilder.AppendLine("    AND(AG.DT_INICIO BETWEEN @DT_INICIO AND @DT_FIM");
            stringBuilder.AppendLine("        OR AG.DT_FIM BETWEEN @DT_INICIO AND @DT_FIM)");
            stringBuilder.AppendLine("    AND AGENDA_STATUS = 'A' /*ATIVA*/");

            DataSet ds = _UnitOfWork.Consulta(stringBuilder.ToString());

            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
        }

        public List<Entidades.Agenda.Agenda> AgendasCadastradas(DateTime pDataInicio, DateTime pDataFim, int pIdUsuario)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @DT_INICIO DATETIME,");
            stringBuilder.AppendLine("	@DT_FIM DATETIME,");
            stringBuilder.AppendLine("    @ID_USUARIO INT");
            stringBuilder.AppendLine($"SET @ID_USUARIO = {(pIdUsuario == 0 ? "NULL" : pIdUsuario.ToString())}");
            stringBuilder.AppendLine($"SET @DT_INICIO = {(pDataInicio == DateTime.MinValue ? "NULL" : "'" + pDataInicio.ToString(Constantes.MascaraDataHoraSegundoSql) + "'")}");
            stringBuilder.AppendLine($"SET @DT_FIM = {(pDataFim == DateTime.MinValue ? "NULL" : "'" + pDataFim.ToString(Constantes.MascaraDataHoraSegundoSql) + "'")}");
            stringBuilder.AppendLine("SELECT");
            stringBuilder.AppendLine("  AG.ID_AGENDA,");
            stringBuilder.AppendLine("  AG.ID_USUARIO,");
            stringBuilder.AppendLine("  AG.ID_ENDERECO,");
            stringBuilder.AppendLine("  AG.DT_INICIO,");
            stringBuilder.AppendLine("  AG.DT_FIM");
            stringBuilder.AppendLine("FROM AGENDA_TB AG WITH(NOLOCK)");
            stringBuilder.AppendLine("    INNER JOIN ENDERECO_TB EN WITH(NOLOCK) ON AG.ID_ENDERECO = EN.ID_ENDERECO");
            stringBuilder.AppendLine("WHERE AG.AGENDA_STATUS = 'A'");
            stringBuilder.AppendLine("    AND(AG.DT_INICIO BETWEEN ISNULL(@DT_INICIO, AG.DT_INICIO) AND ISNULL(@DT_FIM, AG.DT_FIM) OR");
            stringBuilder.AppendLine("        AG.DT_FIM BETWEEN ISNULL(@DT_INICIO, AG.DT_INICIO) AND ISNULL(@DT_FIM, AG.DT_FIM))");
            stringBuilder.AppendLine("    AND AG.ID_USUARIO = ISNULL(@ID_USUARIO,AG.ID_USUARIO)");

            DataSet ds = _UnitOfWork.Consulta(stringBuilder.ToString());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<Entidades.Agenda.Agenda> agendas = new List<Entidades.Agenda.Agenda>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int idEndereco = 0, idAgenda = 0, idUsuario = 0;
                    DateTime datainicio = DateTime.MinValue,
                        dataFim = DateTime.MinValue;

                    if (ds.Tables[0].Rows[i]["ID_ENDERECO"] != DBNull.Value)
                        idEndereco = Convert.ToInt32(ds.Tables[0].Rows[i]["ID_ENDERECO"]);
                    if (ds.Tables[0].Rows[i]["ID_USUARIO"] != DBNull.Value)
                        idUsuario = Convert.ToInt32(ds.Tables[0].Rows[i]["ID_USUARIO"]);
                    if (ds.Tables[0].Rows[i]["ID_AGENDA"] != DBNull.Value)
                        idAgenda = Convert.ToInt32(ds.Tables[0].Rows[i]["ID_AGENDA"]);
                    if (ds.Tables[0].Rows[i]["DT_INICIO"] != DBNull.Value)
                        datainicio = Convert.ToDateTime(ds.Tables[0].Rows[i]["DT_INICIO"]);
                    if (ds.Tables[0].Rows[i]["DT_FIM"] != DBNull.Value)
                        dataFim = Convert.ToDateTime(ds.Tables[0].Rows[i]["DT_FIM"]);

                    agendas.Add(new Entidades.Agenda.Agenda(
                        idUsuario,
                        idEndereco,
                        datainicio,
                        dataFim
                        )
                    { StatusAgenda = Enumerados.Agenda.StatusAgendaEnum.Ativa, IdAgenda = idAgenda });
                }
                return agendas;
            }

            return new List<Entidades.Agenda.Agenda>();
        }

        /// <summary>
        /// Cadastra a agencia no banco
        /// </summary>
        /// <param name="agendaCadastro">Agencia para ser cadastrada</param>
        public void CadastrarAgenda(Entidades.Agenda.Agenda agendaCadastro)
        {
            _UnitOfWork.Executar(
                _UnitOfWork.MontaInsertPorAttributo(agendaCadastro).ToString()
                );
        }

        /// <summary>
        /// Desativa a agenda por endereço excluído
        /// </summary>
        /// <param name="pIdUsuario">Id do usuário</param>
        /// <param name="pIdEndereco">Id do endereço</param>
        public void DesativarAgendaPorEnderecoExcluido(int pIdUsuario, int pIdEndereco)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @ID_USUARIO INT,");
            stringBuilder.AppendLine("	@ID_ENDERECO INT");
            stringBuilder.AppendLine($"SET @ID_USUARIO = {pIdUsuario}");
            stringBuilder.AppendLine($"SET @ID_ENDERECO = {pIdEndereco}");
            stringBuilder.AppendLine("UPDATE AGENDA_TB");
            stringBuilder.AppendLine("SET AGENDA_STATUS = 'EE'");
            stringBuilder.AppendLine("WHERE ID_USUARIO = @ID_USUARIO");
            stringBuilder.AppendLine("    AND ID_ENDERECO = @ID_ENDERECO");

            _UnitOfWork.Executar(stringBuilder.ToString());
        }

        /// <summary>
        /// Inativa as agendas
        /// </summary>
        /// <param name="pDataAgora">Agora</param>
        public void InvativarAgendas(DateTime pDataAgora)
        {
            _UnitOfWork.BeginTransaction();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @DT_AGORA DATETIME");
            stringBuilder.AppendLine($"SET @DT_AGORA = '{pDataAgora.ToString(Constantes.MascaraDataHoraSegundoSql)}'");
            stringBuilder.AppendLine("UPDATE AGENDA_TB");
            stringBuilder.AppendLine("    SET AGENDA_STATUS = 'D'");
            stringBuilder.AppendLine("WHERE DT_INICIO<@DT_AGORA");
            stringBuilder.AppendLine("    AND AGENDA_STATUS <> 'D'");
            _UnitOfWork.Executar(stringBuilder.ToString());
            _UnitOfWork.Commit();
        }
        #endregion

    }
}

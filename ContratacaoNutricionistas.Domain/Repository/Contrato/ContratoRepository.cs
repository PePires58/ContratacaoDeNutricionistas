#region Histórico de manutenção
/*
Data: 10/06/2020
Programador: Pedro Henrique Pires
Descrição: Implementação inicial.
*/

/*
Data: 13/06/2020
Programador: Pedro Henrique Pires
Descrição: Listando os contratos disponíveis.
*/

/*
Data: 15/06/2020
Programador: Pedro Henrique Pires
Descrição: Incluindo método de buscar contrato e alterar status.
*/

/*
Data: 16/06/2020
Programador: Pedro Henrique Pires
Descrição: Ajustando comando sql.
*/

/*
Data: 19/06/2020
Programador: Pedro Henrique Pires
Descrição: Verificando disponibilidade de contratação.
*/

/*
Data: 20/06/2020
Programador: Pedro Henrique Pires
Descrição: Ajustando filtro de estado.
*/


/*
Data: 26/06/2020
Programador: Pedro Henrique Pires
Descrição: Método para realizar o atendimento.
*/

/*
Data: 27/06/2020
Programador: Pedro Henrique Pires
Descrição: Ajuste para não considerar contratos cancelados como "ativos".
*/

#endregion
using ContratacaoNutricionistas.Domain.Entidades.Contrato;
using ContratacaoNutricionistas.Domain.Enumerados.Contrato;
using ContratacaoNutricionistas.Domain.Enumerados.Gerais;
using ContratacaoNutricionistas.Domain.Interfaces.Contrato;
using ContratacaoNutricionistas.Domain.Repository.Repository;
using DataBaseHelper.Interfaces;
using ModulosHelper.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            if (pContrato == null)
                throw new ArgumentException("O contrato é obrigatório.");
            if (pContrato.UF == UnidadeFederacaoEnum.NaoDefinido)
                throw new ArgumentException("Unidade de federação é obrigatório.");
            new Entidades.Nutricionista.Endereco(pContrato.IdUsuario,
                pContrato.Logradouro,
                pContrato.Bairro,
                pContrato.Cidade,
                pContrato.CEP,
                pContrato.UF);

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
            stringBuilder.AppendLine("    AND STATUS NOT IN ('CP','CN')");


            DataSet ds = _UnitOfWork.Consulta(stringBuilder.ToString());

            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
        }

        /// <summary>
        /// <summary>
        /// Lista os constratos de algum usuário
        /// </summary>
        /// <param name="pIndiceInicial">Indice inicial</param>
        /// <param name="pRua">Rua</param>
        /// <param name="pCidade">Cidade</param>
        /// <param name="pBairro">Bairro</param>
        /// <param name="pCEP">CEP</param>
        /// <param name="pUF">UF</param>
        /// <param name="pDataInicio">Data de início</param>
        /// <param name="pDataFim">Data fim</param>
        /// <param name="pIdUsuario">ID do usuário</param>
        /// <returns>Lista de contratos</returns>
        public List<Entidades.Contrato.Contrato> ListaContratos(string pRua, string pCidade, string pBairro, string pCEP, string pUF, DateTime pDataInicio, DateTime pDataFim, int pIdUsuario)
        {
            #region Query
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @RUA VARCHAR(100),");
            stringBuilder.AppendLine("	@CIDADE VARCHAR(30),");
            stringBuilder.AppendLine("	@BAIRRO VARCHAR(50),");
            stringBuilder.AppendLine("	@CEP VARCHAR(9),");
            stringBuilder.AppendLine("	@ESTADO VARCHAR(2),");
            stringBuilder.AppendLine("	@DT_INICIO DATETIME,");
            stringBuilder.AppendLine("  @DT_FIM DATETIME,");
            stringBuilder.AppendLine("	@ID_USUARIO INT");
            stringBuilder.AppendLine($"SET @ID_USUARIO = {pIdUsuario}");
            stringBuilder.AppendLine($"SET @RUA = '{pRua}'");
            stringBuilder.AppendLine($"SET @BAIRRO = '{pBairro}'");
            stringBuilder.AppendLine($"SET @CIDADE = '{pCidade}'");
            stringBuilder.AppendLine($"SET @CEP = {(string.IsNullOrEmpty(pCEP) ? "NULL" : "'" + pCEP + "'")}");
            stringBuilder.AppendLine($"SET @ESTADO = {(string.IsNullOrEmpty(pUF) ? "NULL" : "'" + Enum.GetValues(typeof(UnidadeFederacaoEnum)).Cast<UnidadeFederacaoEnum>().FirstOrDefault(c => c.GetDescription().Equals(pUF)).GetDefaultValue() + "'")}");
            stringBuilder.AppendLine($"SET @DT_INICIO = {(pDataInicio == DateTime.MinValue ? "NULL" : "'" + pDataInicio.ToString(Constantes.MascaraDataHoraSegundoSql) + "'")}");
            stringBuilder.AppendLine($"SET @DT_FIM = {(pDataFim == DateTime.MinValue ? "NULL" : "'" + pDataFim.ToString(Constantes.MascaraDataHoraSegundoSql) + "'")}");
            stringBuilder.AppendLine("SELECT");
            stringBuilder.AppendLine("  C.ID_CONTRATO,");
            stringBuilder.AppendLine("	C.ID_USUARIO,");
            stringBuilder.AppendLine("	C.ID_NUTRI,");
            stringBuilder.AppendLine("	C.RUA,");
            stringBuilder.AppendLine("	C.COMPLEMENTO,");
            stringBuilder.AppendLine("	C.NUMERO,");
            stringBuilder.AppendLine("	C.BAIRRO,");
            stringBuilder.AppendLine("  C.CIDADE,");
            stringBuilder.AppendLine("	C.ESTADO,");
            stringBuilder.AppendLine("	C.CEP,");
            stringBuilder.AppendLine("	C.DT_INICIO,");
            stringBuilder.AppendLine("	C.DT_FIM,");
            stringBuilder.AppendLine("	C.STATUS,");
            stringBuilder.AppendLine("	C.DT_CADASTRO,");
            stringBuilder.AppendLine("	C.MENSAGEM");
            stringBuilder.AppendLine("FROM CONTRATO_TB C WITH(NOLOCK)");
            stringBuilder.AppendLine("WHERE");
            stringBuilder.AppendLine("    (ISNULL(@RUA, C.RUA) = C.RUA OR C.RUA LIKE @RUA + '%')");
            stringBuilder.AppendLine("    AND(ISNULL(@CIDADE, C.CIDADE) = C.CIDADE OR C.CIDADE LIKE @CIDADE + '%')");
            stringBuilder.AppendLine("    AND(ISNULL(@BAIRRO, C.BAIRRO) = C.BAIRRO OR C.BAIRRO LIKE @BAIRRO + '%')");
            stringBuilder.AppendLine("    AND ISNULL(@CEP, C.CEP) = C.CEP");
            stringBuilder.AppendLine("    AND ISNULL(@ESTADO, C.ESTADO) = C.ESTADO");
            stringBuilder.AppendLine("    AND(C.DT_INICIO BETWEEN ISNULL(@DT_INICIO, C.DT_INICIO) AND ISNULL(@DT_FIM, C.DT_FIM)");
            stringBuilder.AppendLine("        OR C.DT_FIM BETWEEN ISNULL(@DT_INICIO, C.DT_INICIO) AND ISNULL(@DT_FIM, C.DT_FIM)");
            stringBuilder.AppendLine("    )");
            stringBuilder.AppendLine("    AND(C.ID_NUTRI = @ID_USUARIO OR C.ID_USUARIO = @ID_USUARIO)");

            #endregion
            List<Entidades.Contrato.Contrato> listaContratos = new List<Entidades.Contrato.Contrato>();

            DataSet ds = _UnitOfWork.Consulta(stringBuilder.ToString());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DateTime datainicio = DateTime.MinValue,
                        dataFim = DateTime.MinValue,
                        dataCadastro = DateTime.MinValue;

                    string Rua = string.Empty,
                        Bairro = string.Empty,
                        Cidade = string.Empty,
                        CEP = string.Empty,
                        Complemento = string.Empty,
                        Mensagem = string.Empty;

                    uint? Numero = null;
                    int idPaciente = 0, idNutricionista = 0, idContrato = 0;

                    UnidadeFederacaoEnum UF = UnidadeFederacaoEnum.NaoDefinido;
                    StatusContratoEnum statusContrato = StatusContratoEnum.Agendada;

                    if (ds.Tables[0].Rows[i]["RUA"] != DBNull.Value)
                        Rua = ds.Tables[0].Rows[i]["RUA"].ToString();
                    if (ds.Tables[0].Rows[i]["BAIRRO"] != DBNull.Value)
                        Bairro = ds.Tables[0].Rows[i]["BAIRRO"].ToString();
                    if (ds.Tables[0].Rows[i]["CIDADE"] != DBNull.Value)
                        Cidade = ds.Tables[0].Rows[i]["CIDADE"].ToString();
                    if (ds.Tables[0].Rows[i]["CEP"] != DBNull.Value)
                        CEP = ds.Tables[0].Rows[i]["CEP"].ToString();
                    if (ds.Tables[0].Rows[i]["CEP"] != DBNull.Value)
                        CEP = ds.Tables[0].Rows[i]["CEP"].ToString();
                    if (ds.Tables[0].Rows[i]["COMPLEMENTO"] != DBNull.Value)
                        Complemento = ds.Tables[0].Rows[i]["COMPLEMENTO"].ToString();
                    if (ds.Tables[0].Rows[i]["NUMERO"] != DBNull.Value)
                        Numero = Convert.ToUInt32(ds.Tables[0].Rows[i]["NUMERO"]);
                    if (ds.Tables[0].Rows[i]["ESTADO"] != DBNull.Value)
                        UF = Enum.GetValues(typeof(UnidadeFederacaoEnum)).Cast<UnidadeFederacaoEnum>().
                            FirstOrDefault(s => s.GetDefaultValue().Equals(ds.Tables[0].Rows[i]["ESTADO"].ToString(), StringComparison.CurrentCultureIgnoreCase));

                    Entidades.Nutricionista.Endereco endereco = new Entidades.Nutricionista.Endereco(
                        pIdUsuario,
                        Rua,
                        Bairro,
                        Cidade,
                        CEP,
                        UF
                        )
                    {
                        Complemento = Complemento,
                        Numero = Numero,
                    };

                    if (ds.Tables[0].Rows[i]["DT_INICIO"] != DBNull.Value)
                        datainicio = Convert.ToDateTime(ds.Tables[0].Rows[i]["DT_INICIO"]);
                    if (ds.Tables[0].Rows[i]["DT_FIM"] != DBNull.Value)
                        dataFim = Convert.ToDateTime(ds.Tables[0].Rows[i]["DT_FIM"]);
                    if (ds.Tables[0].Rows[i]["DT_CADASTRO"] != DBNull.Value)
                        dataCadastro = Convert.ToDateTime(ds.Tables[0].Rows[i]["DT_CADASTRO"]);

                    if (ds.Tables[0].Rows[i]["ID_CONTRATO"] != DBNull.Value)
                        idContrato = Convert.ToInt32(ds.Tables[0].Rows[i]["ID_CONTRATO"]);
                    if (ds.Tables[0].Rows[i]["ID_USUARIO"] != DBNull.Value)
                        idPaciente = Convert.ToInt32(ds.Tables[0].Rows[i]["ID_USUARIO"]);
                    if (ds.Tables[0].Rows[i]["ID_NUTRI"] != DBNull.Value)
                        idNutricionista = Convert.ToInt32(ds.Tables[0].Rows[i]["ID_NUTRI"]);
                    if (ds.Tables[0].Rows[i]["MENSAGEM"] != DBNull.Value)
                        Mensagem = ds.Tables[0].Rows[i]["MENSAGEM"].ToString();

                    if (ds.Tables[0].Rows[i]["STATUS"] != DBNull.Value)
                        statusContrato = Enum.GetValues(typeof(StatusContratoEnum)).Cast<StatusContratoEnum>().
                            FirstOrDefault(s => s.GetDefaultValue().Equals(ds.Tables[0].Rows[i]["STATUS"].ToString()));

                    listaContratos.Add(new Entidades.Contrato.Contrato(
                        idPaciente,
                        idNutricionista,
                        endereco.Logradouro,
                        endereco?.Complemento,
                        endereco?.Numero,
                        endereco.Bairro,
                        endereco.Cidade,
                        endereco.UF,
                        endereco.CEP,
                        datainicio,
                        dataFim,
                        statusContrato
                        )
                    {
                        IdContrato = idContrato,
                        DataCadastro = dataCadastro,
                        Mensagem = Mensagem
                    });
                }
            }

            return listaContratos;
        }

        /// <summary>
        /// Altera o status do contrato
        /// </summary>
        /// <param name="pIdContrato">ID do contrato</param>
        /// <param name="pStatusContratoEnum">novo status</param>
        public void AlterarStatusContrato(int pIdContrato, StatusContratoEnum pStatusContratoEnum)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @ID_CONTRATO INT");
            stringBuilder.AppendLine($"SET @ID_CONTRATO = {pIdContrato}");
            stringBuilder.AppendLine("UPDATE CONTRATO_TB");
            stringBuilder.AppendLine($"SET STATUS = '{pStatusContratoEnum.GetDefaultValue()}'");
            stringBuilder.AppendLine("WHERE ID_CONTRATO = @ID_CONTRATO");

            _UnitOfWork.Executar(stringBuilder.ToString());
        }

        /// <summary>
        /// Busca um contrato pelo seu número
        /// </summary>
        /// <param name="pID">Número do contrato</param>
        /// <returns>Contrato ou null</returns>
        public Entidades.Contrato.Contrato BuscaContratoPorID(int pID)
        {
            #region Query
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @ID_CONTRATO INT");
            stringBuilder.AppendLine($"SET @ID_CONTRATO = {pID}");
            stringBuilder.AppendLine("SELECT");
            stringBuilder.AppendLine("  C.ID_CONTRATO,");
            stringBuilder.AppendLine("	C.ID_USUARIO,");
            stringBuilder.AppendLine("	C.ID_NUTRI,");
            stringBuilder.AppendLine("	C.RUA,");
            stringBuilder.AppendLine("	C.COMPLEMENTO,");
            stringBuilder.AppendLine("	C.NUMERO,");
            stringBuilder.AppendLine("	C.BAIRRO,");
            stringBuilder.AppendLine("  C.CIDADE,");
            stringBuilder.AppendLine("	C.ESTADO,");
            stringBuilder.AppendLine("	C.CEP,");
            stringBuilder.AppendLine("	C.DT_INICIO,");
            stringBuilder.AppendLine("	C.DT_FIM,");
            stringBuilder.AppendLine("	C.STATUS,");
            stringBuilder.AppendLine("	C.DT_CADASTRO,");
            stringBuilder.AppendLine("	C.MENSAGEM");
            stringBuilder.AppendLine("FROM CONTRATO_TB C WITH(NOLOCK)");
            stringBuilder.AppendLine("WHERE");
            stringBuilder.AppendLine("    ID_CONTRATO = @ID_CONTRATO");

            #endregion
            List<Entidades.Contrato.Contrato> listaContratos = new List<Entidades.Contrato.Contrato>();

            DataSet ds = _UnitOfWork.Consulta(stringBuilder.ToString());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string Rua = string.Empty,
                    Bairro = string.Empty,
                    Cidade = string.Empty,
                    CEP = string.Empty,
                    Complemento = string.Empty,
                    Mensagem = string.Empty;

                uint? Numero = null;
                int idPaciente = 0, idNutricionista = 0, idContrato = 0;

                DateTime datainicio = DateTime.MinValue,
                        dataFim = DateTime.MinValue,
                        dataCadastro = DateTime.MinValue;

                UnidadeFederacaoEnum UF = UnidadeFederacaoEnum.NaoDefinido;
                StatusContratoEnum statusContrato = StatusContratoEnum.Agendada;

                if (ds.Tables[0].Rows[0]["ID_CONTRATO"] != DBNull.Value)
                    idContrato = Convert.ToInt32(ds.Tables[0].Rows[0]["ID_CONTRATO"]);
                if (ds.Tables[0].Rows[0]["ID_USUARIO"] != DBNull.Value)
                    idPaciente = Convert.ToInt32(ds.Tables[0].Rows[0]["ID_USUARIO"]);
                if (ds.Tables[0].Rows[0]["ID_NUTRI"] != DBNull.Value)
                    idNutricionista = Convert.ToInt32(ds.Tables[0].Rows[0]["ID_NUTRI"]);
                if (ds.Tables[0].Rows[0]["RUA"] != DBNull.Value)
                    Rua = ds.Tables[0].Rows[0]["RUA"].ToString();
                if (ds.Tables[0].Rows[0]["BAIRRO"] != DBNull.Value)
                    Bairro = ds.Tables[0].Rows[0]["BAIRRO"].ToString();
                if (ds.Tables[0].Rows[0]["CIDADE"] != DBNull.Value)
                    Cidade = ds.Tables[0].Rows[0]["CIDADE"].ToString();
                if (ds.Tables[0].Rows[0]["CEP"] != DBNull.Value)
                    CEP = ds.Tables[0].Rows[0]["CEP"].ToString();
                if (ds.Tables[0].Rows[0]["CEP"] != DBNull.Value)
                    CEP = ds.Tables[0].Rows[0]["CEP"].ToString();
                if (ds.Tables[0].Rows[0]["COMPLEMENTO"] != DBNull.Value)
                    Complemento = ds.Tables[0].Rows[0]["COMPLEMENTO"].ToString();
                if (ds.Tables[0].Rows[0]["NUMERO"] != DBNull.Value)
                    Numero = Convert.ToUInt32(ds.Tables[0].Rows[0]["NUMERO"]);
                if (ds.Tables[0].Rows[0]["ESTADO"] != DBNull.Value)
                    UF = Enum.GetValues(typeof(UnidadeFederacaoEnum)).Cast<UnidadeFederacaoEnum>().
                        FirstOrDefault(s => s.GetDefaultValue().Equals(ds.Tables[0].Rows[0]["ESTADO"].ToString(), StringComparison.CurrentCultureIgnoreCase));

                Entidades.Nutricionista.Endereco endereco = new Entidades.Nutricionista.Endereco(
                    idNutricionista,
                    Rua,
                    Bairro,
                    Cidade,
                    CEP,
                    UF
                    )
                {
                    Complemento = Complemento,
                    Numero = Numero,
                };

                if (ds.Tables[0].Rows[0]["DT_INICIO"] != DBNull.Value)
                    datainicio = Convert.ToDateTime(ds.Tables[0].Rows[0]["DT_INICIO"]);
                if (ds.Tables[0].Rows[0]["DT_FIM"] != DBNull.Value)
                    dataFim = Convert.ToDateTime(ds.Tables[0].Rows[0]["DT_FIM"]);
                if (ds.Tables[0].Rows[0]["DT_CADASTRO"] != DBNull.Value)
                    dataCadastro = Convert.ToDateTime(ds.Tables[0].Rows[0]["DT_CADASTRO"]);

                if (ds.Tables[0].Rows[0]["STATUS"] != DBNull.Value)
                    statusContrato = Enum.GetValues(typeof(StatusContratoEnum)).Cast<StatusContratoEnum>().
                        FirstOrDefault(s => s.GetDefaultValue().Equals(ds.Tables[0].Rows[0]["STATUS"].ToString()));

                if (ds.Tables[0].Rows[0]["MENSAGEM"] != DBNull.Value)
                    Mensagem = ds.Tables[0].Rows[0]["MENSAGEM"].ToString();

                return new Entidades.Contrato.Contrato(
                    idPaciente,
                    idNutricionista,
                    endereco.Logradouro,
                    endereco?.Complemento,
                    endereco?.Numero,
                    endereco.Bairro,
                    endereco.Cidade,
                    endereco.UF,
                    endereco.CEP,
                    datainicio,
                    dataFim,
                    statusContrato
                    )
                {
                    IdContrato = idContrato,
                    DataCadastro = dataCadastro,
                    Mensagem = Mensagem
                };

            }

            return null;
        }

        /// <summary>
        /// Agenda disponível para contratação?
        /// </summary>
        /// <param name="pIdAgenda">ID da agenda</param>
        /// <returns>Retorna se a agenda está disponível para contratar</returns>
        public bool AgendaDisponivelParaContratar(int pIdAgenda)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @ID_AGENDA INT");
            stringBuilder.AppendLine($"SET @ID_AGENDA = {pIdAgenda}");
            stringBuilder.AppendLine("SELECT TOP 1 1 FROM CONTRATO_TB TB WITH(NOLOCK)");
            stringBuilder.AppendLine("    INNER JOIN AGENDA_TB AG WITH(NOLOCK) ON TB.ID_NUTRI = AG.ID_USUARIO");
            stringBuilder.AppendLine("WHERE AG.ID_AGENDA = @ID_AGENDA AND TB.STATUS NOT IN ('CN','CP')");

            DataSet ds = _UnitOfWork.Consulta(stringBuilder.ToString());

            return !(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0);
        }

        /// <summary>
        /// Realiza o atendimento
        /// </summary>
        /// <param name="idContrato">ID do contrato</param>
        /// <param name="mensagemAtendimento">Mensagem de atendimento</param>
        public void RealizarAtendimento(int idContrato, string mensagemAtendimento)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("DECLARE @MENSAGEM VARCHAR(255),");
            stringBuilder.AppendLine("    @ID_CONTRATO INT");
            stringBuilder.AppendLine($"SET @MENSAGEM = '{mensagemAtendimento}'");
            stringBuilder.AppendLine($"SET @ID_CONTRATO = {idContrato}");
            stringBuilder.AppendLine("UPDATE CONTRATO_TB");
            stringBuilder.AppendLine("    SET MENSAGEM = @MENSAGEM");
            stringBuilder.AppendLine("WHERE ID_CONTRATO = @ID_CONTRATO");

            _UnitOfWork.Executar(stringBuilder.ToString());
        }
    }
}

#region Histórico de manutenções
/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Implementação inicial
*/
#endregion
using ContratacaoNutricionistas.Domain.Enumerados.Gerais;
using ContratacaoNutricionistas.Domain.Interfaces.Endereco;
using ContratacaoNutricionistas.Domain.Repository.Repository;
using DataBaseHelper.Interfaces;
using ModulosHelper.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ContratacaoNutricionistas.Domain.Repository.Endereco
{
    /// <summary>
    /// Comandos no banco para endereço
    /// </summary>
    public class EnderecoRepository : RepositoryBase, IEnderecoRepository
    {
        #region Construtor
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="UnitOfWork">Unit of work</param>
        public EnderecoRepository(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Lista os endereços cadastrados
        /// </summary>
        /// <param name="pIdUsuario">ID do usuário</param>
        /// <param name="pRua">Rua</param>
        /// <param name="pCidade">Cidade</param>
        /// <param name="pBairro">Bairro</param>
        /// <param name="pCEP">CEP</param>
        /// <param name="pUF">UF</param>
        /// <returns>Uma lista de endereços</returns>
        public List<Entidades.Nutricionista.Endereco> EnderecosCadastrados(int pIdUsuario, string pRua, string pCidade, string pBairro, string pCEP, string pUF)
        {
            StringBuilder stringBuilder = new StringBuilder();

            #region Select
            stringBuilder.AppendLine("DECLARE @ID_USUARIO INT,");
            stringBuilder.AppendLine("  @RUA VARCHAR(100),");
            stringBuilder.AppendLine("  @BAIRRO VARCHAR(50),");
            stringBuilder.AppendLine("  @CIDADE VARCHAR(30),");
            stringBuilder.AppendLine("  @CEP VARCHAR(9) = NULL,");
            stringBuilder.AppendLine("  @ESTADO VARCHAR(2) = NULL");
            stringBuilder.AppendLine($"SET @ID_USUARIO = {pIdUsuario}");
            stringBuilder.AppendLine($"SET @RUA = '{pRua}'");
            stringBuilder.AppendLine($"SET @BAIRRO = '{pBairro}'");
            stringBuilder.AppendLine($"SET @CIDADE = '{pCidade}'");
            stringBuilder.AppendLine($"SET @CEP = {(string.IsNullOrEmpty(pCEP) ? "NULL" : "'" + pCEP + "'")}");
            stringBuilder.AppendLine($"SET @ESTADO = {(string.IsNullOrEmpty(pUF) ? "NULL" : "'" + pUF + "'")}");
            stringBuilder.AppendLine("SELECT");
            stringBuilder.AppendLine("  E.ID_ENDERECO,");
            stringBuilder.AppendLine("  E.RUA,");
            stringBuilder.AppendLine("	E.COMPLEMENTO,");
            stringBuilder.AppendLine("	E.NUMERO,");
            stringBuilder.AppendLine("	E.BAIRRO,");
            stringBuilder.AppendLine("	E.CIDADE,");
            stringBuilder.AppendLine("	E.ESTADO,");
            stringBuilder.AppendLine("	E.CEP");
            stringBuilder.AppendLine("FROM ENDERECO_TB E WITH(NOLOCK)");
            stringBuilder.AppendLine("    INNER JOIN USUARIO_TB U WITH(NOLOCK) ON U.ID_USUARIO = E.ID_USUARIO");
            stringBuilder.AppendLine("        AND U.TP_USUARIO = 1");
            stringBuilder.AppendLine("WHERE(ISNULL(@RUA, E.RUA) = E.RUA OR E.RUA LIKE @RUA + '%')");
            stringBuilder.AppendLine("    AND(ISNULL(@BAIRRO, E.BAIRRO) = E.BAIRRO OR E.BAIRRO LIKE @BAIRRO + '%')");
            stringBuilder.AppendLine("    AND(ISNULL(@CIDADE, E.CIDADE) = E.CIDADE OR E.CIDADE LIKE @CIDADE + '%')");
            stringBuilder.AppendLine("    AND ISNULL(@ESTADO, E.ESTADO) = E.ESTADO");
            stringBuilder.AppendLine("    AND ISNULL(@CEP, E.CEP) = E.CEP");
            stringBuilder.AppendLine("    AND @ID_USUARIO = E.ID_USUARIO");
            #endregion

            DataSet ds = _UnitOfWork.Consulta(stringBuilder.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<Entidades.Nutricionista.Endereco> enderecos = new List<Entidades.Nutricionista.Endereco>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int idEndereco = 0;
                    string Rua = string.Empty,
                        Bairro = string.Empty,
                        Cidade = string.Empty,
                        CEP = string.Empty,
                        Complemento = string.Empty;

                    uint? Numero = null;

                    UnidadeFederacaoEnum UF = UnidadeFederacaoEnum.NaoDefinido;

                    if (ds.Tables[0].Rows[i]["ID_ENDERECO"] != DBNull.Value)
                        idEndereco = Convert.ToInt32(ds.Tables[0].Rows[i]["ID_ENDERECO"].ToString());
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
                            FirstOrDefault(s => s.GetDefaultValue().Equals(ds.Tables[0].Rows[i]["ESTADO"].ToString()));

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
                        IdEndereco = idEndereco
                    };



                    enderecos.Add(endereco);
                }

                return enderecos;
            }
            else
            {
                return new List<Entidades.Nutricionista.Endereco>();
            }
        }


        /// <summary>
        /// Altera os dados do endereço
        /// </summary>
        /// <param name="pEndereco"></param>
        public void AlterarDadosEndereco(Entidades.Nutricionista.Endereco pEndereco)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @ID_ENDERECO INT,");
            stringBuilder.AppendLine("	@ID_USUARIO INT,");
            stringBuilder.AppendLine("  @NUMERO INT = NULL,");
            stringBuilder.AppendLine("	@RUA VARCHAR(100),");
            stringBuilder.AppendLine("	@COMPLEMENTO VARCHAR(255),");
            stringBuilder.AppendLine("	@BAIRRO VARCHAR(50),");
            stringBuilder.AppendLine("	@CIDADE VARCHAR(30),");
            stringBuilder.AppendLine("	@ESTADO VARCHAR(2),");
            stringBuilder.AppendLine("	@CEP VARCHAR(9)");
            stringBuilder.AppendLine($"SET @ID_ENDERECO = {pEndereco.IdEndereco}");
            stringBuilder.AppendLine($"SET @ID_USUARIO = {pEndereco.IdUsuario}");
            stringBuilder.AppendLine($"SET @NUMERO = {(pEndereco?.Numero == null ? "NULL" : pEndereco.Numero.ToString())}");
            stringBuilder.AppendLine($"SET @RUA = '{pEndereco.Logradouro}'");
            stringBuilder.AppendLine($"SET @COMPLEMENTO = '{pEndereco.Complemento}'");
            stringBuilder.AppendLine($"SET @BAIRRO = '{pEndereco.Bairro}'");
            stringBuilder.AppendLine($"SET @CIDADE = '{pEndereco.Cidade}'");
            stringBuilder.AppendLine($"SET @ESTADO = '{pEndereco.UF.GetDefaultValue()}'");
            stringBuilder.AppendLine($"SET @CEP = '{pEndereco.CEP}'");
            stringBuilder.AppendLine("UPDATE ENDERECO_TB");
            stringBuilder.AppendLine("    SET RUA = @RUA,");
            stringBuilder.AppendLine("    COMPLEMENTO = @COMPLEMENTO,");
            stringBuilder.AppendLine("    NUMERO = @NUMERO,");
            stringBuilder.AppendLine("    BAIRRO = @BAIRRO,");
            stringBuilder.AppendLine("    CIDADE = @CIDADE,");
            stringBuilder.AppendLine("    ESTADO = @ESTADO,");
            stringBuilder.AppendLine("    CEP = @CEP");
            stringBuilder.AppendLine("WHERE");
            stringBuilder.AppendLine("    ID_ENDERECO = @ID_ENDERECO");
            stringBuilder.AppendLine("    AND ID_USUARIO = @ID_USUARIO");
            stringBuilder.AppendLine("    AND EXISTS(SELECT TOP 1 1 FROM USUARIO_TB TB WITH(NOLOCK)");
            stringBuilder.AppendLine("        WHERE TB.ID_USUARIO = @ID_USUARIO");
            stringBuilder.AppendLine("        AND TB.TP_USUARIO = 1)");

            _UnitOfWork.Executar(stringBuilder.ToString());
        }


        /// <summary>
        /// Consulta o endereço do nutricionista pelo ID
        /// </summary>
        /// <param name="pID">ID do endereço</param>
        /// <param name="pIDUsuario">ID do usuário</param>
        /// <returns>Endereço ou null</returns>
        public Entidades.Nutricionista.Endereco ConsultarEnderecoNutricionistaPorID(int pID, int pIDUsuario)
        {
            StringBuilder stringBuilder = new StringBuilder();

            #region Select
            stringBuilder.AppendLine("DECLARE @ID_USUARIO INT,");
            stringBuilder.AppendLine("  @ID_ENDERECO INT");
            stringBuilder.AppendLine($"SET @ID_USUARIO = {pIDUsuario}");
            stringBuilder.AppendLine($"SET @ID_ENDERECO = {pID}");
            stringBuilder.AppendLine("SELECT");
            stringBuilder.AppendLine("  E.ID_ENDERECO,");
            stringBuilder.AppendLine("  E.RUA,");
            stringBuilder.AppendLine("	E.COMPLEMENTO,");
            stringBuilder.AppendLine("	E.NUMERO,");
            stringBuilder.AppendLine("	E.BAIRRO,");
            stringBuilder.AppendLine("	E.CIDADE,");
            stringBuilder.AppendLine("	E.ESTADO,");
            stringBuilder.AppendLine("	E.CEP");
            stringBuilder.AppendLine("FROM ENDERECO_TB E WITH(NOLOCK)");
            stringBuilder.AppendLine("    INNER JOIN USUARIO_TB U WITH(NOLOCK) ON U.ID_USUARIO = E.ID_USUARIO");
            stringBuilder.AppendLine("        AND U.TP_USUARIO = 1");
            stringBuilder.AppendLine("WHERE @ID_ENDERECO = E.ID_ENDERECO");
            stringBuilder.AppendLine("    AND @ID_USUARIO = E.ID_USUARIO");
            #endregion

            DataSet ds = _UnitOfWork.Consulta(stringBuilder.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int idEndereco = 0;
                string Rua = string.Empty,
                    Bairro = string.Empty,
                    Cidade = string.Empty,
                    CEP = string.Empty,
                    Complemento = string.Empty;

                uint? Numero = null;

                UnidadeFederacaoEnum UF = UnidadeFederacaoEnum.NaoDefinido;

                if (ds.Tables[0].Rows[0]["ID_ENDERECO"] != DBNull.Value)
                    idEndereco = Convert.ToInt32(ds.Tables[0].Rows[0]["ID_ENDERECO"].ToString());
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
                        FirstOrDefault(s => s.GetDefaultValue().Equals(ds.Tables[0].Rows[0]["ESTADO"].ToString()));

                Entidades.Nutricionista.Endereco endereco = new Entidades.Nutricionista.Endereco(
                        pIDUsuario,
                        Rua,
                        Bairro,
                        Cidade,
                        CEP,
                        UF
                        )
                {
                    Complemento = Complemento,
                    Numero = Numero,
                    IdEndereco = idEndereco
                };
                return endereco;
            }
            else
                return null;
        }

        /// <summary>
        /// Cadastra um endereço
        /// </summary>
        /// <param name="pEndereco">Endereço a ser cadastrado</param>
        public void CadastrarEndereco(Entidades.Nutricionista.Endereco pEndereco)
        {
            _UnitOfWork.Executar(
                _UnitOfWork.MontaInsertPorAttributo(pEndereco).ToString()
                );
        }
        #endregion
    }
}

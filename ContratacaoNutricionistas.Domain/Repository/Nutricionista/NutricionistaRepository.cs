#region Histórico de manutenções
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe que faz os comandos com o banco de dados para Nutricionista.
 */

/*
* Programador: Pedro Henrique Pires
* Data: 30/05/2020
* Implementação: Implementação de métodos de alteração e consulta.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Ajuste nos métodos de alteração e consulta.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Recuperando informações corretamente e ajustando comando SQL.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Restringindo pelo tipo de usuário.
*/

/*
 * Programador: Pedro Henrique Pires
 * Data: 03/06/2020
 * Implementação: Implementação de cadastro/alteração de endereços
 */
#endregion


using ContratacaoNutricionistas.Domain.Entidades.Nutricionista;
using ContratacaoNutricionistas.Domain.Enumerados.Gerais;
using ContratacaoNutricionistas.Domain.Interfaces.Nutricionista;
using DataBaseHelper.Interfaces;
using ModulosHelper.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ContratacaoNutricionistas.Domain.Repository.Nutricionista
{
    /// <summary>
    /// Classe que faz os comandos no banco de dados para nutricionista
    /// </summary>
    public class NutricionistaRepository : INutricionistaRepository
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
        public NutricionistaRepository(IUnitOfWork pIUnitOfWork)
        {
            _UnitOfWork = pIUnitOfWork;
        }
        #endregion

        /// <summary>
        /// Método que cadastra um nutricionista
        /// </summary>
        /// <param name="pNutricionistaCadastro">Nutricionista a ser cadastrado</param>
        public void CadastrarNutricionista(Entidades.Nutricionista.NutricionistaCadastro pNutricionistaCadastro)
        {
            /*Executa o comando passado por parâmetro.*/
            _UnitOfWork.Executar(
                _UnitOfWork.MontaInsertPorAttributo(pNutricionistaCadastro).ToString() /*Monta o comando SQL a partir dos atributos da classe*/
             );
        }

        /// <summary>
        /// Método que retorna um nutricionista pelo ID
        /// </summary>
        /// <param name="pID">ID</param>
        /// <returns>Nutricionista ou null</returns>
        public NutricionistaAlteracao ConsultarNutricionistaPorID(int pID)
        {
            NutricionistaAlteracao nutricionistaAlteracao;

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("DECLARE @ID INT");
            stringBuilder.AppendLine($"SET @ID = {pID}");
            stringBuilder.AppendLine("SELECT");
            stringBuilder.AppendLine("  TB.ID_USUARIO,");
            stringBuilder.AppendLine("  TB.CPF,");
            stringBuilder.AppendLine("  TB.NOME,");
            stringBuilder.AppendLine("  TB.CRN,");
            stringBuilder.AppendLine("  TB.TELEFONE,");
            stringBuilder.AppendLine("  TB.LOGIN,");
            stringBuilder.AppendLine("  TB.SENHA");
            stringBuilder.AppendLine("FROM USUARIO_TB TB WITH(NOLOCK)");
            stringBuilder.AppendLine("WHERE TB.ID_USUARIO = @ID AND TB.TP_USUARIO = 1");

            DataSet ds = _UnitOfWork.Consulta(stringBuilder.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int ID = 0, CRN = 0;
                string Nome = string.Empty,
                    Telefone = string.Empty,
                    Login = string.Empty,
                    Senha = string.Empty,
                    CPF = string.Empty;

                if (ds.Tables[0].Rows[0]["ID_USUARIO"] != DBNull.Value)
                    ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID_USUARIO"]);
                if (ds.Tables[0].Rows[0]["CRN"] != DBNull.Value)
                    CRN = Convert.ToInt32(ds.Tables[0].Rows[0]["CRN"]);
                if (ds.Tables[0].Rows[0]["CPF"] != DBNull.Value)
                    CPF = ds.Tables[0].Rows[0]["CPF"].ToString();
                if (ds.Tables[0].Rows[0]["NOME"] != DBNull.Value)
                    Nome = ds.Tables[0].Rows[0]["NOME"].ToString();
                if (ds.Tables[0].Rows[0]["TELEFONE"] != DBNull.Value)
                    Telefone = ds.Tables[0].Rows[0]["TELEFONE"].ToString();
                if (ds.Tables[0].Rows[0]["LOGIN"] != DBNull.Value)
                    Login = ds.Tables[0].Rows[0]["LOGIN"].ToString();
                if (ds.Tables[0].Rows[0]["SENHA"] != DBNull.Value)
                    Senha = ds.Tables[0].Rows[0]["SENHA"].ToString();

                nutricionistaAlteracao = new NutricionistaAlteracao(
                    ID,
                    Nome,
                    Telefone,
                    CRN,
                    Login,
                    Senha,
                    new Entidades.Usuario.CPF(CPF, false)
                    );
            }
            else
                nutricionistaAlteracao = null;

            return nutricionistaAlteracao;
        }

        /// <summary>
        /// Altera os dados do nutricionista
        /// </summary>
        /// <param name="pNutricionistaAlteracao">Nutricionista a ser alterado</param>
        public void AlterarDadosNutricionista(NutricionistaAlteracao pNutricionistaAlteracao)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("DECLARE @ID INT, @CRN INT");
            stringBuilder.AppendLine("DECLARE @CPF VARCHAR(14),@NOME VARCHAR(50), @TELEFONE VARCHAR(15), @SENHA VARCHAR(8)");
            stringBuilder.AppendLine($"SET @ID = {pNutricionistaAlteracao.ID}");
            stringBuilder.AppendLine($"SET @CRN = {pNutricionistaAlteracao.CRN}");
            stringBuilder.AppendLine($"SET @CPF = '{pNutricionistaAlteracao.CpfObjeto.Numero}'");
            stringBuilder.AppendLine($"SET @NOME = '{pNutricionistaAlteracao.Nome}'");
            stringBuilder.AppendLine($"SET @TELEFONE = '{pNutricionistaAlteracao.Telefone}'");
            stringBuilder.AppendLine($"SET @SENHA = '{pNutricionistaAlteracao.Senha}'");
            stringBuilder.AppendLine("UPDATE USUARIO_TB");
            stringBuilder.AppendLine("SET");
            stringBuilder.AppendLine("    CPF = @CPF,");
            stringBuilder.AppendLine("    NOME = @NOME,");
            stringBuilder.AppendLine("    TELEFONE = @TELEFONE,");
            stringBuilder.AppendLine("    SENHA = @SENHA,");
            stringBuilder.AppendLine("    CRN = @CRN");
            stringBuilder.AppendLine("WHERE ID_USUARIO = @ID AND TP_USUARIO = 1");

            _UnitOfWork.Executar(stringBuilder.ToString());
        }

        /// <summary>
        /// Cadastra um endereço
        /// </summary>
        /// <param name="pEndereco">Endereço a ser cadastrado</param>
        public void CadastrarEndereco(Endereco pEndereco)
        {
            _UnitOfWork.Executar(
                _UnitOfWork.MontaInsertPorAttributo(pEndereco).ToString()
                );
        }

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
        public List<Endereco> EnderecosCadastrados(int pIdUsuario, string pRua, string pCidade, string pBairro, string pCEP, string pUF)
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
                List<Endereco> enderecos = new List<Endereco>();

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

                    Endereco endereco = new Endereco(
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
                return new List<Endereco>();
            }
        }

        /// <summary>
        /// Consulta o endereço do nutricionista pelo ID
        /// </summary>
        /// <param name="pID">ID do endereço</param>
        /// <param name="pIDUsuario">ID do usuário</param>
        /// <returns>Endereço ou null</returns>
        public Endereco ConsultarEnderecoNutricionistaPorID(int pID, int pIDUsuario)
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

                Endereco endereco = new Endereco(
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
        /// Altera os dados do endereço
        /// </summary>
        /// <param name="pEndereco"></param>
        public void AlterarDadosEndereco(Endereco pEndereco)
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
            stringBuilder.AppendLine($"SET @NUMERO = {(pEndereco?.Numero == null? "NULL": pEndereco.Numero.ToString())}");
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
    }
}

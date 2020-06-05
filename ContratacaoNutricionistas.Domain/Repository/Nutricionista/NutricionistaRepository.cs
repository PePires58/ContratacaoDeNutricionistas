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

/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Herdando do repositório base.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 03/06/2020
* Implementação: Migração para classe de endereço
*/
#endregion

using ContratacaoNutricionistas.Domain.Entidades.Nutricionista;
using ContratacaoNutricionistas.Domain.Interfaces.Nutricionista;
using ContratacaoNutricionistas.Domain.Repository.Repository;
using DataBaseHelper.Interfaces;
using System;
using System.Data;
using System.Text;

namespace ContratacaoNutricionistas.Domain.Repository.Nutricionista
{
    /// <summary>
    /// Classe que faz os comandos no banco de dados para nutricionista
    /// </summary>
    public class NutricionistaRepository : RepositoryBase, INutricionistaRepository
    {

        #region Construtor
        /// <summary>
        /// Unidade de trabalho
        /// </summary>
        /// <param name="pIUnitOfWork"></param>
        public NutricionistaRepository(IUnitOfWork pIUnitOfWork): base(pIUnitOfWork)
        {
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
    }
}

#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de paciente repository
 */

/*
 * Programador: Pedro Henrique Pires
 * Data: 01/06/2020
 * Implementação: Implementação dos métodos de alteração e consulta.
 */
#endregion

using ContratacaoNutricionistas.Domain.Entidades.Paciente;
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

        /// <summary>
        /// Método que altera os dados do paciente
        /// </summary>
        /// <param name="pacienteAlteracao">Paciente a ser alterado</param>
        public void AlterarDados(PacienteAlteracao pacienteAlteracao)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("DECLARE @ID INT");
            stringBuilder.AppendLine("DECLARE @CPF VARCHAR(14),@NOME VARCHAR(50), @TELEFONE VARCHAR(15), @SENHA VARCHAR(8)");
            stringBuilder.AppendLine($"SET @ID = {pacienteAlteracao.ID}");
            stringBuilder.AppendLine($"SET @CPF = '{pacienteAlteracao.CpfObjeto.Numero}'");
            stringBuilder.AppendLine($"SET @NOME = '{pacienteAlteracao.Nome}'");
            stringBuilder.AppendLine($"SET @TELEFONE = '{pacienteAlteracao.Telefone}'");
            stringBuilder.AppendLine($"SET @SENHA = '{pacienteAlteracao.Senha}'");
            stringBuilder.AppendLine("UPDATE USUARIO_TB");
            stringBuilder.AppendLine("SET");
            stringBuilder.AppendLine("    CPF = @CPF,");
            stringBuilder.AppendLine("    NOME = @NOME,");
            stringBuilder.AppendLine("    TELEFONE = @TELEFONE,");
            stringBuilder.AppendLine("    SENHA = @SENHA");
            stringBuilder.AppendLine("WHERE ID_USUARIO = @ID");

            _UnitOfWork.Executar(stringBuilder.ToString());
        }

        /// <summary>
        /// Consulta um paciente pelo ID
        /// </summary>
        /// <param name="pID">ID do paciente</param>
        /// <returns>Paciente para alteração ou NULL</returns>
        public PacienteAlteracao ConsultarPacientePorID(int pID)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("DECLARE @ID INT");
            stringBuilder.AppendLine($"SET @ID = {pID}");
            stringBuilder.AppendLine("SELECT");
            stringBuilder.AppendLine("  TB.ID_USUARIO,");
            stringBuilder.AppendLine("  TB.CPF,");
            stringBuilder.AppendLine("  TB.NOME,");
            stringBuilder.AppendLine("  TB.TELEFONE,");
            stringBuilder.AppendLine("  TB.LOGIN,");
            stringBuilder.AppendLine("  TB.SENHA");
            stringBuilder.AppendLine("FROM USUARIO_TB TB WITH(NOLOCK)");
            stringBuilder.AppendLine("WHERE TB.ID_USUARIO = @ID");

            DataSet ds = _UnitOfWork.Consulta(stringBuilder.ToString());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int ID = 0;
                string Nome = string.Empty, 
                    Telefone = string.Empty, 
                    CPF = string.Empty, 
                    Login = string.Empty,
                    Senha = string.Empty;

                if (ds.Tables[0].Rows[0]["ID_USUARIO"] != DBNull.Value)
                    ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID_USUARIO"]);
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

                return new PacienteAlteracao(
                    ID,
                    Nome,
                    Telefone,
                    Login,
                    Senha,
                    new Entidades.Usuario.CPF(CPF, false));
            }
            return null;
        }
    }
}

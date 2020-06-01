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
#endregion


using ContratacaoNutricionistas.Domain.Entidades.Nutricionista;
using ContratacaoNutricionistas.Domain.Interfaces.Nutricionista;
using DataBaseHelper.Interfaces;
using System;
using System.Data;
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

            /*Montar query com o stringbuilder*/
            //stringBuilder.AppendLine("SELECT...");

            DataSet ds = _UnitOfWork.Consulta(stringBuilder.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int ID = 0;
                string Nome = string.Empty,
                    CRM = string.Empty,
                    Telefone = string.Empty,
                    Login = string.Empty, 
                    Senha = string.Empty;
                Entidades.Usuario.CPF CpfObjeto = null;

                if (ds.Tables[0].Rows[0]["ID_USUARIO"] != DBNull.Value)
                    ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID_USUARIO"].ToString());
                if (ds.Tables[0].Rows[0]["CPF"] != DBNull.Value)
                {
                    CpfObjeto = new Entidades.Usuario.CPF(ds.Tables[0].Rows[0]["CPF"].ToString(), false);
                }

                /*Continuar com demais campos*/

                nutricionistaAlteracao = new NutricionistaAlteracao(
                    ID,
                    Nome,
                    Telefone,
                    CRM,
                    Login,
                    Senha,
                    CpfObjeto
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
            /*Montar comando*/

            //_UnitOfWork.Executar(stringBuilder.ToString());
        }
    }
}

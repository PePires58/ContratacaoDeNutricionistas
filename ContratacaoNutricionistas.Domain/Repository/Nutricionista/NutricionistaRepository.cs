#region Histórico de manutenções
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe que faz os comandos com o banco de dados para Nutricionista.
 */
#endregion


using ContratacaoNutricionistas.Domain.Interfaces.Nutricionista;
using DataBaseHelper.Interfaces;

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
    }
}

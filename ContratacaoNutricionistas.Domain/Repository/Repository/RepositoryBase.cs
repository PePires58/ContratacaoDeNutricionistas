#region Histórico de manutenções
/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Implementação inicial.
*/
#endregion
using ContratacaoNutricionistas.Domain.Interfaces.Repository;
using DataBaseHelper.Interfaces;

namespace ContratacaoNutricionistas.Domain.Repository.Repository
{
    /// <summary>
    /// Repositório de base
    /// </summary>
    public class RepositoryBase : IRepositoryBase
    {
        #region Propriedades Readonly
        /// <summary>
        /// Unidade de conexão e execução com banco de dados
        /// </summary>
        protected readonly IUnitOfWork _UnitOfWork;
        #endregion

        #region Construtores
        /// <summary>
        /// Repositório base
        /// </summary>
        /// <param name="UnitOfWork"></param>
        public RepositoryBase(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Começa a transação
        /// </summary>
        public void BeginTransaction()
        {
            _UnitOfWork.BeginTransaction();
        }

        /// <summary>
        /// Commit
        /// </summary>
        public void Commit()
        {
            _UnitOfWork.Commit();
        }

        /// <summary>
        /// Rollback
        /// </summary>
        public void RollBack()
        {
            _UnitOfWork.Rollback();
        }

        #endregion

    }
}

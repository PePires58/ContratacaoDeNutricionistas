#region Histórico de manutenções
/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Implementação inicial.
*/
#endregion

namespace ContratacaoNutricionistas.Domain.Interfaces.Repository
{
    /// <summary>
    /// Repository Base
    /// </summary>
    public interface IRepositoryBase
    {
        /// <summary>
        /// Abre a transação
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Realiza o commit
        /// </summary>
        void Commit();

        /// <summary>
        /// Realiza o rollback
        /// </summary>
        void RollBack();
    }
}

#region Histórico de manutenções
/*
Data: 10/06/2020
Programador: Pedro Henrique Pires
Descrição: Implementação inicial.
*/
#endregion

namespace ContratacaoNutricionistas.Domain.Interfaces.Contrato
{
    /// <summary>
    /// Serviço de contrato
    /// </summary>
    public interface IServiceContrato
    {
        /// <summary>
        /// Contrata um nutricionista
        /// </summary>
        /// <param name="pContrato">Contrato</param>
        void ContratarNutricionista(Entidades.Contrato.Contrato pContrato);
    }
}

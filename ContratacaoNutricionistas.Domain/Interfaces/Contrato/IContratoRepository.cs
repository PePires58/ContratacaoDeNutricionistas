#region Histórico de manutenções
/*
Data: 10/06/2020
Programador: Pedro Henrique Pires
Descrição: Implementação inicial.
*/
#endregion
using System;

namespace ContratacaoNutricionistas.Domain.Interfaces.Contrato
{
    public interface IContratoRepository
    {
        /// <summary>
        /// Verifica se o paciente já possui um contrato para a data informada
        /// </summary>
        /// <param name="pDataInicio">Data de início</param>
        /// <param name="pDataTermino">Data de término</param>
        /// <param name="pIdUsuario">Id do usuário</param>
        /// <returns>Se o paciente já tem contrato ativo</returns>
        bool VerificarContratoExistenteNaData(DateTime pDataInicio, DateTime pDataTermino, int pIdUsuario);

        // <summary>
        /// Contrata um nutricionista
        /// </summary>
        /// <param name="pContrato">Contrato</param>
        void ContratarNutricionista(Entidades.Contrato.Contrato pContrato);
    }
}

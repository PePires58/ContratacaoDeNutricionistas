#region Histórico de manutenções
/*
Data: 10/06/2020
Programador: Pedro Henrique Pires
Descrição: Implementação inicial.
*/


/*
Data: 13/06/2020
Programador: Pedro Henrique Pires
Descrição: Lista os contratos disponíveis.
*/
#endregion

using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Lista os constratos de algum usuário
        /// </summary>
        /// <param name="pRua">Rua</param>
        /// <param name="pCidade">Cidade</param>
        /// <param name="pBairro">Bairro</param>
        /// <param name="pCEP">CEP</param>
        /// <param name="pUF">UF</param>
        /// <param name="pDataInicio">Data de início</param>
        /// <param name="pDataFim">Data fim</param>
        /// <param name="pIdUsuario">ID do usuário</param>
        /// <returns>Lista de contratos</returns>
        List<Entidades.Contrato.Contrato> ListaContratos(string pRua, string pCidade, string pBairro, string pCEP, string pUF, DateTime pDataInicio, DateTime pDataFim, int pIdUsuario);

    }
}

﻿#region Histórico de manutenções
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

/*
Data: 15/06/2020
Programador: Pedro Henrique Pires
Descrição: Incluindo método de buscar contrato e alterar status.
*/

/*
Data: 19/06/2020
Programador: Pedro Henrique Pires
Descrição: Verifica se a agenda está disponível para contratação.
*/


/*
Data: 26/06/2020
Programador: Pedro Henrique Pires
Descrição: Método para realizar o atendimento.
*/
#endregion

using System;
using System.Collections.Generic;
using ContratacaoNutricionistas.Domain.Enumerados.Contrato;

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
        /// Verifica se a agenda está disponível para contratação
        /// </summary>
        /// <param name="pIdAgenda">ID da agenda</param>
        /// <returns>Retorna se a agenda está disponível para contratação</returns>
        bool AgendaDisponivelParaContratar(int pIdAgenda);

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

        /// <summary>
        /// Busca contrato por ID
        /// </summary>
        /// <param name="pID">ID do contrato</param>
        /// <returns>Contrato</returns>
        Entidades.Contrato.Contrato BuscaContratoPorID(int pID);

        /// <summary>
        /// Cancela um contrato
        /// </summary>
        /// <param name="idContrato">ID do contrato</param>
        /// <param name="statusContratoEnum">Status de cancelamento (pelo paciente ou nutricionista)</param>
        void AlterarStatusContrato(int idContrato, StatusContratoEnum statusContratoEnum);

        /// <summary>
        /// Realiza o atendimento
        /// </summary>
        /// <param name="idContrato">Id do contrato</param>
        /// <param name="mensagemAtendimento">Mensagem de atendimento</param>
        void RealizarAtendimento(int idContrato, string mensagemAtendimento);
    }
}

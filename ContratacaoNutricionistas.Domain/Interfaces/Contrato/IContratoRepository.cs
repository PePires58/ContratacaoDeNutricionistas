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

/*
Data: 15/06/2020
Programador: Pedro Henrique Pires
Descrição: Incluindo método de buscar contrato e alterar status.
*/

/*
Data: 19/06/2020
Programador: Pedro Henrique Pires
Descrição: Incluindo método para não permitir contratar mais de uma vez a mesma agenda.
*/

/*
Data: 26/06/2020
Programador: Pedro Henrique Pires
Descrição: Método para realizar o atendimento.
*/

/*
Data: 26/06/2020
Programador: Tatiane
Descrição: Herdando da interface.
*/
#endregion
using System;
using System.Collections.Generic;
using ContratacaoNutricionistas.Domain.Entidades.Contrato;
using ContratacaoNutricionistas.Domain.Enumerados.Contrato;
using ContratacaoNutricionistas.Domain.Interfaces.Repository;

namespace ContratacaoNutricionistas.Domain.Interfaces.Contrato
{
    public interface IContratoRepository : IRepositoryBase
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

        /// <summary>
        /// Lista os constratos de algum usuário
        /// </summary>
        /// <param name="pIndiceInicial">Indice inicial</param>
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
        /// Altera o status do contrato
        /// </summary>
        /// <param name="pIdContrato">Contrato a ser alterado</param>
        /// <param name="pStatusContratoEnum">Novo status</param>
        void AlterarStatusContrato(int pIdContrato, StatusContratoEnum pStatusContratoEnum);

        /// <summary>
        /// Busca o contrato por ID
        /// </summary>
        /// <param name="pID">ID do contrato</param>
        /// <returns>Contrato ou null</returns>
        Entidades.Contrato.Contrato BuscaContratoPorID(int pID);

        /// <summary>
        /// Verifica se a agenda está disponível para contratar
        /// </summary>
        /// <param name="pIdAgenda">ID da agenda</param>
        /// <returns>Se a agenda está disponível ou não para contratar</returns>
        bool AgendaDisponivelParaContratar(int pIdAgenda);

        /// <summary>
        /// Realiza o atendimento
        /// </summary>
        /// <param name="idContrato">ID do contrato</param>
        /// <param name="mensagemAtendimento">Mensagem atendimento</param>
        void RealizarAtendimento(int idContrato, string mensagemAtendimento);
    }
}

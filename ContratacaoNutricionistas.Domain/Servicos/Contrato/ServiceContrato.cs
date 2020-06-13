#region Histórico de manutenções
/*
Data: 10/06/2020
Programador: Pedro Henrique Pires
Descrição: Implementação inicial.
*/

/*
Data: 13/06/2020
Programador: Pedro Henrique Pires
Descrição: Listando os contratos.
*/

#endregion
using ContratacaoNutricionistas.Domain.Entidades.Contrato;
using ContratacaoNutricionistas.Domain.Interfaces.Contrato;
using ModulosHelper.Extensions;
using System;
using System.Collections.Generic;

namespace ContratacaoNutricionistas.Domain.Servicos.Contrato
{
    public class ServiceContrato : IServiceContrato
    {
        #region Construtores
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="ContratoRepository">Classe de contrato repository</param>
        public ServiceContrato(IContratoRepository ContratoRepository)
        {
            _ContratoRepository = ContratoRepository;
        }
        #endregion

        #region Propriedades
        /// <summary>
        /// Classe de comandos no BD para contrato
        /// </summary>
        private readonly IContratoRepository _ContratoRepository;
        #endregion


        #region Métodos
        /// <summary>
        /// Contrata um nutricionista
        /// </summary>
        /// <param name="pContrato">Contrato a ser incluido</param>
        public void ContratarNutricionista(Entidades.Contrato.Contrato pContrato)
        {
            if (pContrato == null)
                throw new ArgumentException("Os dados do contrato são obrigatórios.");
            if (pContrato.DataInicio < Constantes.DateTimeNow())
                throw new ArgumentException("Data/hora de início não pode ser menor que o dia/hora atual");
            if (pContrato.DataTermino< Constantes.DateTimeNow())
                throw new ArgumentException("Data/hora de término não pode ser menor que o dia atual");
            if (pContrato.DataInicio > pContrato.DataTermino)
                throw new ArgumentException("A data de início não pode ser maior que a data de término");
            if (pContrato.DataInicio == pContrato.DataTermino && pContrato.DataInicio.TimeOfDay > pContrato.DataTermino.TimeOfDay)
                throw new ArgumentException("O horário de início não pode ser maior que o horário de término");
            if (pContrato.DataInicio == pContrato.DataTermino && pContrato.DataTermino.TimeOfDay == pContrato.DataTermino.TimeOfDay)
                throw new ArgumentException("O horário de início e término não podem ser iguais");

            if (pContrato.IdNutricionista <= 0)
                throw new ArgumentException("O nutricionista é obrigatório");
            if (pContrato.IdUsuario <= 0)
                throw new ArgumentException("O paciente é obrigatório");
            if (pContrato.Status != Enumerados.Contrato.StatusContratoEnum.PendenteAceitacaoNutricionista)
                throw new ArgumentException($"O status do cadastrado do contrato deve ser {Enumerados.Contrato.StatusContratoEnum.PendenteAceitacaoNutricionista.GetDescription()}");

            if (_ContratoRepository.VerificarContratoExistenteNaData(pContrato.DataInicio, pContrato.DataTermino, pContrato.IdUsuario))
                throw new Exception($"Você já possui uma consulta entre o dia/horário {pContrato.DataInicio.ToString(Constantes.MascaraDataHora)} até {pContrato.DataTermino.ToString(Constantes.MascaraDataHora)}. Não foi possível realizar o agendamento.");

            _ContratoRepository.ContratarNutricionista(pContrato);
        }

        public List<Entidades.Contrato.Contrato> ListaContratos(string pRua, string pCidade, string pBairro, string pCEP, string pUF, DateTime pDataInicio, DateTime pDataFim, int pIdUsuario)
        {
            return _ContratoRepository.ListaContratos(pRua, pCidade, pBairro, pCEP, pUF, pDataInicio, pDataFim, pIdUsuario);
        }
        #endregion

    }
}

#region Histórico de manutenções
/*
 Data: 06/05/2020
 Programador: Pedro Henrique Pires
 Descrição: Implementação Inicial da classe.
 */
#endregion
using ContratacaoNutricionistas.Domain.Enumerados.Agenda;
using DataBaseHelper.Atributos;
using System;

namespace ContratacaoNutricionistas.Domain.Entidades.Agenda
{
    [Tabela(pNomeTabela:"AGENDA_TB")]
    public class Agenda
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="idUsuario">Id do usuário</param>
        /// <param name="idEndereco">Id do endereço</param>
        /// <param name="dataInicio">Data de início</param>
        /// <param name="dataFim">Data fim</param>
        public Agenda(int idUsuario, int idEndereco, DateTime dataInicio, DateTime dataFim)
        {
            IdUsuario = idUsuario;
            IdEndereco = idEndereco;
            DataInicio = dataInicio;
            DataFim = dataFim;
        }

        public int IdAgenda { get; set; }

        [Coluna(pNomeColuna:"ID_USUARIO",pTipoDadosBanco:DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int IdUsuario { get; set; }

        [Coluna(pNomeColuna:"ID_ENDERECO",pTipoDadosBanco:DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int IdEndereco { get; set; }

        [Coluna(pNomeColuna: "DT_INICIO", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Datetime)]
        public DateTime DataInicio { get; set; }

        [Coluna(pNomeColuna: "DT_FIM", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Datetime)]
        public DateTime DataFim { get; set; }

        [Coluna(pNomeColuna: "AGENDA_STATUS",pTipoDadosBanco:DataBaseHelper.Enumerados.TipoDadosBanco.Enum, pTamanhoCampo:2)]
        public StatusAgendaEnum StatusAgenda { get; set; }
    }
}

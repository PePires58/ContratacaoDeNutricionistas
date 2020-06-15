#region Histórico de manutenções
/*
Data: 10/06/2020
Programador: Pedro Henrique Pires
Descrição: Implementação inicial.
*/

/*
Data: 13/06/2020
Programador: Pedro Henrique Pires
Descrição: Critica UF.
*/

/*
Data: 13/06/2020
Programador: Pedro Henrique Pires
Descrição: Incluindo ID do contrato.
*/
#endregion
using ContratacaoNutricionistas.Domain.Enumerados.Contrato;
using ContratacaoNutricionistas.Domain.Enumerados.Gerais;
using DataBaseHelper.Atributos;
using System;

namespace ContratacaoNutricionistas.Domain.Entidades.Contrato
{
    [Tabela(pNomeTabela: "CONTRATO_TB")]
    public class Contrato
    {
        public Contrato(int pIdUsuario, int pIdNutricionista, string pLogradouro, string complemento, uint? numero, string pBairro, string pCidade, UnidadeFederacaoEnum pUF, string pCEP, DateTime dataInicio, DateTime dataTermino, StatusContratoEnum status)
        {
            ValidarDados(pIdUsuario, pIdNutricionista, pLogradouro, pCidade, pBairro, pCEP, pUF, dataInicio, dataTermino);
            IdUsuario = pIdUsuario;
            IdNutricionista = pIdNutricionista;
            Logradouro = pLogradouro;
            Complemento = complemento;
            Numero = numero;
            Bairro = pBairro;
            Cidade = pCidade;
            UF = pUF;
            CEP = pCEP;
            DataInicio = dataInicio;
            DataTermino = dataTermino;
            Status = status;
        }

        private void ValidarDados(int pIdUsuario, int pIdNutricionista, string pLogradouro, string pCidade, string pBairro, string pCEP, UnidadeFederacaoEnum pUF, DateTime dataInicio, DateTime dataTermino)
        {
            new Nutricionista.Endereco(pIdNutricionista,
                pLogradouro,
                pBairro,
                pCidade,
                pCEP,
                pUF
                );

            if (pIdUsuario <= 0)
                throw new ArgumentException("O paciente é obrigatório.");
            if (dataInicio > dataTermino)
                throw new Exception("Data de início não pode ser maior que data de término.");
            if (pUF == UnidadeFederacaoEnum.NaoDefinido)
                throw new ArgumentException("Unidade de federação é obrigatório.");

        }

        public int IdContrato { get; set; }

        [Coluna(pNomeColuna: "ID_USUARIO", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int IdUsuario { get; set; }

        [Coluna(pNomeColuna: "ID_NUTRI", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int IdNutricionista { get; set; }

        [Coluna(pNomeColuna: "RUA", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 100)]
        public string Logradouro { get; set; }

        [Coluna(pNomeColuna: "COMPLEMENTO", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 255)]
        public string Complemento { get; set; }

        [Coluna(pNomeColuna: "NUMERO", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public uint? Numero { get; set; }

        [Coluna(pNomeColuna: "BAIRRO", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 50)]
        public string Bairro { get; set; }

        [Coluna(pNomeColuna: "CIDADE", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 30)]
        public string Cidade { get; set; }

        [Coluna(pNomeColuna: "ESTADO", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 2)]
        public UnidadeFederacaoEnum UF { get; set; }

        [Coluna(pNomeColuna: "CEP", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 9)]
        public string CEP { get; set; }

        [Coluna(pNomeColuna: "DT_INICIO", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Datetime)]
        public DateTime DataInicio { get; set; }

        [Coluna(pNomeColuna: "DT_FIM", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Datetime)]
        public DateTime DataTermino { get; set; }

        [Coluna(pNomeColuna: "STATUS", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Enum, pTamanhoCampo: 2)]
        public StatusContratoEnum Status { get; set; }
    }
}

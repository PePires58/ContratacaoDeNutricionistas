#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de endereço
 */

/*
* Programador: Pedro Henrique Pires
* Data: 03/06/2020
* Implementação: Inclusão de atributos para insert
*/
#endregion

using ContratacaoNutricionistas.Domain.Enumerados.Gerais;
using DataBaseHelper.Atributos;
using System;
using System.Text.RegularExpressions;

namespace ContratacaoNutricionistas.Domain.Entidades.Nutricionista
{
    /// <summary>
    /// Classe de endereço de algum usuário
    /// </summary>
    [Tabela("ENDERECO_TB")]
    public sealed class Endereco
    {
        #region Construtores
        public Endereco(
           int pIdUsuario,
           string pLogradouro,
           string pBairro,
           string pCidade,
           string pCEP,
           UnidadeFederacaoEnum pUF)
        {
            ValidarDados(pIdUsuario,pLogradouro, pCidade, pBairro, pCEP);
            Bairro = pBairro;
            Logradouro = pLogradouro;
            Cidade = pCidade;
            UF = pUF;
            CEP = pCEP;
            IdUsuario = pIdUsuario;
        }

        public Endereco(
            int pIdUsuario,
            string pLogradouro,
            string pBairro,
            string pCidade,
            string pCEP,
            UnidadeFederacaoEnum pUF,
            uint pNumero) : this(pIdUsuario,pLogradouro, pBairro, pCidade, pCEP, pUF)
        {
            Numero = pNumero;
        }

        public Endereco(
            int pIdUsuario,
            string pLogradouro,
            string pBairro,
            string pCidade,
            string pCEP,
            UnidadeFederacaoEnum pUF,
            uint pNumero,
            string pComplemento) : this(pIdUsuario,pLogradouro, pBairro, pCidade, pCEP, pUF, pNumero)
        {
            Complemento = pComplemento;
        }
        #endregion

        #region Propriedades

        public int IdEndereco { get; set; }

        [Coluna(pNomeColuna:"ID_USUARIO",pTipoDadosBanco:DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int IdUsuario { get; set; }

        [Coluna(pNomeColuna:"BAIRRO",pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar,pTamanhoCampo:50)]
        public string Bairro { get; set; }

        [Coluna(pNomeColuna: "CIDADE", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 30)]
        public string Cidade { get; set; }

        [Coluna(pNomeColuna: "CEP", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 9)]
        public string CEP { get; set; }

        [Coluna(pNomeColuna: "COMPLEMENTO", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 255)]
        public string Complemento { get; set; }

        [Coluna(pNomeColuna: "RUA", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 100)]
        public string Logradouro { get; set; }

        [Coluna(pNomeColuna: "ESTADO", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 2)]
        public UnidadeFederacaoEnum UF { get; set; }

        [Coluna(pNomeColuna: "NUMERO", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public uint? Numero { get; set; }

        #endregion

        #region Métodos privados
        private void ValidarDados(int pIdUsuario,string pLogradouro, string pCidade, string pBairro, string pCEP)
        {
            if (pIdUsuario == 0)
                throw new ArgumentException("O ID do usuário é obrigatório");
            else if (pIdUsuario < 0)
                throw new Exception("Valor do ID do usuário inválido");
            if (string.IsNullOrEmpty(pLogradouro))
                throw new ArgumentException($"O {nameof(Logradouro)} é obrigatório.");
            if (string.IsNullOrEmpty(pBairro))
                throw new ArgumentException($"O {nameof(Bairro)} é obrigatório.");
            if (string.IsNullOrEmpty(pCEP))
                throw new ArgumentException($"O {nameof(CEP)} é obrigatório.");
            if (!new Regex(@"^\d{5}\-\d{3}$").IsMatch(pCEP))
                throw new Exception($"O {nameof(CEP)} deve estar no formato 99999-999");
            if (string.IsNullOrEmpty(pCidade))
                throw new ArgumentException($"A {nameof(Cidade)} é obrigatória.");
        }
        #endregion
    }
}

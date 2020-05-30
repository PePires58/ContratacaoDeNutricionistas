#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de endereço
 */
#endregion

using ContratacaoNutricionistas.Domain.Enumerados.Gerais;
using System;
using System.Text.RegularExpressions;

namespace ContratacaoNutricionistas.Domain.Entidades.Nutricionista
{
    public sealed class Endereco
    {
        #region Construtores
        public Endereco(
           string pLogradouro,
           string pBairro,
           string pCidade,
           string pCEP,
           UnidadeFederacaoEnum pUF)
        {
            ValidarDados(pLogradouro, pCidade, pBairro, pCEP);
            Bairro = pBairro;
            Logradouro = pLogradouro;
            Cidade = pCidade;
            UF = pUF;
            CEP = pCEP;
        }

        public Endereco(
            string pLogradouro,
            string pBairro,
            string pCidade,
            string pCEP,
            UnidadeFederacaoEnum pUF,
            uint pNumero) : this(pLogradouro, pBairro, pCidade, pCEP, pUF)
        {
            Numero = pNumero;
        }

        public Endereco(string pLogradouro,
            string pBairro,
            string pCidade,
            string pCEP,
            UnidadeFederacaoEnum pUF,
            uint pNumero,
            string pComplemento) : this(pLogradouro, pBairro, pCidade, pCEP, pUF, pNumero)
        {
            Complemento = pComplemento;
        }
        #endregion

        #region Propriedades
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }
        public string Complemento { get; set; }
        public string Logradouro { get; set; }
        public UnidadeFederacaoEnum UF { get; set; }
        public uint? Numero { get; set; }

        #endregion

        #region Métodos privados
        private void ValidarDados(string pLogradouro, string pCidade, string pBairro, string pCEP)
        {
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

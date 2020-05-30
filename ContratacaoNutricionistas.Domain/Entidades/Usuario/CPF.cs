#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de CPF
 */

/*
* Programador: Pedro Henrique Pires
* Data: 30/05/2020
* Implementação: Ajuste no namespace.
*/
#endregion

using System;
using System.Text.RegularExpressions;

namespace ContratacaoNutricionistas.Domain.Entidades.Usuario
{
    public sealed class CPF
    {
        #region Construtor
        public CPF(string pNumero, bool pSemPontuacao)
        {
            ValidarCPF(pNumero, pSemPontuacao);
            Numero = pNumero;
        }
        #endregion

        #region Propriedades

        public string Numero { get; set; }
        #endregion

        #region Métodos privados
        private void ValidarCPF(string pNumero, bool pSemPontuacao)
        {
            if (string.IsNullOrEmpty(pNumero))
                throw new ArgumentException(message: "O número não pode ser vázio.");
            if (pNumero.Length != 11 && pSemPontuacao)
                throw new ArgumentException("O número do CPF deve ter 11 caracteres.");
            if (pNumero.Length != 14 && !pSemPontuacao)
                throw new ArgumentException("O número do CPF deve ter 14 caracteres, incluindo a pontuação.");
            if (!pSemPontuacao)
            {
                Regex regex = new Regex(pattern: @"^\d{3}\.\d{3}\.\d{3}\-\d{2}$");
                if (!regex.IsMatch(pNumero))
                    throw new ArgumentException("O número do CPF deve estar no formato 999.999.999-99.");
            }
            else if (!long.TryParse(pNumero, out _))
                throw new ArgumentException("O número do CPF deve estar no formato 99999999999.");

            string CPF = pSemPontuacao ? pNumero : pNumero.Replace(".", "").Replace("-", "");
            string novePrimeirosDigitos = CPF.Substring(0, 9);
            string dezPrimeirosDigitos = CPF.Substring(0, 10);
            string digitoVerificador = CPF.Substring(9, 2);
          
            int primeiroDigito = RetornaDigitoParaVerificar(novePrimeirosDigitos, 10);
            int segundoDigito = RetornaDigitoParaVerificar(dezPrimeirosDigitos, 11);

            if (!digitoVerificador.StartsWith(primeiroDigito.ToString()) || !digitoVerificador.EndsWith(segundoDigito.ToString()))
                throw new Exception("O CPF é inválido");
        }

        private int RetornaDigitoParaVerificar(string pDigitos, int multiplicador)
        {
            int resultadoSomaPrimeiroDigito = 0;
            for (int i = 0; i < pDigitos.Length; i++)
            {
                int digito = Convert.ToInt32(pDigitos[i].ToString());
                resultadoSomaPrimeiroDigito += digito * multiplicador--;
            }

            return (resultadoSomaPrimeiroDigito % 11) < 2 ? 0 : 11 - resultadoSomaPrimeiroDigito % 11;
        }
        #endregion
    }
}
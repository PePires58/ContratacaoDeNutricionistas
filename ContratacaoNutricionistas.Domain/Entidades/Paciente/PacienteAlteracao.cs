#region Histórico de manutenção
 /*
 * Programador: Pedro Henrique Pires
 * Data: 01/06/2020
 * Implementação: Implementação inicial.
 */
#endregion
using ContratacaoNutricionistas.Domain.Entidades.Usuario;
using ContratacaoNutricionistas.Domain.Enumerados.Usuario;
using DataBaseHelper.Atributos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContratacaoNutricionistas.Domain.Entidades.Paciente
{
    [Tabela(pNomeTabela: "USUARIO_TB")]
    public class PacienteAlteracao : Usuario.UsuarioAlteracao
    {
        public PacienteAlteracao(int pID, string pNome, string pTelefone, string pLogin, string pSenha, CPF pCPF)
        {
            ValidarDados(pID, pNome, pTelefone, pLogin, pSenha, pCPF);
            Nome = pNome;
            Telefone = pTelefone;
            Login = pLogin;
            Senha = pSenha;
            CpfObjeto = pCPF;
        }

        /// <summary>
        /// ID do paciente
        /// </summary>
        [Coluna(pNomeColuna: "ID_USUARIO", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public override int ID { get; set; }

        /// <summary>
        /// Nome do paciente
        /// </summary>
        [Coluna(pNomeColuna: "NOME", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 50)]
        public override string Nome { get; set; }

        /// <summary>
        /// Telefone
        /// </summary>
        [Coluna(pNomeColuna: "TELEFONE", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 15)]
        public override string Telefone { get; set; }

        /// <summary>
        /// Login
        /// </summary>
        [Coluna(pNomeColuna: "LOGIN", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 20)]
        public override string Login { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        [Coluna(pNomeColuna: "SENHA", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 8)]
        public override string Senha { get; set; }

        /// <summary>
        /// CPF ou CNPJ
        /// </summary>
        [Coluna(pNomeColuna: "CPF", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 18)]
        public string CPF { get { return CpfObjeto.Numero; } set { CpfObjeto.Numero = value; } }

        /// <summary>
        /// Tipo de usuário
        /// </summary>
        [Coluna(pNomeColuna: "TP_USUARIO", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Enum)]
        public override TipoUsuarioEnum TipoUsuario => TipoUsuarioEnum.Paciente;

        /// <summary>
        /// Objeto de CPF
        /// </summary>
        public override CPF CpfObjeto { get; set; }

        private void ValidarDados(int pID,string pNome, string pTelefone, string pLogin, string pSenha, CPF pCPF)
        {
            if (pID == 0)
                throw new ArgumentException($"O {nameof(ID)} é obrigatório.");
            else if (pID < 0)
                throw new ArgumentException("O valor do ID é inválido");
            if (string.IsNullOrEmpty(pNome))
                throw new ArgumentException($"O {nameof(Nome)} é obrigatório.");
            if (string.IsNullOrEmpty(pTelefone))
                throw new ArgumentException($"O {nameof(Telefone)} é obrigatório.");
            if (string.IsNullOrEmpty(pLogin))
                throw new ArgumentException($"O {nameof(Login)} é obrigatório.");
            if (string.IsNullOrEmpty(pSenha))
                throw new ArgumentException($"A {nameof(Senha)} é obrigatória.");
            if (pCPF == null)
                throw new ArgumentException($"O {nameof(CPF)} é obrigatório.");
        }
    }
}

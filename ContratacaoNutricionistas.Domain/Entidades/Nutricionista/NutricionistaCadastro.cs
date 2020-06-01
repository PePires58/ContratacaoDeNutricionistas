#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de alteração de nutricionista.
 */
#endregion
using ContratacaoNutricionistas.Domain.Entidades.Paciente.Usuario;
using ContratacaoNutricionistas.Domain.Entidades.Usuario;
using ContratacaoNutricionistas.Domain.Enumerados.Usuario;
using DataBaseHelper.Atributos;
using System;

namespace ContratacaoNutricionistas.Domain.Entidades.Nutricionista
{
    public class NutricionistaCadastro : UsuarioCadastroAlteracao
    {
        public NutricionistaCadastro(string pNome, string pTelefone, string pCRM, string pLogin, string pSenha, CPF pCPF)
        {
            ValidarDados(pNome, pCRM, pTelefone, pLogin, pSenha, pCPF);
            Nome = pNome;
            Telefone = pTelefone;
            Login = pLogin;
            Senha = pSenha;
            CpfObjeto = pCPF;
        }

        /// <summary>
        /// Nome do nutricionista
        /// </summary>
        [Coluna(pNomeColuna: "NOME", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 50)]
        public override string Nome { get; set; }

        /// <summary>
        /// CRM do nutricionista
        /// </summary>
        [Coluna(pNomeColuna:"CRM",pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo:50)]
        public string CRM { get; set; }

        /// <summary>
        /// Telefone
        /// </summary>
        [Coluna(pNomeColuna: "TELEFONE", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 12)]
        public override string Telefone { get; set; }

        /// <summary>
        /// Login
        /// </summary>
        [Coluna(pNomeColuna: "LOGIN", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 20)]
        public override string Login { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        [Coluna(pNomeColuna: "SENHA", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 20)]
        public override string Senha { get; set; }

        /// <summary>
        /// CPF ou CNPJ
        /// </summary>
        [Coluna(pNomeColuna: "CPF", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 18)]
        private string CPF { get { return CpfObjeto.Numero; } set { CpfObjeto.Numero = value; } }

        /// <summary>
        /// Tipo de usuário
        /// </summary>
        [Coluna(pNomeColuna: "TP_USUARIO", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 20)]
        public override TipoUsuarioEnum TipoUsuario => TipoUsuarioEnum.Nutricionista;

        /// <summary>
        /// Objeto de CPF
        /// </summary>
        public override CPF CpfObjeto { get; set; }

        private void ValidarDados(string pNome,string pCRM, string pTelefone, string pLogin, string pSenha, CPF pCPF)
        {
            if (!string.IsNullOrEmpty(pNome))
                throw new ArgumentException($"O {nameof(Nome)} é obrigatório.");
            if (!string.IsNullOrEmpty(pTelefone))
                throw new ArgumentException($"O {nameof(Telefone)} é obrigatório.");
            if (!string.IsNullOrEmpty(pLogin))
                throw new ArgumentException($"O {nameof(Login)} é obrigatório.");
            if (!string.IsNullOrEmpty(pSenha))
                throw new ArgumentException($"A {nameof(Senha)} é obrigatória.");
            if (pCPF == null)
                throw new ArgumentException($"O {nameof(CPF)} é obrigatório.");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ContratacaoNutricionistas.Domain.Enumerados.Usuario;
using DataBaseHelper.Atributos;

namespace ContratacaoNutricionistas.Domain.Entidades.Paciente
{
    /// <summary>
    /// Nome da tabela de cadastro de paciente
    /// </summary>
    [Tabela(pNomeTabela:"PACIENTE_TB", pTemporaria:false)]
    public class PacienteCadastro : Usuario.UsuarioCadastro
    {
        /// <summary>
        /// Tipo de pessoa
        /// </summary>
        [Coluna(pNomeColuna:"TP_PESSOA", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Enum)]
        public override TipoPessoaEnum TipoPessoa { get; set ; }

        /// <summary>
        /// Nome do paciente
        /// </summary>
        [Coluna(pNomeColuna: "NOME", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo:50)]
        public override string Nome { get; set; }

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
        [Coluna(pNomeColuna:"CPFCNPJ", pTipoDadosBanco:DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo:18)]
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Tipo de usuário
        /// </summary>
        [Coluna(pNomeColuna: "TP_USUARIO", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 20)]
        public override TipoUsuarioEnum TipoUsuario { get; set; }
    }
}

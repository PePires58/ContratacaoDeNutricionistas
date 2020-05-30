#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de cadastro ou alteração de usuários
 */
#endregion

namespace ContratacaoNutricionistas.Domain.Entidades.Paciente.Usuario
{
    /// <summary>
    /// Classe abstrata para cadastro e alteração
    /// </summary>
    public abstract class UsuarioCadastroAlteracao : UsuarioLogin
    {
        /// <summary>
        /// Nome
        /// </summary>
        public abstract string Nome { get; set; }

        /// <summary>
        /// Telefone
        /// </summary>
        public abstract string Telefone { get; set; }

        /// <summary>
        /// Objeto de cpf
        /// </summary>
        public abstract CPF CpfObjeto { get; set; }
    }
}

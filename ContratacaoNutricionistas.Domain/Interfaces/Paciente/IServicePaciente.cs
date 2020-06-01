#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da Interface que faz os serviços para o Paciente
 */

  /*
 * Programador: Pedro Henrique Pires
 * Data: 01/06/2020
 * Implementação: Inclusão de métodos de alteração e consulta.
 */
#endregion

using ContratacaoNutricionistas.Domain.Entidades.Paciente;

namespace ContratacaoNutricionistas.Domain.Interfaces.Paciente
{
    /// <summary>
    /// Interface que define os métodos da classe responsável pela regra de negócio de pacientes
    /// </summary>
    public interface IServicePaciente
    {
        /// <summary>
        /// Cadastro o paciente
        /// </summary>
        /// <param name="pModel">Modelo de cadastro</param>
        void Cadastra(PacienteCadastro pModel);

        /// <summary>
        /// Retorna um paciente para alteração pelo ID
        /// </summary>
        /// <param name="pID">ID do paciente</param>
        /// <returns>Paciente ou NULL</returns>
        PacienteAlteracao ConsultarPacientePorID(int pID);

        /// <summary>
        /// Altera os dados do paciente
        /// </summary>
        /// <param name="pacienteAlteracao">Paciente a ser alterado</param>
        void AlterarDados(PacienteAlteracao pacienteAlteracao);
    }
}

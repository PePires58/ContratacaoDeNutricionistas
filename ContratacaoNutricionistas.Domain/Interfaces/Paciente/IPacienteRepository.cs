#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da interface do paciente repository
 */

  /*
 * Programador: Pedro Henrique Pires
 * Data: 01/06/2020
 * Implementação: Inclusão de método de alteração e consulta.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using ContratacaoNutricionistas.Domain.Entidades.Paciente;

namespace ContratacaoNutricionistas.Domain.Interfaces.Paciente
{
    /// <summary>
    /// Interface da classe que ira conectar e fazer comando com o banco
    /// </summary>
    public interface IPacienteRepository
    {
        /// <summary>
        /// Método que cadastra um paciente no banco
        /// </summary>
        /// <param name="pPacienteCadastro">Paciente a ser cadastrado</param>
        void CadastrarPaciente(Entidades.Paciente.PacienteCadastro pPacienteCadastro);

        /// <summary>
        /// Método que realiza a alteração dos dados do paciente
        /// </summary>
        /// <param name="pacienteAlteracao">Paciente a ser alterado</param>
        void AlterarDados(PacienteAlteracao pacienteAlteracao);

        /// <summary>
        /// Retorna um paciente para alteração
        /// </summary>
        /// <param name="pID">ID do paciente</param>
        /// <returns>Paciente para alteração ou NULL</returns>
        PacienteAlteracao ConsultarPacientePorID(int pID);
    }
}

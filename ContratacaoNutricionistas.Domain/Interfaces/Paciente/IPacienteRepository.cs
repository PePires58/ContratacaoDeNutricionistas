#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da interface do paciente repository
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;

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
        /// Método que verifica se um paciente já está cadastrado (Login já existe)
        /// </summary>
        /// <param name="pLogin">Login</param>
        /// <returns>Se o login está disponível ou não</returns>
        bool LoginExiste(string pLogin);
    }
}

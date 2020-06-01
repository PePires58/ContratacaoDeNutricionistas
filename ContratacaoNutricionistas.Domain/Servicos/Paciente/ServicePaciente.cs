#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de serviço de paciente
 */

  /*
 * Programador: Pedro Henrique Pires
 * Data: 01/06/2020
 * Implementação: Implementação dos métodos de alteração e consulta.
 */
#endregion

using ContratacaoNutricionistas.Domain.Entidades.Paciente;
using ContratacaoNutricionistas.Domain.Interfaces.Paciente;
using ContratacaoNutricionistas.Domain.Interfaces.Usuario;
using DataBaseHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContratacaoNutricionistas.Domain.Servicos.Paciente
{
    /// <summary>
    /// Essa classe contém as regras quando se trata de paciente
    /// </summary>
    public class ServicePaciente : IServicePaciente
    {
        #region Propriedades
        /// <summary>
        /// Interface que faz os comandos com o banco de dados para paciente
        /// </summary>
        private readonly IPacienteRepository _PacienteRepository;

        /// <summary>
        /// Interface dos serviços do paciente
        /// </summary>
        private readonly IServiceUsuario _ServiceUsuario;
        #endregion

        #region Construtor
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="pPacienteRepository">Interface que faz os comandos com o banco de dados para paciente</param>
        public ServicePaciente(IPacienteRepository pPacienteRepository, IServiceUsuario pServiceUsuario)
        {
            _PacienteRepository = pPacienteRepository;
            _ServiceUsuario = pServiceUsuario;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Cadastro o paciente
        /// </summary>
        /// <param name="pModel"></param>
        public void Cadastra(PacienteCadastro pModel)
        {
            if (!_ServiceUsuario.LoginExiste(pModel.Login))
                _PacienteRepository.CadastrarPaciente(pModel);
            else
                throw new Exception($"O login: { pModel }, já existe!");
        }

        /// <summary>
        /// Método que consulta um paciente pelo ID
        /// </summary>
        /// <param name="pID">ID do paciente</param>
        /// <returns>Paciente para a alteração ou NULL</returns>
        public PacienteAlteracao ConsultarPacientePorID(int pID)
        {
            if (pID == 0)
                throw new ArgumentException("O ID do paciente é obrigatório");
            else if (pID < 0)
                throw new ArgumentException("Valor do ID é inválido");

            return _PacienteRepository.ConsultarPacientePorID(pID);
        }


        /// <summary>
        /// Altera os dados do paciente
        /// </summary>
        /// <param name="pacienteAlteracao">Paciente a ser alterado</param>
        public void AlterarDados(PacienteAlteracao pacienteAlteracao)
        {
            if (pacienteAlteracao == null)
                throw new ArgumentException("Os dados do paciente devem ser preenchidos");
            _PacienteRepository.AlterarDados(pacienteAlteracao);
        }

        #endregion

    }
}

#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de serviço de paciente
 */
#endregion

using ContratacaoNutricionistas.Domain.Entidades.Paciente;
using ContratacaoNutricionistas.Domain.Interfaces.Paciente;
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
        /// Unidade de conexão e execução com banco de dados
        /// </summary>
        private readonly IPacienteRepository _PacienteRepository;
        #endregion

        #region Construtor
        /// <summary>
        /// Unidade de trabalho
        /// </summary>
        /// <param name="pPacienteRepository"></param>
        public ServicePaciente(IPacienteRepository pPacienteRepository)
        {
            _PacienteRepository = pPacienteRepository;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Cadastro o paciente
        /// </summary>
        /// <param name="pModel"></param>
        public void Cadastra(PacienteCadastro pModel)
        {
            if (!LoginExistente(pModel.Login))
                _PacienteRepository.CadastrarPaciente(pModel);
            else
                throw new Exception($"O login: { pModel }, já existe!");
        }

        /*Esse método será migrado para um serviço específico. 
         * Pois o login é tanto para paciente quanto para nutricionista*/
        public bool LoginExistente(string pLogin)
        {
            if (string.IsNullOrEmpty(pLogin))
                throw new ArgumentException(message: "O login é obrigatório para verificar a existência");

            return _PacienteRepository.LoginExiste(pLogin);
        }


        #endregion

    }
}

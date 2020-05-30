using ContratacaoNutricionistas.Domain.Entidades.Nutricionista;
using ContratacaoNutricionistas.Domain.Interfaces.Nutricionista;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContratacaoNutricionistas.Domain.Servicos.Nutricionista
{
    public class ServiceNutricionista : IServiceNutricionista
    {
        #region Propriedades
        /// <summary>
        /// Interface que faz os comandos com o banco de dados para nutricionista
        /// </summary>
        private readonly INutricionistaRepository _PacienteRepository;
        #endregion

        #region Construtor
        /// <summary>
        /// Construtor da classe de serviço
        /// </summary>
        /// <param name="pPacienteRepository">Interface que faz os comandos com o banco de dados para nutricionista</param>
        public ServiceNutricionista(INutricionistaRepository pPacienteRepository)
        {
            _PacienteRepository = pPacienteRepository;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que cadastra um nutricionista
        /// </summary>
        /// <param name="pNutricionistaCadastro">Nutricionista a ser cadastrado</param>
        public void CadastrarNutricionista(NutricionistaCadastro pNutricionistaCadastro)
        {
            _PacienteRepository.CadastrarNutricionista(pNutricionistaCadastro);
        }
        #endregion

    }
}

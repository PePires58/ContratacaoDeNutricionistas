#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de serviço de nutricionista
 */
#endregion
using ContratacaoNutricionistas.Domain.Entidades.Nutricionista;
using ContratacaoNutricionistas.Domain.Interfaces.Nutricionista;
using System;

namespace ContratacaoNutricionistas.Domain.Servicos.Nutricionista
{
    public class ServiceNutricionista : IServiceNutricionista
    {
        #region Propriedades
        /// <summary>
        /// Interface que faz os comandos com o banco de dados para nutricionista
        /// </summary>
        private readonly INutricionistaRepository _NutricionistaRepository;
        #endregion

        #region Construtor
        /// <summary>
        /// Construtor da classe de serviço
        /// </summary>
        /// <param name="pPacienteRepository">Interface que faz os comandos com o banco de dados para nutricionista</param>
        public ServiceNutricionista(INutricionistaRepository pPacienteRepository)
        {
            _NutricionistaRepository = pPacienteRepository;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que cadastra um nutricionista
        /// </summary>
        /// <param name="pNutricionistaCadastro">Nutricionista a ser cadastrado</param>
        public void CadastrarNutricionista(NutricionistaCadastro pNutricionistaCadastro)
        {
            _NutricionistaRepository.CadastrarNutricionista(pNutricionistaCadastro);
        }

        /// <summary>
        /// Consulta o nutricionista pelo ID
        /// </summary>
        /// <param name="pID">ID</param>
        /// <returns>Nutricionista ou null</returns>
        public NutricionistaAlteracao ConsultarNutricionistaPorID(int pID)
        {
            if (pID == 0)
                throw new ArgumentException("O ID é obrigatório.");
            else if (pID < 0)
                throw new ArgumentException("O valor do ID é inválido");

            return _NutricionistaRepository.ConsultarNutricionistaPorID(pID);
        }

        /// <summary>
        /// Altera os dados do nutricionista
        /// </summary>
        /// <param name="pNutricionistaAlteracao">Nutricionista para ser alterado</param>
        public void AlterarDadosNutricionista(NutricionistaAlteracao pNutricionistaAlteracao)
        {
            if (pNutricionistaAlteracao == null)
                throw new ArgumentException("O nutricionista deve ser preenchido");
            _NutricionistaRepository.AlterarDadosNutricionista(pNutricionistaAlteracao);
        }
        #endregion

    }
}

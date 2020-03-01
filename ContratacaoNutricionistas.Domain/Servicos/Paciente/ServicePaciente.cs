using ContratacaoNutricionistas.Domain.Entidades.Paciente;
using ContratacaoNutricionistas.Domain.Interfaces.Paciente;
using DataBaseHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContratacaoNutricionistas.Domain.Servicos.Paciente
{
    public class ServicePaciente : IServicePaciente
    {
        #region Propriedades
        /// <summary>
        /// Unidade de conexão e execução com banco de dados
        /// </summary>
        private readonly IUnitOfWork _UnitOfWork;
        #endregion

        #region Construtor
        /// <summary>
        /// Unidade de trabalho
        /// </summary>
        /// <param name="pIUnitOfWork"></param>
        public ServicePaciente(IUnitOfWork pIUnitOfWork)
        {
            _UnitOfWork = pIUnitOfWork;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Cadastro o paciente
        /// </summary>
        /// <param name="pModel"></param>
        public void Cadastra(PacienteCadastro pModel)
        {
            _UnitOfWork.Executar(_UnitOfWork.MontaInsertPorAttributo(pModel).ToString());
        }

        public bool LoginExistente(string pLogin)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}

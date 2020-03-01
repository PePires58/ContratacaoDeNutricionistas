using ContratacaoNutricionistas.Domain.Entidades.Paciente;
using System;
using System.Collections.Generic;
using System.Text;

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
        /// Verifica se o login existe
        /// </summary>
        /// <param name="pLogin">Login existe</param>
        /// <returns></returns>
        bool LoginExistente(string pLogin);
    }
}

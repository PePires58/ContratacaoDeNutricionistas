#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da Interface que faz os serviços para o Paciente
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
        /// Verifica se o login existe
        /// </summary>
        /// <param name="pLogin">Login existe</param>
        /// <returns></returns>
        bool LoginExistente(string pLogin);
    }
}

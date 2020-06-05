#region Histórico de manutenções
/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Implementação inicial
*/
#endregion
using ContratacaoNutricionistas.Domain.Enumerados.Gerais;
using ContratacaoNutricionistas.Domain.Interfaces.Endereco;
using Correios.NET.Models;
using ModulosHelper.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContratacaoNutricionistas.Domain.Servicos.Endereco
{
    /// <summary>
    /// Serviço para endereços
    /// </summary>
    public class ServiceEndereco : IServiceEndereco
    {

        #region Propriedades
        /// <summary>
        /// Interface que faz os comandos com o banco de dados para nutricionista
        /// </summary>
        private readonly IEnderecoRepository _EnderecoRepository;
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="EnderecoRepository">Serviço de comandos com o banco</param>
        public ServiceEndereco(IEnderecoRepository EnderecoRepository)
        {
            _EnderecoRepository = EnderecoRepository;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Cadastra um endereço
        /// </summary>
        /// <param name="pEndereco">Endereço a ser cadastrado</param>
        public void CadastrarEndereco(Entidades.Nutricionista.Endereco pEndereco)
        {
            if (pEndereco == null)
                throw new ArgumentException("O endereço é obrigatório");

            if (string.IsNullOrEmpty(pEndereco.CEP))
                throw new ArgumentException("O CEP não pode ser nulo.");

            Address endereco = new Correios.NET.Services().GetAddresses(pEndereco.CEP.Replace("-", "")).FirstOrDefault();
            if (endereco == null)
                throw new Exception("CEP não localizado.");
            if (!endereco.Street.Equals(pEndereco.Logradouro))
                throw new Exception("Logradouro não corresponde com a cidade informada pelo CEP");
            if (!endereco.District.Equals(pEndereco.Bairro))
                throw new Exception("Cidade não corresponde com a cidade informada pelo CEP");
            if (!endereco.City.Equals(pEndereco.Cidade))
                throw new Exception("Cidade não corresponde com a cidade informada pelo CEP");
            if (!Enum.GetValues(typeof(UnidadeFederacaoEnum))
                    .Cast<UnidadeFederacaoEnum>()
                    .FirstOrDefault(c => c.GetDefaultValue().Equals(endereco.State)).GetDefaultValue().Equals(pEndereco.UF.GetDefaultValue()))
                throw new Exception("UF não corresponde com a cidade informada pelo CEP");

            _EnderecoRepository.CadastrarEndereco(pEndereco);
        }

        /// <summary>
        /// Retorna um endereço a partir do CEP
        /// </summary>
        /// <param name="pCEP">Número do CEP</param>
        /// <param name="pIdUsuario">ID do usuário</param>
        /// <returns>Endereço ou NULL quando não encontrado</returns>
        public Entidades.Nutricionista.Endereco ConsultarEnderecoPorCEP(string pCEP, int pIdUsuario)
        {
            if (string.IsNullOrEmpty(pCEP))
                throw new ArgumentException("O CEP não pode ser nulo.");

            Address endereco = new Correios.NET.Services().GetAddresses(pCEP.Replace("-", "")).FirstOrDefault();

            if (endereco == null)
                throw new Exception("CEP não localizado.");
            return new Entidades.Nutricionista.Endereco(
                pIdUsuario,
                pLogradouro: endereco.Street,
                pBairro: endereco.District,
                pCidade: endereco.City,
                pCEP: pCEP,
                pUF: Enum.GetValues(typeof(UnidadeFederacaoEnum))
                    .Cast<UnidadeFederacaoEnum>()
                    .FirstOrDefault(c => c.GetDefaultValue().Equals(endereco.State))
                );
        }

        /// <summary>
        /// Lista os endereços cadastrados pelo nutricionista
        /// </summary>
        /// <param name="pIdUsuario">ID do usuário</param>
        /// <param name="pRua">Rua</param>
        /// <param name="pCidade">Cidade</param>
        /// <param name="pBairro">Bairro</param>
        /// <param name="pCEP">CEP</param>
        /// <param name="pUF">UF</param>
        /// <returns>Lista de endereços</returns>
        public List<Entidades.Nutricionista.Endereco> EnderecosCadastrados(int pIdUsuario, string pRua, string pCidade, string pBairro, string pCEP, string pUF)
        {
            return _EnderecoRepository.EnderecosCadastrados(
                pIdUsuario,
                pRua,
                pCidade,
                pBairro,
                pCEP,
                pUF
                );
        }

        /// <summary>
        /// Consulta o endereço do nutricionista pelo ID
        /// </summary>
        /// <param name="pID">ID</param>
        /// <param name="pIDUsuario">Usuario logado</param>
        /// <returns>Endereço ou null</returns>
        public Entidades.Nutricionista.Endereco ConsultarEnderecoNutricionistaPorID(int pID, int pIDUsuario)
        {
            if (pID == 0)
                throw new ArgumentException("O ID do endereço é obrigatório.");
            else if (pID < 0)
                throw new ArgumentException("Valor do ID do endereço inválido.");
            if (pIDUsuario == 0)
                throw new ArgumentException("Id do usuário logado é inválido");

            return _EnderecoRepository.ConsultarEnderecoNutricionistaPorID(pID, pIDUsuario);
        }

        /// <summary>
        /// Altera os dados do endereço
        /// </summary>
        /// <param name="pEndereco"></param>
        public void AlterarDadosEndereco(Entidades.Nutricionista.Endereco pEndereco)
        {
            if (pEndereco == null)
                throw new ArgumentException("O endereço é obrigatório.");
            if (pEndereco.IdEndereco == 0)
                throw new ArgumentException("O id do endereço é obrigatório.");
            if (pEndereco.IdUsuario == 0)
                throw new ArgumentException("O id do usuário logado é obrigatório.");

            if (string.IsNullOrEmpty(pEndereco.CEP))
                throw new ArgumentException("O CEP não pode ser nulo.");

            Address endereco = new Correios.NET.Services().GetAddresses(pEndereco.CEP.Replace("-", "")).FirstOrDefault();
            if (endereco == null)
                throw new Exception("CEP não localizado.");
            if (!endereco.Street.Equals(pEndereco.Logradouro))
                throw new Exception("Logradouro não corresponde com a cidade informada pelo CEP");
            if (!endereco.District.Equals(pEndereco.Bairro))
                throw new Exception("Cidade não corresponde com a cidade informada pelo CEP");
            if (!endereco.City.Equals(pEndereco.Cidade))
                throw new Exception("Cidade não corresponde com a cidade informada pelo CEP");
            if (!Enum.GetValues(typeof(UnidadeFederacaoEnum))
                    .Cast<UnidadeFederacaoEnum>()
                    .FirstOrDefault(c => c.GetDefaultValue().Equals(endereco.State)).GetDefaultValue().Equals(pEndereco.UF.GetDefaultValue()))
                throw new Exception("UF não corresponde com a cidade informada pelo CEP");

            _EnderecoRepository.AlterarDadosEndereco(pEndereco);
        }
        #endregion
    }
}

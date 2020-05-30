#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial do controlador de nutricionista
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContratacaoNutricionistasWEB.Models.Nutricionista;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModulosHelper.Extensions;

namespace ContratacaoNutricionistasWEB.Controllers
{
    /// <summary>
    /// Controlador de nutricionista
    /// </summary>
    public class NutricionistaController : Controller
    {
        /// <summary>
        /// Lista de tipo de pessoa
        /// </summary>
        private List<SelectListItem> ListaTipoPessoa => new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Text = ContratacaoNutricionistas.Domain.Enumerados.Usuario.TipoPessoaEnum.NaoDefinido.GetDescription(),
                Value = ContratacaoNutricionistas.Domain.Enumerados.Usuario.TipoPessoaEnum.NaoDefinido.GetDefaultValue()
            },
            new SelectListItem()
            {
                Text = ContratacaoNutricionistas.Domain.Enumerados.Usuario.TipoPessoaEnum.Fisica.GetDescription(),
                Value = ContratacaoNutricionistas.Domain.Enumerados.Usuario.TipoPessoaEnum.Fisica.GetDefaultValue()
            }
        };

        /// <summary>
        /// Essa tela retorna a página de Cadastro.cshtml da pasta Nutricionista
        /// </summary>
        /// <returns>Cadastro.cshtml da pasta Nutricionista</returns>
        [HttpGet]
        public IActionResult Cadastro()
        {
            /*Lista de tipo de pessoas, passada para montar o combo em tela*/
            ViewData[Constantes.ViewDataListaTipoPessoa] = ListaTipoPessoa;

            return View(new NutricionistaCadastroVM());
        }
    }
}
#region Histórico de manutenção
/*
 * Programador: Pedro Henrique Pires
 * Data: 30/05/2020
 * Implementação: Implementação Inicial da classe de configuração do site
 */
/*
* Programador: Pedro Henrique Pires
* Data: 30/05/2020
* Implementação: Inclusão de classes de serviço e repositório de nutricionista.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContratacaoNutricionistas.Domain.Interfaces.Nutricionista;
using ContratacaoNutricionistas.Domain.Interfaces.Paciente;
using ContratacaoNutricionistas.Domain.Repository.Nutricionista;
using ContratacaoNutricionistas.Domain.Repository.Paciente;
using ContratacaoNutricionistas.Domain.Servicos.Nutricionista;
using ContratacaoNutricionistas.Domain.Servicos.Paciente;
using DataBaseHelper;
using DataBaseHelper.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ContratacaoNutricionistasWEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region Injeção de depencia
            services.AddSingleton<IUnitOfWork>(new UnitOfWork(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IServicePaciente, ServicePaciente>();
            services.AddSingleton<IPacienteRepository, PacienteRepository>();
            services.AddSingleton<IServiceNutricionista, ServiceNutricionista>();
            services.AddSingleton<INutricionistaRepository, NutricionistaRepository>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

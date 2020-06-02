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

/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Inclusão de classe de serviço e repositório para usuário.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Implementação de restrição de usuários logados.
*/
#endregion

using ContratacaoNutricionistas.Domain.Interfaces.Nutricionista;
using ContratacaoNutricionistas.Domain.Interfaces.Paciente;
using ContratacaoNutricionistas.Domain.Interfaces.Usuario;
using ContratacaoNutricionistas.Domain.Repository.Nutricionista;
using ContratacaoNutricionistas.Domain.Repository.Paciente;
using ContratacaoNutricionistas.Domain.Repository.Usuario;
using ContratacaoNutricionistas.Domain.Servicos.Nutricionista;
using ContratacaoNutricionistas.Domain.Servicos.Paciente;
using ContratacaoNutricionistas.Domain.Servicos.Usuario;
using DataBaseHelper;
using DataBaseHelper.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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


            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", config =>
                {
                    config.Cookie.Name = "Authentication.Cookie";
                    config.LoginPath = "/Login/Index";
                    config.AccessDeniedPath = "/Home/Index";
                });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("Nutricionista", politica =>
                {
                    politica.RequireClaim("Nutricionista");
                });

                option.AddPolicy("Paciente", paciente =>
                {
                    paciente.RequireClaim("Paciente");
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region Injeção de depencia
            services.AddSingleton<IUnitOfWork>(new UnitOfWork(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IServicePaciente, ServicePaciente>();
            services.AddSingleton<IPacienteRepository, PacienteRepository>();
            services.AddSingleton<IServiceNutricionista, ServiceNutricionista>();
            services.AddSingleton<INutricionistaRepository, NutricionistaRepository>();
            services.AddSingleton<IServiceUsuario, ServiceUsuario>();
            services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
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

            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

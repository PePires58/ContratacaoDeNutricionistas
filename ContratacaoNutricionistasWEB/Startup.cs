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

/*
* Programador: Pedro Henrique Pires
* Data: 01/06/2020
* Implementação: Implementando recurso para expirar a autenticação.
*/

/*
* Programador: Pedro Henrique Pires
* Data: 04/06/2020
* Implementação: Implementação da classe de endereço
*/
#endregion

using ContratacaoNutricionistas.Domain.Interfaces.Endereco;
using ContratacaoNutricionistas.Domain.Interfaces.Nutricionista;
using ContratacaoNutricionistas.Domain.Interfaces.Paciente;
using ContratacaoNutricionistas.Domain.Interfaces.Repository;
using ContratacaoNutricionistas.Domain.Interfaces.Usuario;
using ContratacaoNutricionistas.Domain.Repository.Endereco;
using ContratacaoNutricionistas.Domain.Repository.Nutricionista;
using ContratacaoNutricionistas.Domain.Repository.Paciente;
using ContratacaoNutricionistas.Domain.Repository.Repository;
using ContratacaoNutricionistas.Domain.Repository.Usuario;
using ContratacaoNutricionistas.Domain.Servicos.Endereco;
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
                    config.ExpireTimeSpan = new System.TimeSpan(hours: 0, minutes: 10, seconds: 0);
                    config.SlidingExpiration = true;
                });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("Nutricionista", politica =>
                {
                    politica.RequireClaim(Constantes.NutricionistaLogado);
                });

                option.AddPolicy("Paciente", paciente =>
                {
                    paciente.RequireClaim(Constantes.PacienteLogado);
                });

                option.AddPolicy("UsuarioLogado", usuario =>
                {
                    usuario.RequireClaim(Constantes.IDUsuarioLogado);
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region Injeção de depencia
            services.AddSingleton<IUnitOfWork>(new UnitOfWork(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IRepositoryBase, RepositoryBase>();
            services.AddSingleton<IServicePaciente, ServicePaciente>();
            services.AddSingleton<IPacienteRepository, PacienteRepository>();
            services.AddSingleton<IServiceNutricionista, ServiceNutricionista>();
            services.AddSingleton<INutricionistaRepository, NutricionistaRepository>();
            services.AddSingleton<IServiceUsuario, ServiceUsuario>();
            services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
            services.AddSingleton<IServiceEndereco, ServiceEndereco>();
            services.AddSingleton<IEnderecoRepository, EnderecoRepository>();
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

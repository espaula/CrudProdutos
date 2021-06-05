using CadastroProdutos.API.Controllers.Validacoes;
using CadastroProdutos.BLL;
using CadastroProdutos.BLL.Models;
using CadastroProdutos.DAL;
using CadastroProdutos.DAL.Interfaces;
using CadastroProdutos.DAL.Repositorios;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace CadastroProdutos.API
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


            services.AddDbContext<Contexto>(opcoes => opcoes.UseSqlServer(Configuration.GetConnectionString("ConexaoDB")));

            services.AddIdentity<Usuario, Funcao>().AddEntityFrameworkStores<Contexto>();

            services.AddScoped<IProdutosRepositorio, ProdutosRepositorio>();
            services.AddScoped<IFuncaoRepositorio, FuncaoRepositorio>();

            services.AddCors();

            services.AddSpaStaticFiles(diretorio => {

                diretorio.RootPath = "CadastroProdutos-UI";
            
            });
          
            services.AddControllers()
              .AddFluentValidation(x =>
              {
                  x.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
              })
           .AddJsonOptions(opcoes =>
            {
                opcoes.JsonSerializerOptions.IgnoreNullValues = true;
            })
            .AddNewtonsoftJson(opcoes =>
            {
                opcoes.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CadastroProdutos.API", Version = "v1" });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CadastroProdutos.API v1"));
            }

            app.UseCors(opcoes => opcoes.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();


            app.UseStaticFiles();
            app.UseSpaStaticFiles();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa => {

                spa.Options.SourcePath = Path.Combine(Directory.GetCurrentDirectory(), "CadastroProdutos-UI");
                if (env.IsDevelopment())
                {

                    spa.UseProxyToSpaDevelopmentServer($"http://localhost:4200/");
                }
            
            });
        }
    }
}

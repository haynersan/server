﻿#region usings

using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shift.Infra.CrossCutting.AspNetFilters;
using Shift.Infra.CrossCutting.Identity.Context;
using Shift.Services.Api.Configurations;

#endregion

namespace Shift.Services.Api
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }



        public void ConfigureServices(IServiceCollection services)
        {

            

            // Contexto do EF para o Identity
            services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));





            //Configura o modo de compressão Fonte: (https://www.youtube.com/watch?v=uUmNvH7F-eo&t=1302s_) 
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true;

            });



            // Configurações de Autenticação, Autorização e JWT.
            services.AddMvcSecurity(Configuration);


            //Ativando o uso do Cache em Memória
            services.AddMemoryCache();


            // Options para configurações customizadas
            services.AddOptions();





            // MVC com restrição de XML e adição de filtro de ações.
            services.AddMvc(options => 
            {

                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));

            });


            //Adicionei este
            services.AddCors();


            // Versionamento do WebApi
            //services.AddApiVersioning("api/v{version}");



            // Configurações do Swagger
            services.AddSwaggerConfig();

  
            
            // Registrar todos os DI
            services.AddDIConfiguration();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IHttpContextAccessor accessor)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            


            #region Configurações MVC
            //CORS = Cross Orige Request
            //O Objetivo do CORS é controlar as requisições que vem de fora de sua aplicação. 
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
                //c.WithExposedHeaders("X-Pagination-TotalRegisters","X-Pagination-TotalPages");
                c.WithHeaders();
                //c.WithOrigins("http://localhost:4200", "http://localhost:50552", "https://localhost:44390/");
                //c.WithMethods("POST,GET,PUT,DELETE");
            });

            app.UseAuthentication();
            app.UseStaticFiles();

            //Ativa a Comprensão
            app.UseResponseCompression();
            app.UseMvc();

            #endregion



            #region Swagger


            if (env.IsProduction())
            {
                //Se não tiver um token válido no browser não funciona.
                //Descomente para ativar a segurança.
                //app.UseSwaggerAuthorized();
            }

            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Shift.IO API v1.0");
            });

            #endregion
        }
    }
}
#region usings

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shift.Infra.CrossCutting.Identity.Context;
using Shift.Infra.CrossCutting.Identity.Models;
using Shift.Services.Api.Configurations;
using Swashbuckle.AspNetCore.Swagger;

#endregion

namespace Shift.Services.Api
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        public void ConfigureServices(IServiceCollection services)
        {

            #region Identity

                
                #region EntityFramework

                var connection = Configuration.GetConnectionString("DefaultConnection");

                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(connection));


                services.AddIdentity<Usuario, UsuarioRole>()
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddDefaultTokenProviders();

                #endregion


                #region Senha

                services.Configure<IdentityOptions>(options =>
                {
                    options.Password.RequireDigit               = false;         // A senha deve conter pelo menos um número
                    options.Password.RequiredLength             = 6;             // A senha deve conter pelo menos 8 caractares => Valor default = 6
                    options.Password.RequiredUniqueChars        = 6;             // A senha deve conter pelo menos 6 caracteres únicos (diferentes) =>Valor default = 1
                    options.Password.RequireLowercase           = false;         // A senha deve conter letras em minúsculo
                    options.Password.RequireUppercase           = false;         // A senha deve conter letras em maiúsculo
                    options.Password.RequireNonAlphanumeric     = false;
                });


                services.ConfigureApplicationCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                });

                #endregion


                #region Cookie

                services.ConfigureApplicationCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(05);
                    //options.LoginPath
                    //options.LogoutPath
                    //options.AccessDeniedPath
                    options.SlidingExpiration = true; //Indica que ao atingir a metade de expiração do tempo definido para cookie, ele será renovado automáticamente.
                });

            #endregion


            #endregion


            // Options para configurações customizadas
            services.AddOptions();


            // MVC com restrição de XML e adição de filtro de ações.
            services.AddMvc(options => 
            {

                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());


                //options.UseCentralRoutePrefix(new RouteAttribute("api/v{version}"));


            });


            // Versionamento do WebApi
            //services.AddApiVersioning("api/v{version}");



            // Configurações do Swagger
            //services.AddSwaggerConfig();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });




            // Registrar todos os DI
            services.AddDIConfiguration();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IHttpContextAccessor accessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /*
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });*/

     

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();


            #region Swagger


            if (env.IsProduction())
            {
                // Se não tiver um token válido no browser não funciona.
                // Descomente para ativar a segurança.
                // app.UseSwaggerAuthorized();
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




/*
//Swagger - Aula 19 - Time: 1:35 minutos
services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("V1", new Info
    {
        Version         = "V1",

        Title           = "App Shift",

        Description     = "API da Aplicação Shift",

        TermsOfService  = "Nenhum no Momento",

        Contact         = new Contact { Name = "Wellington Hayner", Email = "whayners@hotmail.com", Url = "http://shift.io" },

        License         = new License { Name = "PVT", Url = "http://shift.io/license" }
    });
});*/

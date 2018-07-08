#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shift.Infra.CrossCutting.Identity.Context;
using Shift.Infra.CrossCutting.Identity.Models;

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
                    options.Password.RequireDigit = false;           //A senha deve conter pelo menos um número
                    options.Password.RequiredLength = 6;            //A senha deve conter pelo menos 8 caractares => Valor default = 6
                    options.Password.RequiredUniqueChars = 6;       //A senha deve conter pelo menos 6 caracteres únicos (diferentes) =>Valor default = 1
                    options.Password.RequireLowercase = false;       //A senha deve conter letras em minúsculo
                    options.Password.RequireUppercase = false;       //A senha deve conter letras em maiúsculo
                    options.Password.RequireNonAlphanumeric = false;
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

            services.AddMvc();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}

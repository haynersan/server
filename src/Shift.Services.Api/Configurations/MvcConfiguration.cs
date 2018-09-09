#region usings

using System;
using System.IO.Compression;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shift.Infra.CrossCutting.Identity.Authorization;
using Shift.Infra.CrossCutting.Identity.Context;
using Shift.Infra.CrossCutting.Identity.Models;

#endregion


namespace Shift.Services.Api.Configurations
{
    public static class MvcConfiguration
    {

        public static void AddMvcSecurity(this IServiceCollection services, IConfigurationRoot configuration)
        {






            //Remove os dados NULL dos responses. Ajuda a melhorar a Performance da Aplicação. 
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
               {
                   options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
               });



            if (services == null) throw new ArgumentNullException(nameof(services));



            var tokenConfigurations = new TokenDescriptor();


            new ConfigureFromConfigurationOptions<TokenDescriptor>(
                    configuration.GetSection("JwtTokenOptions"))
                .Configure(tokenConfigurations);



            services.AddSingleton(tokenConfigurations);



            services.AddIdentity<Usuario, UsuarioRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();


            
            #region Senha

            
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit           = false;         // A senha deve conter pelo menos um número
                options.Password.RequiredLength         = 6;             // A senha deve conter pelo menos 8 caractares => Valor default = 6
                options.Password.RequiredUniqueChars    = 6;             // A senha deve conter pelo menos 6 caracteres únicos (diferentes) =>Valor default = 1
                options.Password.RequireLowercase       = false;         // A senha deve conter letras em minúsculo
                options.Password.RequireUppercase       = false;         // A senha deve conter letras em maiúsculo
                options.Password.RequireNonAlphanumeric = false;
            });


            #endregion


            #region ConfiguracoesIdentityAdicionais

            
             //services.ConfigureApplicationCookie(options =>
             //{
             //    options.Cookie.HttpOnly = true;
             //});

             #endregion


            
             #region Cookie

             //services.ConfigureApplicationCookie(options =>
             //{
             //    options.Cookie.HttpOnly = true;
             //    options.ExpireTimeSpan = TimeSpan.FromMinutes(05);
             //    //options.LoginPath
             //    //options.LogoutPath
             //    //options.AccessDeniedPath
             //    options.SlidingExpiration = true; //Indica que ao atingir a metade de expiração do tempo definido para cookie, ele será renovado automáticamente.
             //});

            #endregion




            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {

                bearerOptions.RequireHttpsMetadata = false;
                bearerOptions.SaveToken = true;

                var paramsValidation = bearerOptions.TokenValidationParameters;

                paramsValidation.IssuerSigningKey           = SigningCredentialsConfiguration.Key;
                paramsValidation.ValidAudience              = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer                = tokenConfigurations.Issuer;

                paramsValidation.ValidateIssuerSigningKey   = true;
                paramsValidation.ValidateLifetime           = true;
                paramsValidation.ClockSkew                  = TimeSpan.Zero;

            });


            services.AddAuthorization(options =>
            {

                options.AddPolicy("PodeLerUsuario",         policy => policy.RequireClaim("Usuario", "Ler"));
                options.AddPolicy("PodeGravarUsuario",      policy => policy.RequireClaim("Usuario", "Gravar"));

                options.AddPolicy("PodeLerEmpresa",         policy => policy.RequireClaim("Empresa", "Ler"));
                options.AddPolicy("PodeGravarEmpresa",      policy => policy.RequireClaim("Empresa", "Gravar"));

                options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }
    }
}







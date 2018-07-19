#region usings

using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

#endregion


namespace Shift.Services.Api.Configurations
{
    public static class SwaggerConfiguration
    {

        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {

                    Version         = "v1",

                    Title           = "Shift API",

                    Description     = "API da APP Shift",

                    TermsOfService  = "Nenhum",

                    Contact = new Contact { Name = "Wellington Hayner", Email = "whayners@hotmail.com", Url = "http://shift.io" },

                    License = new License { Name = "PVT", Url = "http://shift.io/licensa" }
                });

                s.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });

            services.ConfigureSwaggerGen(opt =>
            {
                opt.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });
        }

    }
}

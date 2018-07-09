#region usings

using Microsoft.Extensions.DependencyInjection;
using Shift.Infra.CrossCutting.IoC;

#endregion

namespace Shift.Services.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {

            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }  
}

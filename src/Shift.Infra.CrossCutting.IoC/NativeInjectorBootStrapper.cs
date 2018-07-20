#region usings

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shift.Domain.Core.Interfaces;
using Shift.Infra.CrossCutting.AspNetFilters;
using Shift.Infra.CrossCutting.Identity.Handlers;
using Shift.Infra.CrossCutting.Identity.Models;
using Shift.Infra.CrossCutting.Identity.Repository;
using Shift.Infra.Data.Context;
using Shift.Infra.Data.UoW;

#endregion


namespace Shift.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {

            #region AspNet

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion


            #region Infra.CrossCutting.Identity

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<UsuarioHandler, UsuarioHandler>();
            services.AddScoped<IUser, AspNetUser>();

            #endregion


            #region Infra.Data


            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddScoped<ShiftContext>();



            #endregion


            #region Infra.Filtros

            services.AddScoped<ILogger<GlobalExceptionHandlingFilter>, Logger<GlobalExceptionHandlingFilter>>();
            services.AddScoped<ILogger<GlobalActionLogger>, Logger<GlobalActionLogger>>();
            services.AddScoped<GlobalExceptionHandlingFilter>();
            services.AddScoped<GlobalActionLogger>();

            #endregion

        }
    }
}

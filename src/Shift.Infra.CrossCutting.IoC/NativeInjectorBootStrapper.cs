﻿#region usings

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shift.Domain.Core.Interfaces;
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


            #region Infra.Data


            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddScoped<ShiftContext>();



            #endregion

        }
    }
}

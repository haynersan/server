using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shift.Domain.Core.Interfaces;

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


            //services.AddScoped<IUnitOfWork, UnitOfWork>();


            //services.AddScoped<IplanContext>();



            #endregion

        }
    }
}

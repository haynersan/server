#region usings

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel.Handlers;
using Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel.Repository;
using Shift.Domain.Cadastro.EmpresaModel.Handlers;
using Shift.Domain.Cadastro.EmpresaModel.Repository;
using Shift.Domain.Cadastro.LogAuditoriaModel;
using Shift.Domain.Cadastro.ModelsEstatica;
using Shift.Domain.Cadastro.ModelsEstatica.ClasseContabilModel;
using Shift.Domain.Cadastro.ModelsEstatica.GrupoModel;
using Shift.Domain.Cadastro.ModelsEstatica.SituacaoModel;
using Shift.Domain.Cadastro.ModelsEstatica.TipoBloqueioModel;
using Shift.Domain.Core.Interfaces;
using Shift.Infra.CrossCutting.AspNetFilters;
using Shift.Infra.CrossCutting.Identity.Context;
using Shift.Infra.CrossCutting.Identity.Handlers;
using Shift.Infra.CrossCutting.Identity.Models;
using Shift.Infra.CrossCutting.Identity.Repository;
using Shift.Infra.Data.Context;
using Shift.Infra.Data.Repository.Cadastro;
using Shift.Infra.Data.Repository.CadastrosContabeis;
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



            #region Domain.Cadastro

            services.AddScoped<EmpresaHandler, EmpresaHandler>();

            services.AddScoped<CentroCustoHandler, CentroCustoHandler>();

            //Exemplo Padrao Eduardo Pires Aula 15 Time: 49:29
            //services.AddScoped<IHandler<AdicionarEmpresaCommand>, EmpresaHandler>();


            #endregion



            #region Infra.CrossCutting.Identity

            services.AddScoped<IUsuarioRepository,  UsuarioRepository>();
            services.AddScoped<UsuarioHandler,      UsuarioHandler>();
            services.AddScoped<Usuario,             Usuario>();
            services.AddScoped<IUser,               AspNetUser>();

            #endregion



            #region Infra.Data.Repository.Cadastro

            services.AddScoped<IClaimValueRepository,       ClaimValueRepository>();

            services.AddScoped<IClasseContabilRepository,   ClasseContabilRepository>();

            services.AddScoped<IEmpresaRepository,          EmpresaRepository>();

            services.AddScoped<IGrupoRepository,            GrupoRepository>();

            services.AddScoped<ILogAuditoriaRepository,     LogAuditoriaRepository>();

            services.AddScoped<ISituacaoRepository,         SituacaoRepository>();

            services.AddScoped<ITipoBloqueioRepository,     TipoBloqueioRepository>();


            #endregion

            
            
            #region Infra.Data.Repository.CadastrosContabeis

            services.AddScoped<ICentroCustoRepository, CentroCustoRepository>();

            #endregion


            
            #region Infra.Data


            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddScoped<ShiftContext>();
            services.AddScoped<IdentityContext>();



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



/*-------------------------------------------------------------------------------------------------------------------
    
    Conceitos de Dependency Injector

    - Singleton:    Cria uma instância que é usada por toda aplicação;
    
    - Scoped:       Cria uma instância por requisição dentro do escopo. Ou seja, por request. 

    - Transient:    Gera uma instância a cada vez que você chama um objeto;
------------------------------------------------------------------------------------------------------------------*/

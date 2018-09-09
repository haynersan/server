#region usings

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shift.Domain.Cadastro.ModelsEstatica.GrupoModel;
using Shift.Domain.Core.Interfaces;
using Shift.Services.Api.Configurations;

#endregion

namespace Shift.Services.Api.Controllers.Cadastro
{
    public class GrupoController : BaseController
    {

        #region Config

        private readonly IGrupoRepository _grupoRepository;

        public GrupoController(
                                IUnitOfWork uow,
                                IUser user,
                                IGrupoRepository grupoRepository) : base(uow, user)
        {
            _grupoRepository = grupoRepository;
        }

        #endregion



        #region Leitura



        [HttpGet]
        [Route("v1/grupo")]
        [Authorize()]
        public IEnumerable<GrupoCommandResult> Listar([FromServices]IMemoryCache cache)
        {

            IEnumerable<GrupoCommandResult> dadosJSON = cache.GetOrCreate<IEnumerable<GrupoCommandResult>>("", context =>
            {

                context.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

                context.SetPriority(CacheItemPriority.High);

                return _grupoRepository.Listar();
            });


            return dadosJSON;

        }



        #endregion
    }
}

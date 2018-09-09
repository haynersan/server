#region usings

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shift.Domain.Cadastro.ModelsEstatica.TipoBloqueioModel;
using Shift.Domain.Core.Interfaces;
using Shift.Services.Api.Configurations;

#endregion


namespace Shift.Services.Api.Controllers.Cadastro
{
    public class TipoBloqueioController : BaseController
    {

        
        #region Config

        private readonly ITipoBloqueioRepository _tipoBloqueioRepository;

        public TipoBloqueioController(
                                        IUnitOfWork uow,
                                        IUser user,
                                        ITipoBloqueioRepository tipoBloqueioRepository) : base(uow, user)
        {
            _tipoBloqueioRepository = tipoBloqueioRepository;
        }

        #endregion



        #region Leitura



        [HttpGet]
        [Route("v1/tipo-bloqueio")]
        [Authorize()]
        public IEnumerable<TipoBloqueioCommandResult> Listar([FromServices]IMemoryCache cache)
        {

            IEnumerable<TipoBloqueioCommandResult> dadosJSON = cache.GetOrCreate<IEnumerable<TipoBloqueioCommandResult>>("", context =>
            {

                context.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

                context.SetPriority(CacheItemPriority.High);

                return _tipoBloqueioRepository.Listar();
            });


            return dadosJSON;
        }


        
        #endregion

    }
}

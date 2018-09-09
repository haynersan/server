#region usings

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shift.Domain.Cadastro.ModelsEstatica.SituacaoModel;
using Shift.Domain.Core.Interfaces;
using Shift.Services.Api.Configurations;

#endregion

namespace Shift.Services.Api.Controllers.Cadastro
{
    public class SituacaoController : BaseController
    {

        #region Config

        private readonly ISituacaoRepository _situacaoRepository;

        public SituacaoController(
                                    IUnitOfWork uow, 
                                    IUser user,
                                    ISituacaoRepository situacaoRepository) : base(uow, user)
        {
            _situacaoRepository = situacaoRepository;
        }

        #endregion


        #region Leitura


        [HttpGet]
        [Route("v1/situacao")]
        // [Authorize()]
        public IEnumerable<SituacaoCommandResult> Listar([FromServices]IMemoryCache cache)
        {


            IEnumerable<SituacaoCommandResult> dadosJSON = cache.GetOrCreate<IEnumerable<SituacaoCommandResult>>("", context =>
            {

                context.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

                context.SetPriority(CacheItemPriority.High);

                return _situacaoRepository.Listar();
            });


            return dadosJSON;

        }


        #endregion
    }
}

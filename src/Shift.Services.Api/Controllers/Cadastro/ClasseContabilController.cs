#region usings

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shift.Domain.Cadastro.ModelsEstatica.ClasseContabilModel;
using Shift.Domain.Core.Interfaces;
using Shift.Services.Api.Configurations;

#endregion

namespace Shift.Services.Api.Controllers.Cadastro
{
    public class ClasseContabilController : BaseController
    {

        #region Config

        private readonly IClasseContabilRepository _classeContabilRepository;


        public ClasseContabilController(IUnitOfWork uow,
                                IUser user,
                                IClasseContabilRepository classeContabilRepository) : base(uow, user)
        {
            _classeContabilRepository = classeContabilRepository;
        }



        #endregion



        #region Leitura



        [HttpGet]
        [Route("v1/classe-contabil")]
        [Authorize()]
        public IEnumerable<ClasseContabilCommandResult> Listar([FromServices]IMemoryCache cache)
        {

            IEnumerable<ClasseContabilCommandResult> dadosJSON = cache.GetOrCreate<IEnumerable<ClasseContabilCommandResult>>("", context =>
            {

                context.SetAbsoluteExpiration(TimeSpan.FromHours(10));

                context.SetPriority(CacheItemPriority.High);

                return _classeContabilRepository.Listar();
            });


            return dadosJSON;
        }



        #endregion
    }
}

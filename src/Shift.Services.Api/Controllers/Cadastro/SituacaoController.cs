#region usings

using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [AllowAnonymous]
        public IEnumerable<SituacaoCommandResult> ObterTodos()
        {
            return _situacaoRepository.ListarSituacao();
        }


        #endregion
    }
}

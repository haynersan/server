#region usings

using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shift.Domain.Cadastro.ModelsEstatica.TipoBloqueioModel;
using Shift.Domain.Core.Interfaces;
using Shift.Services.Api.Configurations;

#endregion


namespace Shift.Services.Api.Controllers.Cadastro
{
    public class TipoBloqueioController : BaseController
    {

        
        #region Config

        private ITipoBloqueioRepository _tipoBloqueioRepository;

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
        [AllowAnonymous]
        public IEnumerable<TipoBloqueioCommandResult> ObterTodos()
        {
            return _tipoBloqueioRepository.ListarTiposBloqueio();
        }


        #endregion
    }
}

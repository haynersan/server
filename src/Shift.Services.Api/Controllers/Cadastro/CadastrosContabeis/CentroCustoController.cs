#region usings

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel.Commands.Inputs;
using Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel.Handlers;
using Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel.Repository;
using Shift.Domain.Core.Interfaces;
using Shift.Services.Api.Configurations;

#endregion

namespace Shift.Services.Api.Controllers.Cadastro.CadastrosContabeis
{
    public class CentroCustoController : BaseController
    {


        #region Config

        private readonly CentroCustoHandler     _handler;

        private readonly ICentroCustoRepository _centroCustoRepository;

        public CentroCustoController(
                                        IUnitOfWork             uow,
                                        IUser                   user,
                                        CentroCustoHandler      centroCustoHandler,
                                        ICentroCustoRepository  centroCustoRepository) : base (uow, user)
        {

            _handler                = centroCustoHandler;

            _centroCustoRepository  = centroCustoRepository;
        }


        #endregion



        #region Escrita


        [HttpPost]
        [Route("v1/centro-custo")]
        [Authorize()]
        public IActionResult Post([FromBody] AdicionarCentroCustoCommand command)
        {

            var result = _handler.Handle(command);

            return Response(result, _handler.Notifications);

        }



        [HttpPut]
        [Route("v1/centro-custo")]
        [Authorize()]
        public IActionResult Put([FromBody] EditarCentroCustoCommand command)
        {

            var result = _handler.Handle(command);

            return Response(result, _handler.Notifications);

        }



        [HttpDelete]
        [Route("v1/centro-custo/{codEmpresa}/{codCentroCusto}")]
        [Authorize()]
        public IActionResult Delete(string codEmpresa, long codCentroCusto)
        {

            var command = new ExcluirCentroCustoCommand(codEmpresa, codCentroCusto);

            var result = _handler.Handle(command);

            return Response(result, _handler.Notifications);

        }


        #endregion
    }
}

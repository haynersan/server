#region usings

using System.Collections.Generic;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shift.Domain.Core.Interfaces;
using Shift.Infra.CrossCutting.Identity.Commands.Inputs;
using Shift.Infra.CrossCutting.Identity.Handlers;
using Shift.Infra.CrossCutting.Identity.Models;
using Shift.Services.Api.Configurations;

#endregion


namespace Shift.Services.Api.Controllers.Usuarios
{

    public class UsuarioController : BaseController
    {

        #region Config

        private readonly UsuarioHandler _usuarioHandler;


        public UsuarioController(IUnitOfWork uow, UsuarioHandler usuarioHandler) : base(uow)
        {
            _usuarioHandler = usuarioHandler;
        }


        #endregion


        [HttpPost]
        [AllowAnonymous]
        [Route("conta")]
        public IActionResult Adicionar([FromBody] AdicionarUsuarioCommand command)
        {

            var result = _usuarioHandler.Handle(command);

            return Response(result, _usuarioHandler.Notifications);

        }
    }
}


/*
if (!ModelState.IsValid)
{
    return Response(command, null);
}


var user = new Usuario()
{

    UserName = command.UserName,

    Email = command.Email,

    Matricula = command.Matricula
};


var result = await _userManager.CreateAsync(user, command.Password);

if (result.Succeeded)
{
    Response(result,null);
}
else
{
    foreach (var error in result.Errors)
    {
        ModelState.AddModelError(string.Empty, error.Description);
    }
}

return Response(command, null);
*/

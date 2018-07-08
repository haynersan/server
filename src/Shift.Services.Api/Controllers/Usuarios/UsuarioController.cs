#region usings

using System.Collections.Generic;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shift.Infra.CrossCutting.Identity.Commands.Inputs;
using Shift.Infra.CrossCutting.Identity.Models;
using Shift.Services.Api.Configurations;

#endregion


namespace Shift.Services.Api.Controllers.Usuarios
{

    public class UsuarioController : BaseController
    {

        private readonly UserManager<Usuario> _userManager;

        private readonly SignInManager<Usuario> _signInManager;

        private readonly ILogger _logger;



        public UsuarioController(
                                    UserManager<Usuario> userManager, 
                                    SignInManager<Usuario> signInManager,
                                    ILoggerFactory loggerFactory
                                )
        {
            _userManager    = userManager;

            _signInManager  = signInManager;

            _logger         = loggerFactory.CreateLogger<UsuarioController>();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("conta")]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarUsuarioCommand command)
        {

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
        }
    }
}
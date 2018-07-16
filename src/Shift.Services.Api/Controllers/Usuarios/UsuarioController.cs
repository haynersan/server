#region usings

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

        private readonly UsuarioHandler         _usuarioHandler;

        private readonly UserManager<Usuario>   _userManager;

        private readonly SignInManager<Usuario> _signInManager;

        private readonly ILogger _logger;

        //private readonly TokenDescriptor        _tokenDescriptor;

        public UsuarioController(
                                    IUnitOfWork             uow,
                                    IUser                   user,
                                    UsuarioHandler          usuarioHandler,
                                    UserManager<Usuario>    userManager,
                                    SignInManager<Usuario>  signInManager,
                                    ILoggerFactory          loggerFactory
                                    //TokenDescriptor         tokenDescriptor

            ) : base(uow, user)
        {

            _usuarioHandler     = usuarioHandler;

            _userManager        = userManager;

            _signInManager      = signInManager;

            _logger             = loggerFactory.CreateLogger<UsuarioController>();

            //_tokenDescriptor    = tokenDescriptor;
        }


        #endregion



        [HttpPost]
        [AllowAnonymous]
        [Route("nova-conta")]
        public IActionResult Adicionar([FromBody] AdicionarUsuarioCommand command)
        {

            var result = _usuarioHandler.Handle(command);

            
            return Response(result, _usuarioHandler.Notifications);

        }




        [HttpPost]
        [AllowAnonymous]
        [Route("conta")]
        public IActionResult Login([FromBody] LoginUsuarioCommand command)
        {

            var result = _usuarioHandler.Handle(command);


            return Response(result, _usuarioHandler.Notifications);

        }


    }
}










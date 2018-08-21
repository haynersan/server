#region usings

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Shift.Domain.Core.Interfaces;
using Shift.Domain.Core.Utils;
using Shift.Infra.CrossCutting.Identity.Authorization;
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

        //private readonly ILogger _logger;

        private readonly TokenDescriptor        _tokenDescriptor;

        public UsuarioController(
                                    IUnitOfWork             uow,
                                    IUser                   user,
                                    UsuarioHandler          usuarioHandler,
                                    UserManager<Usuario>    userManager,
                                    SignInManager<Usuario>  signInManager,
                                    //ILoggerFactory          loggerFactory
                                    TokenDescriptor         tokenDescriptor

            ) : base(uow, user)
        {

            _usuarioHandler     = usuarioHandler;

            _userManager        = userManager;

            _signInManager      = signInManager;

            //_logger             = loggerFactory.CreateLogger<UsuarioController>();

            _tokenDescriptor    = tokenDescriptor;
        }


        #endregion


        private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);




        [HttpPost]
        [Route("v1/nova-conta")]
        [AllowAnonymous]
        //[Authorize(Policy = "PodeGravarUsuario")]
        public IActionResult Adicionar([FromBody] AdicionarUsuarioCommand command)
        {

            var result = _usuarioHandler.Handle(command);

            
            return Response(result, _usuarioHandler.Notifications);

        }



        [HttpPost]
        [Route("v1/editar-conta")]
        [Authorize()]
        //[Authorize(Policy = "PodeGravarUsuario")]
        public IActionResult Editar([FromBody] EditarUsuarioCommand command)
        {

            var result = _usuarioHandler.Handle(command);


            return Response(result, _usuarioHandler.Notifications);

        }








        [HttpPost]
        //[AllowAnonymous]
        [Route("v1/conta")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioCommand command)
        {

            var result = _usuarioHandler.Handle(command);

            if (_usuarioHandler.Notifications.Any())
            {
                return Response(result, _usuarioHandler.Notifications);
            }

            var response = await GerarTokenUsuario(command);

            return Response(response, _usuarioHandler.Notifications);

        }




        private async Task<object> GerarTokenUsuario(LoginUsuarioCommand login)
        {
            
            var user = await _userManager.FindByNameAsync(login.UserName);


            var userClaims = await _userManager.GetClaimsAsync(user);


            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub,   user.Id.ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat,   ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            
            // Necessário converver para IdentityClaims
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(userClaims);


            var handler         = new JwtSecurityTokenHandler();

            var signingConf     = new SigningCredentialsConfiguration();

            var securityToken   = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer              = _tokenDescriptor.Issuer,
                Audience            = _tokenDescriptor.Audience,
                SigningCredentials  = signingConf.SigningCredentials,
                Subject             = identityClaims,
                NotBefore           = DateTime.Now,
                Expires             = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid)
            });


            var encodedJwt = handler.WriteToken(securityToken);


            var response = new
            {
                access_token = encodedJwt,
                expires_in = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid),
                user = new
                {
                    id = user.Id,
                    nome = user.UserName,
                    matricula = user.Matricula,
                    email = user.Email,
                    claims      = userClaims.Select(c => new { c.Type, c.Value })
                }
            };

            return response;
        }
    }
}







//Aos 3 horas da aula 19 - uso de claims






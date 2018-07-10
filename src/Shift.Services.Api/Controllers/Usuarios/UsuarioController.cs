#region usings

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Shift.Domain.Core.Interfaces;
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

        //private readonly TokenDescriptor        _tokenDescriptor;


        public UsuarioController(
                                    IUnitOfWork             uow, 
                                    UsuarioHandler          usuarioHandler,
                                    UserManager<Usuario>    userManager,
                                    SignInManager<Usuario>  signInManager
                                    //TokenDescriptor         tokenDescriptor

            ) : base(uow)
        {

            _usuarioHandler     = usuarioHandler;

            _userManager        = userManager;

            _signInManager      = signInManager;

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



        
        /*
        private async Task<object> GerarTokenUsuario(LoginUsuarioCommand login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            var userClaims = await _userManager.GetClaimsAsync(user);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            // Necessário converver para IdentityClaims
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(userClaims);

            var handler = new JwtSecurityTokenHandler();
            var signingConf = new SigningCredentialsConfiguration();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenDescriptor.Issuer,
                Audience = _tokenDescriptor.Audience,
                SigningCredentials = signingConf.SigningCredentials,
                Subject = identityClaims,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid)
            });

            var encodedJwt = handler.WriteToken(securityToken);
            var orgUser = _organizadorRepository.ObterPorId(Guid.Parse(user.Id));

            var response = new
            {
                access_token = encodedJwt,
                expires_in = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid),
                user = new
                {
                    id = user.Id,
                    nome = orgUser.Nome,
                    email = orgUser.Email,
                    claims = userClaims.Select(c => new { c.Type, c.Value })
                }
            };

            return response;
        }*/
    }
}










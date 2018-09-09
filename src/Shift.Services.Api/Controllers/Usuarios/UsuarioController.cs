#region usings

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Shift.Domain.Cadastro.ModelsEstatica;
using Shift.Domain.Cadastro.ModelsEstatica.ClaimModel.Commands.Results;
using Shift.Domain.Core.Interfaces;
using Shift.Infra.CrossCutting.Identity.Authorization;
using Shift.Infra.CrossCutting.Identity.Commands.Inputs;
using Shift.Infra.CrossCutting.Identity.Commands.Results;
using Shift.Infra.CrossCutting.Identity.Handlers;
using Shift.Infra.CrossCutting.Identity.Models;
using Shift.Infra.CrossCutting.Identity.Repository;
using Shift.Services.Api.Configurations;

#endregion


namespace Shift.Services.Api.Controllers.Usuarios
{

    public class UsuarioController : BaseController
    {

        
        #region Config

        private readonly UsuarioHandler         _usuarioHandler;

        private readonly IUsuarioRepository     _usuarioRepository;

        private readonly IClaimValueRepository  _claimValueRepository;

        private readonly UserManager<Usuario>   _userManager;

        private readonly SignInManager<Usuario> _signInManager;

        private readonly ILogger                _logger;

        private readonly TokenDescriptor        _tokenDescriptor;


        public UsuarioController(
                                    IUnitOfWork             uow,
                                    IUser                   user,
                                    UsuarioHandler          usuarioHandler,
                                    IUsuarioRepository      usuarioRepository,
                                    IClaimValueRepository   claimValueRepository,
                                    UserManager<Usuario>    userManager,
                                    SignInManager<Usuario>  signInManager,
                                    ILoggerFactory          loggerFactory,
                                    TokenDescriptor         tokenDescriptor

            ) : base(uow, user)
        {

            _usuarioHandler         = usuarioHandler;

            _usuarioRepository      = usuarioRepository;

            _claimValueRepository   = claimValueRepository;

            _userManager            = userManager;

            _signInManager          = signInManager;

            _logger                 = loggerFactory.CreateLogger<UsuarioController>();

            _tokenDescriptor        = tokenDescriptor;
        }


        private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


        #endregion



        #region Escrita-Usuarios



        [HttpPost]
        [Route("v1/usuarios")]
        [Authorize()]
        public IActionResult Post([FromBody] AdicionarUsuarioCommand command)
        {

            var result = _usuarioHandler.Handle(command);

            
            return Response(result, _usuarioHandler.Notifications);

        }



        [HttpPut]
        [Route("v1/usuarios")]
        [Authorize()]
        public async Task<IActionResult> Put([FromBody] EditarUsuarioCommand command)
        {

            var handler = _usuarioHandler.Handle(command);

            if (_usuarioHandler.Notifications.Any())
            {
                return Response(handler, _usuarioHandler.Notifications);
            }

            var user = await _userManager.FindByIdAsync(command.Id.ToString());

            user.UserName = command.UserName;
            user.Email = command.Email;
            user.PhoneNumber = command.PhoneNumber;
            user.Matricula = command.Matricula;
            user.EmailConfirmed = false;


            var result = await _userManager.UpdateAsync(user);


            if (!result.Succeeded)
            {
                _logger.LogInformation(1, "Não foi possível atualizar o usuário");
            }

            _logger.LogInformation(1, "Usuario atualizado com sucesso");

            return Response(result, _usuarioHandler.Notifications);
        }



        [HttpPut]
        [Route("v1/usuarios-excluir")]
        [Authorize()]
        public async Task<IActionResult> Delete([FromBody] ExcluirUsuarioCommand command)
        {

            var handler = _usuarioHandler.Handle(command);

            if (_usuarioHandler.Notifications.Any())
            {
                return Response(handler, _usuarioHandler.Notifications);
            }

            var user = await _userManager.FindByIdAsync(command.Id.ToString());

            user.Excluido = command.Excluido;


            //var result = await _userManager.UpdateAsync(user);

            var result = await _userManager.DeleteAsync(user);


            if (!result.Succeeded)
            {
                _logger.LogInformation(1, "Não foi possível excluir o usuário");
            }

            _logger.LogInformation(1, "Usuario excluido com sucesso");

            return Response(result, _usuarioHandler.Notifications);
        }



        [HttpPost]
        [Route("v1/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioCommand command)
        {

            var user = await _userManager.FindByNameAsync(command.UserName);

            if (user == null)
            {
                return BadRequest("O usuário informado não existe");
            }

            var result = _usuarioHandler.Handle(command);

            if (_usuarioHandler.Notifications.Any())
            {
                return Response(result, _usuarioHandler.Notifications);
            }

            var response = await GerarTokenUsuario(command);

            return Response(response, _usuarioHandler.Notifications);

        }



        #endregion



        #region Escrita-Claims



        [HttpPost]
        [Route("v1/claim")]
        [Authorize()]
        public async Task<IActionResult> PostClaim([FromBody] AdicionarUsuarioClaimCommand command)
        {

            var handler = _usuarioHandler.Handle(command);

            if (_usuarioHandler.Notifications.Any())
            {
                return Response(handler, _usuarioHandler.Notifications);
            }

            var user = await _userManager.FindByIdAsync(command.UserId.ToString());

            var result = await _userManager.AddClaimAsync(user, new Claim(command.ClaimType, command.ClaimValue));


            if (!result.Succeeded)
            {
                _logger.LogInformation(1, "Não foi possível adcionar claim para o usuário");
            }

            _logger.LogInformation(1, "Claim adicionada com sucesso");

            return Response(result, _usuarioHandler.Notifications);

        }



        [HttpDelete]
        [Route("v1/claim")]
        [Authorize()]
        public async Task<IActionResult> DeleteClaim([FromBody] ExcluirUsuarioClaimCommand command)
        {

            var handler = _usuarioHandler.Handle(command);

            if (_usuarioHandler.Notifications.Any())
            {
                return Response(handler, _usuarioHandler.Notifications);
            }

            var user = await _userManager.FindByIdAsync(command.UserId.ToString());

            var result = await _userManager.RemoveClaimAsync(user, new Claim(command.ClaimType, command.ClaimValue));

            if (!result.Succeeded)
            {
                _logger.LogInformation(1, "Não foi possível adcionar claim para o usuário");
            }

            _logger.LogInformation(1, "Claim adicionada com sucesso");

            return Response(result, _usuarioHandler.Notifications);

        }



        #endregion



        #region Leitura-Usuarios



        [HttpGet]
        [Route("v1/usuarios")]
        [Authorize()]
        public IEnumerable<UsuarioCommandResult> ListarUsuarios()
        {
            return _usuarioRepository.ListarUsuarios(false);
        }



        [HttpGet]
        [Route("v1/usuarios-paginados")]
        [Authorize()]
        public IActionResult ListarUsuariosPaginados(int pagina = 1, int qtdeItensPorPagina = 2, string nome = null)
        {


            if (pagina <= 0 || qtdeItensPorPagina <= 0)
                return BadRequest("Os parâmetros pagina e tamanhoPagina devem ser maiores que zero.");



            if (qtdeItensPorPagina > 10)
                return BadRequest("O tamanho máximo de página permitido é 10.");



            int totalRegistros = _usuarioRepository.TotalizarUsuarios(false).ToList().Count;



            int totalPaginas = (int)Math.Ceiling(totalRegistros / Convert.ToDecimal(qtdeItensPorPagina));



            if (pagina > totalPaginas)
                return BadRequest("A página solicitada não existe.");



            var resultado = new
            {

                dados = _usuarioRepository.ListarUsuariosPaginados(pagina, qtdeItensPorPagina, nome, false),


                paginaAtual = pagina.ToString(),


                itensPorPagina = qtdeItensPorPagina.ToString(),


                totalPaginas = totalPaginas.ToString(),


                totalRegistros = totalRegistros.ToString()
            };



            return Ok(resultado);

        }



        #endregion



        #region Leitura-Claims



        [HttpGet]
        [Route("v1/claim-types")]
        [Authorize()]
        public IEnumerable<ClaimTypeCommandResult> ListarClaimTypes([FromServices]IMemoryCache cache)
        {


            IEnumerable<ClaimTypeCommandResult> dadosJSON = cache.GetOrCreate<IEnumerable<ClaimTypeCommandResult>>("", context =>
            {

                context.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

                context.SetPriority(CacheItemPriority.High);

                return _claimValueRepository.ListarClaimTypes();
            });


            return dadosJSON;

        }



        [HttpGet]
        [Route("v1/claim-values")]
        [Authorize()]
        public IEnumerable<ClaimValueCommandResult> ListarClaimValues([FromServices]IMemoryCache cache)
        {


            IEnumerable<ClaimValueCommandResult> dadosJSON = cache.GetOrCreate<IEnumerable<ClaimValueCommandResult>>("", context =>
            {

                context.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

                context.SetPriority(CacheItemPriority.High);

                return _claimValueRepository.ListarClaimValues();
            });


            return dadosJSON;

        }



        [HttpGet]
        [Route("v1/claim-usuarios")]
        [Authorize()]
        public IActionResult ListarClaimUsuarios(int pagina = 1, int qtdeItensPorPagina = 2, string nome = null)
        {


            if (pagina <= 0 || qtdeItensPorPagina <= 0)
                return BadRequest("Os parâmetros pagina e tamanhoPagina devem ser maiores que zero.");



            if (qtdeItensPorPagina > 10)
                return BadRequest("O tamanho máximo de página permitido é 10.");



            int totalRegistros = _usuarioRepository.TotalizarUsuarioClaims().ToList().Count;

         

            int totalPaginas = (int)Math.Ceiling(totalRegistros / Convert.ToDecimal(qtdeItensPorPagina));



            if (pagina > totalPaginas)
                return BadRequest("A página solicitada não existe.");



            var resultado = new
            {

                dados           = _usuarioRepository.ListarUsuarioClaims(pagina, qtdeItensPorPagina, nome),


                paginaAtual     = pagina.ToString(),


                itensPorPagina  = qtdeItensPorPagina.ToString(),


                totalPaginas    = totalPaginas.ToString(),


                totalRegistros  = totalRegistros.ToString()
            };



            return Ok(resultado);

        }



        #endregion



        #region Metodos



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
                    id          = user.Id,
                    nome        = user.UserName,
                    matricula   = user.Matricula,
                    email       = user.Email,
                    claims      = userClaims.Select(c => new { c.Type, c.Value })
                }
            };

            return response;
        }


        #endregion

    }
}







//Aos 3 horas da aula 19 - uso de claims






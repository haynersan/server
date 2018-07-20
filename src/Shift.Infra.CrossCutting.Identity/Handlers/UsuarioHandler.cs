#region usings

using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using Shift.Domain.Core.Enums;
using Shift.Domain.Core.Interfaces;
using Shift.Domain.Core.Utils;
using Shift.Infra.CrossCutting.Identity.Commands.Inputs;
using Shift.Infra.CrossCutting.Identity.Models;
using Shift.Infra.CrossCutting.Identity.Repository;

#endregion


namespace Shift.Infra.CrossCutting.Identity.Handlers
{
    public class UsuarioHandler :
        Notifiable,
        IHandler<AdicionarUsuarioCommand>,
        IHandler<LoginUsuarioCommand>
    {

        private readonly UserManager<Usuario>   _userManager;

        private readonly SignInManager<Usuario> _signInManager;

        private readonly IUsuarioRepository     _usuarioRepository;


        public UsuarioHandler(  
                                UserManager<Usuario>    userManager,
                                SignInManager<Usuario>  signInManager,
                                IUsuarioRepository      usuarioRepository)
        {

            _userManager        = userManager;

            _signInManager      = signInManager;

            _usuarioRepository = usuarioRepository;
        }


        public ICommandResult Handle(AdicionarUsuarioCommand command)
        {
            
            
            // Fail Fast Validations
            command.Validar();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível adicionar o registro");
            }



            //Verificar se o Codigo ou Nome informado já está em Uso
            if (_usuarioRepository.checarSeUsuarioExiste((int)EAcao.Adicionar, null, command.UserName, command.Matricula))
            {
                AddNotification("Nome/Matricula", "O Nome ou Matrícula já estão em uso");
                return new CommandResult(false, "Não foi possível adicionar o registro");
            }


            if (command.UserName.Contains(" "))
            {
                AddNotification("Nome", "O Nome não pode conter espaços");
                return new CommandResult(false, "Não foi possível adicionar o registro");
            }
            


            var user = new Usuario()
            {

                UserName = command.UserName,

                Email = command.Email,

                Matricula = command.Matricula
            };


            var result = _userManager.CreateAsync(user, command.Password);

           

            if (!result.Result.Succeeded)
            {
                foreach (var error in result.Result.Errors)
                {
                    AddNotification(string.Empty, error.Description);
                }
            }


            // Checar as notificações
            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar esta operação");


            // Retornar informações
            return new CommandResult(true, "Operação realizada com sucesso");
        }



        public ICommandResult Handle(LoginUsuarioCommand command)
        {
            
            // Fail Fast Validations
            command.Validar();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível efetuar o login");
            }


            var result = _signInManager.PasswordSignInAsync(command.UserName, command.Password, false, false);



            if (!result.Result.Succeeded)
            {
                AddNotification("falha", "Não foi possível realizar login");
                return new CommandResult(false, "Não foi possível realizar esta operação");
               
            }


            // Retornar informações
            return new CommandResult(true, "Operação realizada com sucesso");

        }
    }
}

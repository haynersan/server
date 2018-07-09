#region 

using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using Shift.Domain.Core.Interfaces;
using Shift.Domain.Core.Utils;
using Shift.Infra.CrossCutting.Identity.Commands.Inputs;
using Shift.Infra.CrossCutting.Identity.Models;

#endregion


namespace Shift.Infra.CrossCutting.Identity.Handler
{
    public class UsuarioHandler :
        Notifiable,
        IHandler<AdicionarUsuarioCommand>
    {

        private readonly UserManager<Usuario> _userManager;

        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioHandler(
                                UserManager<Usuario> userManager,
                                SignInManager<Usuario> signInManager)
        {

            _userManager    = userManager;

            _signInManager  = signInManager;

        }


        public ICommandResult Handle(AdicionarUsuarioCommand command)
        {

            // Fail Fast Validations
           // command.Validar();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível adicionar a empresa");
            }

            
            var user = new Usuario()
            {

                UserName = command.UserName,

                Email = command.Email,

                Matricula = command.Matricula
            };


            var result = _userManager.CreateAsync(user, command.Password);



            if (result.IsCompletedSuccessfully)
            {
                return new CommandResult(true, "Operação realizada com sucesso!");
            }
            else
            {
                foreach (var error in result.Result.Errors)
                {

                    command.AddNotification(string.Empty, error.Description);

                }
            }


            // Agrupar as Validações
            AddNotifications(command);



            // Checar as notificações
            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar esta operação");



            // Retornar informações
            return new CommandResult(true, "Operação realizada com sucesso");
        }
    }
}

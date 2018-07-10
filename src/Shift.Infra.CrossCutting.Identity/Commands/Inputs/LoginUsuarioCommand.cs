#region usings

using Flunt.Validations;
using Shift.Domain.Core.Interfaces;

#endregion


namespace Shift.Infra.CrossCutting.Identity.Commands.Inputs
{
    public class LoginUsuarioCommand : BaseUsuarioCommand, ICommandResult
    {

        public LoginUsuarioCommand(string username, string password)
        {
            UserName = username;

            Password = password; 
        }

        //Fail Fast Validations
        public void Validar()
        {
            AddNotifications(new Contract()

            .Requires()

            .IsNotNullOrEmpty(UserName, "Usuario", "O usuário deve ser informado")


            .IsNotNullOrEmpty(Password, "Senha", "A senha deve ser informada")

            );
        }
    }
}

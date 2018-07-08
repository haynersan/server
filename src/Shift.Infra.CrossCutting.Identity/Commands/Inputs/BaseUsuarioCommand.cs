using Flunt.Notifications;

namespace Shift.Infra.CrossCutting.Identity.Commands.Inputs
{
    public class BaseUsuarioCommand : Notifiable
    {
        public string UserName      { get; protected set; }

        public string Email         { get; protected set; }

        public string Password      { get; protected set; }

        public string Matricula     { get; protected set; }
    }
}

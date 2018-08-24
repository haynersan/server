#region usings

using System;
using Flunt.Notifications;

#endregion


namespace Shift.Infra.CrossCutting.Identity.Commands.Inputs
{
    public class BaseUsuarioCommand : Notifiable
    {

        public Guid     Id            { get; protected set; }

        public string   UserName      { get; protected set; }

        public string   Email         { get; protected set; }

        public string   Password      { get; protected set; }

        public string   Matricula     { get; protected set; }

        public string   PhoneNumber   { get; protected set; }

        public bool     Excluido      { get; protected set; }

    }
}

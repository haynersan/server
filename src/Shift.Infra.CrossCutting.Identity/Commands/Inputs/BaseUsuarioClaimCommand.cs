#region usings

using System;
using Flunt.Notifications;

#endregion

namespace Shift.Infra.CrossCutting.Identity.Commands.Inputs
{
    public class BaseUsuarioClaimCommand : Notifiable
    {

        public Guid     UserId        { get; protected set; }

        public string   ClaimType     { get; protected set; }

        public string   ClaimValue    { get; protected set; }
    }
}

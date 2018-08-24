#region usings

using System;
using Shift.Domain.Core.Interfaces;
using Shift.Domain.Core.Models;

#endregion

namespace Shift.Infra.CrossCutting.Identity.Commands.Results
{
    public class UsuarioClaimCommandResult : ICommandResult
    {

        public int      Id            { get; set; }

        public Guid     UserId        { get; set; }

        public string   UserName      { get; set; }

        public string   ClaimType     { get; set; }

        public string   ClaimValue    { get; set; }
    }
}

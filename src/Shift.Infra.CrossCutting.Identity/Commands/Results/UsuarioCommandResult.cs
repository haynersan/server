#region usings

using System;
using Shift.Domain.Core.Interfaces;

#endregion

namespace Shift.Infra.CrossCutting.Identity.Commands.Results
{
    public class UsuarioCommandResult : ICommandResult
    {

        public Guid     Id            { get; set; }

        public string   UserName      { get; set; }

        public string   Email         { get; set; }

        public string   PhoneNumber   { get; set; }

        public string   Matricula     { get; set; }

    }
}

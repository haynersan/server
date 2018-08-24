#region usings

using System;
using Shift.Domain.Core.Interfaces;

#endregion

namespace Shift.Infra.CrossCutting.Identity.Commands.Inputs
{
    public class ExcluirUsuarioCommand : BaseUsuarioCommand, ICommandResult
    {

        public ExcluirUsuarioCommand(Guid id)
        {
            Id          = id;

            Excluido    = true;
        }
    }
}

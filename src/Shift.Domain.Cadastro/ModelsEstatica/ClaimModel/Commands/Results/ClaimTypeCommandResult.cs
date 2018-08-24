using Shift.Domain.Core.Interfaces;

namespace Shift.Domain.Cadastro.ModelsEstatica.ClaimModel.Commands.Results
{
    public class ClaimTypeCommandResult : ICommandResult
    {
        public string ClaimType { get; set; }
    }
}

using Shift.Domain.Core.Interfaces;

namespace Shift.Domain.Cadastro.ModelsEstatica.ClaimModel.Commands.Results
{
    public class ClaimValueCommandResult : ICommandResult
    {
        public int      Codigo      { get; set; }

        public string   Valor       { get; set; }
    }
}

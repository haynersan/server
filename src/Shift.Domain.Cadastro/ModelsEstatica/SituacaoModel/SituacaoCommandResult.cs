using Shift.Domain.Core.Interfaces;

namespace Shift.Domain.Cadastro.ModelsEstatica.SituacaoModel
{
    public class SituacaoCommandResult : ICommandResult
    {
        public int IdSituacao       { get; set; }

        public string DescSituacao  { get; set; }
    }
}

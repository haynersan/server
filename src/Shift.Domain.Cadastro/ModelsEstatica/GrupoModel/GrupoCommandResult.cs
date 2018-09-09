using Shift.Domain.Core.Interfaces;

namespace Shift.Domain.Cadastro.ModelsEstatica.GrupoModel
{
    public class GrupoCommandResult : ICommandResult
    {

        public string Codigo    { get; private set; }

        public string Nome      { get; private set; }
    }
}

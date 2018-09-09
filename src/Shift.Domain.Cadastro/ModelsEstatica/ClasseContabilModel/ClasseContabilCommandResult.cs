using Shift.Domain.Core.Interfaces;

namespace Shift.Domain.Cadastro.ModelsEstatica.ClasseContabilModel
{
    public class ClasseContabilCommandResult : ICommandResult
    {
        public int      Codigo  { get; set; }

        public string   Nome    { get; set; }
    }
}

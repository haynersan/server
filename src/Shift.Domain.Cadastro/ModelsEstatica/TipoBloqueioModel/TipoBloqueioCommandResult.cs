using Shift.Domain.Core.Interfaces;

namespace Shift.Domain.Cadastro.ModelsEstatica.TipoBloqueioModel
{
    public class TipoBloqueioCommandResult : ICommandResult
    {

        public int      Codigo  { get; set; }

        public string   Tipo    { get; set; }
    }
}

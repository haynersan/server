#region usings

using System.Collections.Generic;
using Shift.Domain.Core.Interfaces;

#endregion

namespace Shift.Domain.Cadastro.ModelsEstatica.ClasseContabilModel
{
    public interface IClasseContabilRepository : IRepository<ClasseContabil>
    {
        IEnumerable<ClasseContabilCommandResult> Listar();
    }
}

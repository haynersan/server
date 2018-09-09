#region usings

using System.Collections.Generic;
using Shift.Domain.Core.Interfaces;

#endregion

namespace Shift.Domain.Cadastro.ModelsEstatica.GrupoModel
{
    public interface IGrupoRepository : IRepository<Grupo>
    {
        IEnumerable<GrupoCommandResult> Listar();
    }
}

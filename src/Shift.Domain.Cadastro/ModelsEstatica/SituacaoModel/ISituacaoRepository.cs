#region usings

using System.Collections.Generic;
using Shift.Domain.Core.Interfaces;

#endregion

namespace Shift.Domain.Cadastro.ModelsEstatica.SituacaoModel
{
    public interface ISituacaoRepository : IRepository<Situacao>
    {
        IEnumerable<SituacaoCommandResult> Listar();
    }
}

#region usings

using System.Collections.Generic;
using Shift.Domain.Core.Interfaces;

#endregion

namespace Shift.Domain.Cadastro.ModelsEstatica.TipoBloqueioModel
{
    public interface ITipoBloqueioRepository : IRepository<TipoBloqueio>
    {
        IEnumerable<TipoBloqueioCommandResult> ListarTiposBloqueio();
    }
}

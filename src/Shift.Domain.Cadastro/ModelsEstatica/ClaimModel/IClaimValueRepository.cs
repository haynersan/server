#region usings

using System.Collections.Generic;
using Shift.Domain.Cadastro.ModelsEstatica.ClaimModel;
using Shift.Domain.Cadastro.ModelsEstatica.ClaimModel.Commands.Results;
using Shift.Domain.Core.Interfaces;

#endregion

namespace Shift.Domain.Cadastro.ModelsEstatica
{
    public interface IClaimValueRepository : IRepository<ClaimValue>
    {

        IEnumerable<ClaimValueCommandResult> ListarClaimValues();


        IEnumerable<ClaimTypeCommandResult> ListarClaimTypes();

    }
}

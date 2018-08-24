#region usings

using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shift.Domain.Cadastro.ModelsEstatica;
using Shift.Domain.Cadastro.ModelsEstatica.ClaimModel;
using Shift.Domain.Cadastro.ModelsEstatica.ClaimModel.Commands.Results;
using Shift.Infra.Data.Context;

#endregion

namespace Shift.Infra.Data.Repository.Cadastro
{
    public class ClaimValueRepository : Repository<ClaimValue>, IClaimValueRepository
    {


        public ClaimValueRepository(ShiftContext context) : base(context)
        {

        }



        public IEnumerable<ClaimTypeCommandResult> ListarClaimTypes()
        {
            return Db.Database.GetDbConnection().Query<ClaimTypeCommandResult>("[Estatico].[SP_ClaimTypeListar]",
               commandType: CommandType.StoredProcedure).ToList();
        }



        public IEnumerable<ClaimValueCommandResult> ListarClaimValues()
        {
            return Db.Database.GetDbConnection().Query<ClaimValueCommandResult>("[Estatico].[SP_ClaimValueListar]",
                           commandType: CommandType.StoredProcedure).ToList();
        }
    }
}

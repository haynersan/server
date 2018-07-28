#region usings

using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shift.Domain.Cadastro.ModelsEstatica.SituacaoModel;
using Shift.Infra.Data.Context;

#endregion

namespace Shift.Infra.Data.Repository.Cadastro
{
    public class SituacaoRepository : Repository<Situacao>, ISituacaoRepository
    {
        public SituacaoRepository(ShiftContext context) : base(context)
        {
        }

        public IEnumerable<SituacaoCommandResult> ListarSituacao()
        {
            return Db.Database.GetDbConnection().Query<SituacaoCommandResult>("[Estatico].[SP_SituacaoListar]",
               commandType: CommandType.StoredProcedure).ToList();
        }
    }
}

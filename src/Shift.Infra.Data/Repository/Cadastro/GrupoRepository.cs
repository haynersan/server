#region usings

using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shift.Domain.Cadastro.ModelsEstatica.GrupoModel;
using Shift.Infra.Data.Context;

#endregion

namespace Shift.Infra.Data.Repository.Cadastro
{
    public class GrupoRepository : Repository<Grupo>, IGrupoRepository
    {


        public GrupoRepository(ShiftContext context) : base(context)
        {

        }

        public IEnumerable<GrupoCommandResult> Listar()
        {
            return Db.Database.GetDbConnection().Query<GrupoCommandResult>("[Estatico].[SP_GrupoListar]",
               commandType: CommandType.StoredProcedure).ToList();
        }
    }
}

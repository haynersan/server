#region usings

using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shift.Domain.Cadastro.ModelsEstatica.ClasseContabilModel;
using Shift.Infra.Data.Context;

#endregion

namespace Shift.Infra.Data.Repository.Cadastro
{
    public class ClasseContabilRepository : Repository<ClasseContabil>, IClasseContabilRepository
    {

        public ClasseContabilRepository(ShiftContext context) : base(context)
        {

        }



        public IEnumerable<ClasseContabilCommandResult> Listar()
        {
            return Db.Database.GetDbConnection().Query<ClasseContabilCommandResult>("[Estatico].[SP_ClasseContabilListar]",
              commandType: CommandType.StoredProcedure).ToList();
        }
    }
}

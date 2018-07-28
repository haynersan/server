#region usings

using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shift.Domain.Cadastro.ModelsEstatica.TipoBloqueioModel;
using Shift.Infra.Data.Context;

#endregion


namespace Shift.Infra.Data.Repository.Cadastro
{
    public class TipoBloqueioRepository : Repository<TipoBloqueio>, ITipoBloqueioRepository
    {


        public TipoBloqueioRepository(ShiftContext context) : base(context)
        {

        }

        public IEnumerable<TipoBloqueioCommandResult> ListarTiposBloqueio()
        {
            return Db.Database.GetDbConnection().Query<TipoBloqueioCommandResult>("[Estatico].[SP_TipoBloqueioListar]",
               commandType: CommandType.StoredProcedure).ToList();
        }
    }
}

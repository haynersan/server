#region usings

using Shift.Domain.Cadastro.LogAuditoriaModel;
using Shift.Infra.Data.Context;

#endregion

namespace Shift.Infra.Data.Repository.Cadastro
{
    public class LogAuditoriaRepository : Repository<LogAuditoria>, ILogAuditoriaRepository
    {
        public LogAuditoriaRepository(ShiftContext context) : base(context)
        {
        }
    }
}

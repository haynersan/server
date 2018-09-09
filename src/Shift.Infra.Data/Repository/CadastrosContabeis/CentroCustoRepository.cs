#region usings

using Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel;
using Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel.Repository;
using Shift.Infra.Data.Context;

#endregion

namespace Shift.Infra.Data.Repository.CadastrosContabeis
{
    public class CentroCustoRepository : Repository<CentroCusto>, ICentroCustoRepository
    {

        public CentroCustoRepository(ShiftContext context) : base(context)
        {

        }

        public void Excluir(string codEmpresa, long codCentroCusto)
        {
            DbSet.Remove(DbSet.Find(codEmpresa, codCentroCusto));
        }
    }
}

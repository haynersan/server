using Shift.Domain.Core.Interfaces;

namespace Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel.Repository
{
    public interface ICentroCustoRepository : IRepository<CentroCusto>
    {

        void Excluir(string codEmpresa, long codCentroCusto);
    }
}

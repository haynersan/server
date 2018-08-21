using Shift.Domain.Core.Interfaces;

namespace Shift.Domain.Cadastro.EmpresaModel.Commands.Inputs
{
    public class ExcluirEmpresaCommand : BaseEmpresaCommand, ICommandResult
    {
        public ExcluirEmpresaCommand(string codEmpresa)
        {
            CodEmpresa = codEmpresa;
        }
    }
}

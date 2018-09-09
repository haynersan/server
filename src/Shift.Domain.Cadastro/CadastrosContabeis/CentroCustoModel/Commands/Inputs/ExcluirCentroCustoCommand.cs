#region usings

using System;
using Shift.Domain.Core.Interfaces;

#endregion

namespace Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel.Commands.Inputs
{
    public class ExcluirCentroCustoCommand : BaseCentroCustoCommand, ICommandResult
    {

        public ExcluirCentroCustoCommand(string codEmpresa, long codCentroCusto)
        {
            CodEmpresa      = codEmpresa;

            CodCentroCusto  = codCentroCusto;
        }
    }
}

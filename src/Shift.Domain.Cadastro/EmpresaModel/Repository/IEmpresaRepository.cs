#region usings

using System.Collections.Generic;
using Shift.Domain.Cadastro.EmpresaModel.Commands.Results;
using Shift.Domain.Core.Interfaces;

#endregion

namespace Shift.Domain.Cadastro.EmpresaModel.Repository
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {

        #region Leitura

        EmpresaCommandResult ObterPorCodigo(string codigo);

        IEnumerable<EmpresaCommandResult> ListarEmpresasPaginadas(int pagina, int qtdeItensPorPagina, string nome);

        IEnumerable<EmpresaCommandResult> ListarEmpresas();

        #endregion



        #region Validacoes

        bool ChecarSeEmpresaExiste(int acao, string codEmpresa, string nome, string cnpj);

        #endregion
    }
}

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

        IEnumerable<EmpresaCommandResult> ObterPorNome(string nome);

        IEnumerable<EmpresaCommandResult> ObterEmpresas(int pagina, int quantidadeItens, string nome);

        #endregion



        #region Validacoes

        bool checarSeEmpresaExiste(int acao, string codEmpresa, string nome, string cnpj);

        #endregion
    }
}

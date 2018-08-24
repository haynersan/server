#region usings

using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shift.Domain.Cadastro.EmpresaModel;
using Shift.Domain.Cadastro.EmpresaModel.Commands.Results;
using Shift.Domain.Cadastro.EmpresaModel.Repository;
using Shift.Infra.Data.Context;

#endregion


namespace Shift.Infra.Data.Repository.Cadastro
{
    public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository
    {



        public EmpresaRepository(ShiftContext context) : base(context)
        {
        }



        public bool ChecarSeEmpresaExiste(int acao, string codEmpresa, string nome, string cnpj)
        {
            return Db.Database.GetDbConnection().Query<bool>("[Cadastro].[SP_EmpresaChecar]",
                    new
                        {
                            Acao        = acao,
                            CodEmpresa  = codEmpresa,
                            Nome        = nome,
                            Cnpj        = cnpj
                        },
                        commandType: CommandType.StoredProcedure).FirstOrDefault();
        }



        public IEnumerable<EmpresaCommandResult> ListarEmpresas()
        {

            return Db.Database.GetDbConnection().Query<EmpresaCommandResult>("[Cadastro].[SP_EmpresaListar]",
                commandType: CommandType.StoredProcedure).ToList();
        }



        public IEnumerable<EmpresaCommandResult> ListarEmpresasPaginadas(int pagina, int qtdeItensPorPagina, string nome)
        {
            return Db.Database.GetDbConnection().Query<EmpresaCommandResult>("[Cadastro].[SP_EmpresaListar]",
                new { Nome = nome},
                commandType: CommandType.StoredProcedure).ToList().Skip(qtdeItensPorPagina * (pagina - 1)).Take(qtdeItensPorPagina);
        }



        public EmpresaCommandResult ObterPorCodigo(string codigo)
        {
            return Db.Database.GetDbConnection().Query<EmpresaCommandResult>("[Cadastro].[SP_EmpresaListarPorId]",
                new { CodEmpresa = codigo },
                commandType: CommandType.StoredProcedure).FirstOrDefault();
        }


    }
}





/*
public override void RemoverString(string codigo)
{

    var empresa = Buscar(e => e.CodEmpresa == codigo && e.Excluido == false).FirstOrDefault();


    empresa.Excluir();


    Atualizar(empresa);
}*/

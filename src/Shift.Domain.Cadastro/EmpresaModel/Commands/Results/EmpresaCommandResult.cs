using Shift.Domain.Cadastro.ModelsEstatica.SituacaoModel;
using Shift.Domain.Core.Interfaces;

namespace Shift.Domain.Cadastro.EmpresaModel.Commands.Results
{
    public class EmpresaCommandResult : ICommandResult
    {

        public string   CodEmpresa    { get; set; }


        public string   Nome          { get; set; }


        public string   Cnpj          { get; set; }


        public int      IdSituacao    { get; set; }


        public string   DescSituacao  { get; set; }
    }
}

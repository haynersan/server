using Flunt.Notifications;

namespace Shift.Domain.Cadastro.EmpresaModel.Commands.Inputs
{
    public class BaseEmpresaCommand : Notifiable
    {

        public string   CodEmpresa    { get; protected set; }


        public string   Nome          { get; protected set; }


        public string   Cnpj          { get; protected set; }


        public int      IdSituacao    { get; protected set; }


        public bool     Excluido      { get; protected set; }
    }
}

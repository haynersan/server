#region usings

using Flunt.Validations;
using Shift.Domain.Core.Interfaces;
using Shift.Domain.Core.Utils;
using Shift.Domain.Core.ValueObjects;

#endregion


namespace Shift.Domain.Cadastro.EmpresaModel.Commands.Inputs
{
    public class AtualizarEmpresaCommand : BaseEmpresaCommand, ICommandResult
    {

        public AtualizarEmpresaCommand(string codEmpresa, string nome, string cnpj, int idSituacao)
        {

            CodEmpresa  = TratandoStrings.RemoverAcentuacao(TratandoStrings.RemoverEspacosEntrePalavras(codEmpresa.ToUpper()));

            Nome        = TratandoStrings.RemoverAcentuacao(TratandoStrings.RemoverEspacosEntrePalavras(nome.ToUpper()));

            Cnpj        = cnpj.Replace(".", "").Replace("-", "");

            IdSituacao  = idSituacao;
        }



        //Fail Fast Validations
        public void Validar()
        {
            AddNotifications(new Contract()

                .Requires()

                //Cod. Empresa
                .IsNotNullOrEmpty(CodEmpresa, "Codigo", "O código deve ser informado")
                .HasLen(CodEmpresa, 4, "Codigo", "O código deve possuir 04 caracteres")


                //Nome
                .IsNotNullOrEmpty(Nome, "Nome", "O nome deve ser informado")
                .HasMinLen(Nome, 3, "Nome", "O nome deve possuir no mínimo 03 caracteres")
                .HasMaxLen(Nome, 50, "Nome", "O nome deve possuir no máximo 50 caracteres")


                //Cnpj
                .IsNotNullOrEmpty(Cnpj, "CNPJ", "O CNPJ deve ser informado")
                .HasMaxLen(Cnpj, 14, "CNPJ", "O cnpj deve possuir no máximo 14 caracteres"));
        }
    }
}

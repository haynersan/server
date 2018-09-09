#region usings

using Flunt.Validations;
using Shift.Domain.Core.Interfaces;
using Shift.Domain.Core.Utils;

#endregion

namespace Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel.Commands.Inputs
{
    public class AdicionarCentroCustoCommand : BaseCentroCustoCommand, ICommandResult
    {

        public AdicionarCentroCustoCommand(string codEmpresa, long codCentroCusto, string nomeCentroCusto, string codGrupo, int codClasse, int codTipoBloqueio)
        {


            CodEmpresa      = TratandoStrings.RemoverAcentuacao(TratandoStrings.RemoverEspacosEntrePalavras(codEmpresa.ToUpper()));


            CodCentroCusto  = codCentroCusto;


            NomeCentroCusto = TratandoStrings.RemoverAcentuacao(TratandoStrings.RemoverEspacosEntrePalavras(nomeCentroCusto.ToUpper()));


            CodGrupo        = TratandoStrings.RemoverAcentuacao(TratandoStrings.RemoverEspacosEntrePalavras(codGrupo.ToUpper()));


            CodClasse       = codClasse;


            CodTipoBloqueio = codTipoBloqueio;


            OrigemLegado    = false;

        }



        //Fail Fast Validations
        public void Validar()
        {

            AddNotifications(new Contract()

                .Requires()


                //CodEmpresa
                .IsNotNullOrEmpty(CodEmpresa, "Cod. Empresa", "O código deve ser informado")
                .HasLen(CodEmpresa, 4, "Cod. Empresa", "O código deve possuir 04 caracteres")


                //CodCentroCusto
                .IsNotNull(CodCentroCusto, "Cod. Centro Custo", "O Cód. Centro Custo deve ser informado")


                //Nome Centro Custo
                .IsNotNullOrEmpty(NomeCentroCusto, "Nome", "O nome do Centro de Custo deve ser informado")
                .HasMinLen(NomeCentroCusto, 3, "Nome", "O nome do Centro de Custo deve possuir no mínimo 3 caracteres")
                .HasMaxLen(NomeCentroCusto, 200, "Nome", "O nome do Centro de Custo deve possuir no máximo 200 caracteres")


                //CodGrupo
                .IsNotNullOrEmpty(CodGrupo, "Cod. Grupo", "O Cód. do Grupo deve ser informado")
                .HasMinLen(CodGrupo, 2, "Cod. Grupo", "O Cód. do Grupo deve possuir no mínimo 2 caracteres")
                .HasMaxLen(CodGrupo, 3, "Cod. Grupo", "O Cód. do Grupo deve possuir no máximo 3 caracteres")


                //CodClasse
                .IsNotNull(CodClasse, "Cod. Classe", "O Cód. Classe deve ser informado")


                //Cod Tipo Bloqueio
                .IsNotNull(CodTipoBloqueio, "Cod. Tipo Bloqueio", "O Cód. Tipo Bloqueio deve ser informado")

                );
        }
    }
}

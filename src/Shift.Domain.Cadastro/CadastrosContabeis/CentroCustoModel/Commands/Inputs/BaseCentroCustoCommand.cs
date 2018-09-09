#region usings

using System;
using Flunt.Notifications;

#endregion

namespace Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel.Commands.Inputs
{
    public class BaseCentroCustoCommand : Notifiable
    {

        public string   CodEmpresa          { get; protected set; }


        public long     CodCentroCusto      { get; protected set; }


        public string   NomeCentroCusto     { get; protected set; }


        public bool     OrigemLegado        { get; protected set; }


        public string   CodGrupo            { get; protected set; }


        public int      CodClasse           { get; protected set; }


        public int      CodTipoBloqueio     { get; protected set; }

    }
}

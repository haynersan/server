#region usings

using System;
using Shift.Domain.Core.Interfaces;

#endregion

namespace Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel.Commands.Results
{
    public class CentroCustoCommandResult : ICommandResult
    {

        public Guid     Id                  { get; set; }

        public string   CodEmpresa          { get; set; }

        public long     CodCentroCusto      { get; set; }

        public string   NomeCentroCusto     { get; set; }

        public bool     OrigemLegado        { get; set; }

        public string   CodGrupo            { get; set; }

        public string   Grupo               { get; set; }

        public int      CodClasse           { get; set; }

        public string   ClasseContabil      { get; set; }

        public int      CodTipoBloqueio     { get; set; }

        public string   TipoBloqueio        { get; set; }
    }
}

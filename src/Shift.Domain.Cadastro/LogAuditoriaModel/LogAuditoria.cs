#region usings

using System;
using Shift.Domain.Core.Models;

#endregion


namespace Shift.Domain.Cadastro.LogAuditoriaModel
{
    public class LogAuditoria : Entity<LogAuditoria>
    {


        //Construtor Necessário para atender a uma demanda do Entity Framework
        protected LogAuditoria() {}



        public LogAuditoria(string schema, string tabela, Enum acao, string modulo, string jsonResult, Guid userIdLogado)
        {

            Id              = Guid.NewGuid();

            DataOperacao    = DateTime.Now;

            Schema          = schema;

            Tabela          = tabela;

            Acao            = acao.ToString();

            Modulo          = modulo;

            JsonResult      = jsonResult;

            UserIdLogado    = userIdLogado;

        }


        #region Propriedades

        public DateTime     DataOperacao    { get; private set; }


        public string       Schema          { get; private set; }


        public string       Tabela          { get; private set; }


        public string       Acao            { get; private set; }


        public string       Modulo          { get; private set; }


        public string       JsonResult      { get; private set; }


        public Guid         UserIdLogado    { get; private set; }

        #endregion
    }
}

#region usings

using System;
using Flunt.Notifications;

#endregion


namespace Shift.Domain.Core.Models
{

    //Classes abstract não podem ser instanciadas. Apenas herdadas
    public abstract class Entity<T> : Notifiable where T : Entity<T>
    {

        #region Propriedades

        public Guid     Id          { get; protected set; }

        public string   CodEmpresa  { get; protected set; }

        public int      IdSituacao  { get; protected set; }

        public bool     Excluido    { get; protected set; }

        #endregion

    }
}

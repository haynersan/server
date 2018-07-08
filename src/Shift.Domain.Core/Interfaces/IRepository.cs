#region usings

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Shift.Domain.Core.Models;

#endregion

namespace Shift.Domain.Core.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
    {

        #region Escrita

        void Adicionar(TEntity obj);



        void Atualizar(TEntity obj);



        void RemoverGuid(Guid id);


        void RemoverString(string codigo);

        #endregion



        #region Leitura

        TEntity ObterPorId(Guid id);



        IEnumerable<TEntity> ObterTodos();



        IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
        
        #endregion


        int SaveChanges();
    }
}

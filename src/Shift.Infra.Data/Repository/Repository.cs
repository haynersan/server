#region usings

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shift.Domain.Core.Interfaces;
using Shift.Domain.Core.Models;
using Shift.Infra.Data.Context;

#endregion

namespace Shift.Infra.Data.Repository
{

   

    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {


        #region Configuracao

        protected ShiftContext Db;


        protected DbSet<TEntity> DbSet;


        public Repository(ShiftContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        #endregion



        #region Escrita
        //Quando os métodos possui o modificador "virtual" em sua assinatura, permite que eles sejam 
        //sobrescritos em uma classe específica;

        public virtual void Adicionar(TEntity obj)
        {
            DbSet.Add(obj);
        }


        public virtual void Atualizar(TEntity obj)
        {
            DbSet.Update(obj);
        }


        public virtual void RemoverGuid(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }


        public virtual void RemoverString(string codigo)
        {
            DbSet.Remove(DbSet.Find(codigo));
        }


        #endregion



        #region Leitura

        //Conceito: 
        // - IEnumerable => Realiza filtros no lado do Cliente
        // - IQueryable  => Realiza filtros no lado do Banco de Dados

        public IEnumerable<TEntity> ObterTodos()
        {
            return DbSet.ToList();
        }


        public IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            //AsNoTracking trás benefícios de performance;
            return DbSet.AsNoTracking().Where(predicate);
        }


        public TEntity ObterPorId(Guid id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }




        #endregion


        #region Validacoes

        public virtual bool ExisteRelacionamento(string nameFK, string keyValue)
        {
            return Db.Database.GetDbConnection().Query<bool>("[Relacao].[SP_RelacaoChecar]",
                   new
                   {
                       NAME_FK      = nameFK,

                       KEY_VALUE    = keyValue

                   },
                   commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        #endregion


        #region Fechamento

        public int SaveChanges()
        {
            //Sempre retorna um "int" (número de linhas afetadas)
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }




        #endregion



      

    }
}

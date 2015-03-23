using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using Crucial.Framework.Extensions;
using Crucial.Framework.Enums;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Crucial.Framework.Data.EntityFramework
{
    public interface IDbContextAsync : IDbContext
    {
        Task<int> SaveChangesAsync();
    }

    public interface IDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbSet Set(Type entityType);
        IEnumerable<DbEntityValidationResult> GetValidationErrors();

        void SetState<TEntity>(TEntity entityItem, EntityState state) where TEntity : Crucial.Framework.BaseEntities.ProviderEntityBase;
        DbEntityEntry Entry(object entity);
        int SaveChanges();
    }

    public abstract class BaseRepository<TContext, TEntity, TKey> :
            Framework.DesignPatterns.Repository.ICreateRepository<TEntity, TKey>,
            Framework.DesignPatterns.Repository.IDeleteRepository<TKey>,
            Framework.DesignPatterns.Repository.IUpdateRepository<TEntity>,
            Framework.DesignPatterns.Repository.IQueryableRepository<TEntity>
        where TContext : IDbContext
        where TEntity : Crucial.Framework.BaseEntities.ProviderEntityBase
        where TKey : Crucial.Framework.BaseEntities.ProviderEntityBase
    {
        protected TContext Context;

        public BaseRepository()
        {
            Context = new ContextProvider<TContext>().DbContext;
        }

        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] include)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            foreach (var inc in include)
                query = query.Include(inc);
            return query.Where(predicate);
        }

        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().Where(predicate);
            return query;
        }

        public IQueryable<TEntity> Page<TSortType>(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize, Expression<Func<TEntity, TSortType>> orderByProperty, Framework.Enums.SortOrder sortOrder, out int rowCount)
        {
            return Context.Set<TEntity>().Where(predicate).Page(pageIndex, pageSize, orderByProperty, sortOrder == Framework.Enums.SortOrder.Ascending, out rowCount);
        }

        public IQueryable<TEntity> Page<TSortType>(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize, Expression<Func<TEntity, TSortType>> orderByProperty, SortOrder sortOrder, out int rowCount, params Expression<Func<TEntity, object>>[] include)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().Where(predicate).Page(pageIndex, pageSize, orderByProperty, sortOrder == SortOrder.Ascending, out rowCount);

            foreach (var inc in include)
                query = query.Include(inc);

            return query.ToList().AsQueryable();
        }

        public IQueryable<TEntity> Page<TSortType, TThenSortType>(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize, Expression<Func<TEntity, TSortType>> orderByProperty, SortOrder sortOrder, Expression<Func<TEntity, TThenSortType>> thenByProperty, SortOrder thenSortOrder, out int rowCount)
        {
            return Context.Set<TEntity>().Where(predicate).Page(pageIndex, pageSize, orderByProperty, sortOrder == SortOrder.Ascending, thenByProperty, thenSortOrder == SortOrder.Ascending, out rowCount);
        }

        public IQueryable<TEntity> Page<TSortType, TThenSortType>(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize, Expression<Func<TEntity, TSortType>> orderByProperty, SortOrder sortOrder, Expression<Func<TEntity, TThenSortType>> thenByProperty, SortOrder thenSortOrder, out int rowCount, params Expression<Func<TEntity, object>>[] include)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().Where(predicate).Page(pageIndex, pageSize, orderByProperty, sortOrder == SortOrder.Ascending, thenByProperty, thenSortOrder == SortOrder.Ascending, out rowCount);

            foreach (var inc in include)
                query = query.Include(inc);

            return query.ToList().AsQueryable();
        }


        public TKey Create(TEntity entity)
        {
            var output = Context.Set<TEntity>();
            TEntity result = output.Add(entity);

            try
            {
                Context.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                //CrucialLogger.LogException(ex);
                throw new Exception("Update exception, see inner exception for details", ex);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach(var er in ex.EntityValidationErrors)
                {
                    sb.Append(er.ToString());
                }
                //CrucialLogger.LogException(ex);
                throw new Exception("EF Validation failed, see inner exception for details:" + sb.ToString(), ex);
            }

            return result as TKey;
        }

        public bool Delete(TKey entity)
        {
            Context.Set<TKey>().Remove(entity);

            try
            {
                Context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                //CrucialLogger.LogException(ex);
                throw new Exception("EF Validation failed, see inner exception for details", ex);
            }

            return true;
        }

        public bool Update(TEntity entity)
        {
            Context.SetState(entity, EntityState.Modified);
            //Context.Entry(entity).State = EntityState.Modified;

            try
            {
                Context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                //CrucialLogger.LogException(ex);

                Context.Entry(entity).State = EntityState.Unchanged;
                throw new Exception("EF Validation failed, see inner exception for details", ex);
            }

            return true;
        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }
    }
}

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
using System.Threading;
using Crucial.Framework.Data.EntityFramework;
using System.Diagnostics.CodeAnalysis;
using Crucial.Framework.Logging;

namespace Crucial.Framework.Data.EntityFramework.Async
{
    /// <summary>
    /// Represents a repository of items.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type">type</see> of item in the repository.</typeparam>
    public abstract class BaseRepository<TContext, TEntity, TKey> :
            Crucial.Framework.DesignPatterns.Repository.Async.IReadOnlyRepository<TEntity>
        where TContext : IDbContextAsync
        where TEntity : Crucial.Framework.BaseEntities.ProviderEntityBase
        where TKey : Crucial.Framework.BaseEntities.ProviderEntityBase
    {
        protected TContext Context;
        private ILogger _logger;

        protected BaseRepository(IContextProvider<TContext> contextProvider, ILogger logger)
        {
            _logger = logger;
            Context = contextProvider.DbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryShaper, CancellationToken cancellationToken)
        {
            var query = queryShaper(Context.Set<TEntity>());
            return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        }


        public async Task<TResult> GetAsync<TResult>(Func<IQueryable<TEntity>, TResult> queryShaper, CancellationToken cancellationToken)
        {
            var factory = Task<TResult>.Factory;
            return await factory.StartNew(() => queryShaper(Context.Set<TEntity>()), cancellationToken).ConfigureAwait(false);
        }

        public async Task<TKey> Create(TEntity entity)
        {
            var output = Context.Set<TEntity>();
            TEntity result = output.Add(entity);
            
            try
            {
                await Context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                _logger.LogException(ex);
                throw;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();
                
                foreach (var er in ex.EntityValidationErrors)
                {
                    sb.Append(er.ToString());
                }
                
                _logger.Fatal(sb.ToString());
                _logger.LogException(ex);
                throw;
            }
            
            return result as TKey;
        }

        public async Task<bool> Delete(TKey entity)
        {
            Context.Set<TKey>().Remove(entity);

            try
            {
                await Context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                _logger.LogException(ex);
                throw new Exception("EF Validation failed, see inner exception for details", ex);
            }

            return true;
        }

        public async Task<bool> Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                _logger.LogException(ex);

                Context.Entry(entity).State = EntityState.Unchanged;
                throw;
            }

            return true;
        }
    }
}

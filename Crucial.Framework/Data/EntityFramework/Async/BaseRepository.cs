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
        private readonly ILogger _logger;

        protected BaseRepository(IContextProvider<TContext> contextProvider, ILogger logger)
        {
            _logger = logger;
            Context = contextProvider.DbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryShaper, CancellationToken cancellationToken)
        {
            _logger.Trace(String.Format("Get GetAsync In:IQ<{0}> Out:IQ{0} : Begin", typeof(TEntity).FullName));

            var query = queryShaper(Context.Set<TEntity>());
            var output = await query.ToListAsync(cancellationToken).ConfigureAwait(false);

            _logger.Trace(String.Format("Get GetAsync IQ<{0}> in IQ{0} out: Begin", typeof(TEntity).FullName));

            return output;
        }


        public async Task<TResult> GetAsync<TResult>(Func<IQueryable<TEntity>, TResult> queryShaper, CancellationToken cancellationToken)
        {
            _logger.Trace(String.Format("Get GetAsync In:IQ<{0}> Out:{1} : Begin", typeof(TEntity).FullName, typeof(TResult).FullName));

            var factory = Task<TResult>.Factory;
            var output = await factory.StartNew(() => queryShaper(Context.Set<TEntity>()), cancellationToken).ConfigureAwait(false);

            _logger.Trace(String.Format("Get GetAsync In:IQ<{0}> Out:{1} : End", typeof(TEntity).FullName, typeof(TResult).FullName));

            return output;
        }

        public async Task<TKey> Create(TEntity entity)
        {
            _logger.Trace(String.Format("Create {0}: Begin", typeof(TEntity).FullName));

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
            
            _logger.Trace(String.Format("Create {0}: End", typeof(TEntity).FullName, result));

            return result as TKey;
        }

        public async Task<bool> Delete(TKey entity)
        {
            _logger.Trace(String.Format("Delete {0}: Begin", typeof(TEntity).FullName));

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

            _logger.Trace(String.Format("Delete {0}: End", typeof(TEntity).FullName));

            return true;
        }

        public async Task<bool> Update(TEntity entity)
        {
            _logger.Trace(String.Format("Update {0}: Begin", typeof(TEntity).FullName));
            
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

            _logger.Trace(String.Format("Update {0}: End", typeof(TEntity).FullName));

            return true;
        }

    }
}

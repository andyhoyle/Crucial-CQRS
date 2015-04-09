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
using System.Runtime.Remoting.Contexts;
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
        private ILogger _logger;
        private readonly IContextProvider<TContext> _contextProvider;

        protected BaseRepository(IContextProvider<TContext> contextProvider, ILogger logger)
        {
            _contextProvider = contextProvider;
            _logger = logger;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryShaper, CancellationToken cancellationToken)
        {
            using (var context = _contextProvider.DbContext)
            {
                var query = queryShaper(context.Set<TEntity>());
                return await query.ToListAsync(cancellationToken).ConfigureAwait(false);   
            }
        }


        public async Task<TResult> GetAsync<TResult>(Func<IQueryable<TEntity>, TResult> queryShaper, CancellationToken cancellationToken)
        {
            var factory = Task<TResult>.Factory;

            using (var context = _contextProvider.DbContext)
            {
                return
                    await
                        factory.StartNew(() => queryShaper(context.Set<TEntity>()), cancellationToken)
                            .ConfigureAwait(false);
            }
        }

        public async Task<TKey> Create(TEntity entity)
        {
            using (var context = _contextProvider.DbContext)
            {
                var output = context.Set<TEntity>();
                TEntity result = output.Add(entity);

                try
                {
                    await context.SaveChangesAsync().ConfigureAwait(false);
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
        }

        public async Task<bool> Delete(TKey entity)
        {
            using (var context = _contextProvider.DbContext)
            {
                context.Set<TKey>().Attach(entity);
                context.Entry(entity).State = EntityState.Deleted;

                try
                {
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    _logger.LogException(ex);
                    throw new Exception("EF Validation failed, see inner exception for details", ex);
                }

                return true;
            }
        }

        public async Task<bool> Update(TEntity entity)
        {
            using (var context = _contextProvider.DbContext)
            {
                context.Entry(entity).State = EntityState.Modified;

                try
                {
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    _logger.LogException(ex);

                    context.Entry(entity).State = EntityState.Unchanged;
                    throw;
                }

                return true;
            }
        }
    }
}

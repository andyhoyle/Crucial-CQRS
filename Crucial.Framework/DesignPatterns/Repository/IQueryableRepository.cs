using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using Crucial.Framework.BaseEntities;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.Repository
{
    [ServiceContract]
    public interface IQueryableRepository<TEntity>
        where TEntity : ProviderEntityBase
    {
        [OperationContract]
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        [OperationContract]
        IQueryable<TEntity> Page<TSortType>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize, Expression<Func<TEntity, TSortType>> orderByProperty, Framework.Enums.SortOrder sortOrder, out int rowCount);

        [OperationContract]
        IQueryable<TEntity> Page<TSortType, TThenSortType>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize, Expression<Func<TEntity, TSortType>> orderByProperty, Framework.Enums.SortOrder sortOrder, Expression<Func<TEntity, TThenSortType>> thenByProperty, Framework.Enums.SortOrder thenSortOrder, out int rowCount);

        [OperationContract]
        IQueryable<TEntity> Page<TSortType, TThenSortType>(Expression<Func<TEntity, bool>> predicate, int pageIndex,
                                                           int pageSize,
                                                           Expression<Func<TEntity, TSortType>> orderByProperty,
                                                           Framework.Enums.SortOrder sortOrder,
                                                           Expression<Func<TEntity, TThenSortType>> thenByProperty,
                                                           Framework.Enums.SortOrder thenSortOrder,
                                                           out int rowCount,
                                                           params Expression<Func<TEntity, object>>[] include);

        [OperationContract]
        IQueryable<TEntity> Page<TSortType>(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize,
                                            Expression<Func<TEntity, TSortType>> orderByProperty,
                                            Framework.Enums.SortOrder sortOrder, out int rowCount,
                                            params Expression<Func<TEntity, object>>[] include);

        [OperationContract]
        IQueryable<TEntity> FindBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params System.Linq.Expressions.Expression<Func<TEntity, object>>[] include);
    }
}

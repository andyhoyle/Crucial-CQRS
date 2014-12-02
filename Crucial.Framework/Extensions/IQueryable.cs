using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Crucial.Framework.Extensions
{
    public static class IQueryable
    {
        public static bool HasMore<TSource>(this IQueryable<TSource> source, int pageIndex, int pageSize)
        {
            if (pageIndex < 0)
            {
                throw new ArgumentException("pageIndex must be greater than or equal to zero");
            }

            if (pageSize < 1)
            {
                throw new ArgumentException("pageSize must be greater than or equal to one");
            }

            return source.PageCount<TSource>(pageSize) > pageIndex;
        }

        public static int PageCount<TSource>(this IQueryable<TSource> source, int pageSize)
        {
            if (pageSize < 1)
            {
                throw new ArgumentException("pageSize must be greater than or equal to one");
            }

            return (int)Math.Ceiling((decimal)source.Count() / (decimal)pageSize);
        }

        public static IQueryable<T> Page<T, TResult>(this IQueryable<T> query,
                        int pageNum, int pageSize,
                        Expression<Func<T, TResult>> orderByProperty,
                        bool isAscendingOrder, out int rowsCount)
        {
            if (pageSize <= 0) pageSize = 20;

            rowsCount = query.Count();

            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;

            int excludedRows = (pageNum - 1) * pageSize;

            if (isAscendingOrder)
                query = query.OrderBy(orderByProperty);
            else
                query = query.OrderByDescending(orderByProperty);

            return query.Skip(excludedRows).Take(pageSize);
        }

        public static IQueryable<T> Page<T, TResult, TThenResult>(this IQueryable<T> query,
                        int pageNum, int pageSize,
                        Expression<Func<T, TResult>> orderByProperty,
                        bool orderByAscendingOrder,
                        Expression<Func<T, TThenResult>> thenByProperty,
                        bool thenByAscendingOrder,
                        out int rowsCount)
        {
            if (pageSize <= 0) pageSize = 20;

            rowsCount = query.Count();

            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;

            int excludedRows = (pageNum - 1) * pageSize;

            if (orderByAscendingOrder)
            {
                if (thenByAscendingOrder)
                {
                    query = query.OrderBy(orderByProperty).ThenBy(thenByProperty);
                }
                else
                {
                    query = query.OrderBy(orderByProperty).ThenByDescending(thenByProperty);
                }
            }
            else
            {
                if (thenByAscendingOrder)
                {
                    query = query.OrderByDescending(orderByProperty).ThenBy(thenByProperty);
                }
                else
                {
                    query = query.OrderByDescending(orderByProperty).ThenByDescending(thenByProperty);
                }
            }

            return query.Skip(excludedRows).Take(pageSize);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.Repository.Async
{
    public interface IReadOnlyRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves all items in the repository satisfied by the specified query asynchronously.
        /// </summary>
        /// <param name="queryShaper">The <see cref="Func{T,TResult}">function</see> that shapes the <see cref="IQueryable{T}">query</see> to execute.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken">cancellation token</see> that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Task{T}">task</see> containing the retrieved <see cref="IEnumerable{T}">sequence</see>
        /// of <typeparamref name="T">items</typeparamref>.</returns>
        /// <example>The following example demonstrates how to paginate the items in a repository using an example query.
        /// <code><![CDATA[
        /// using Microsoft.DesignPatterns.Examples;
        /// using System;
        /// using System.Collections.Generic;
        /// using System.Linq;
        /// using System.Threading;
        /// using System.Threading.Tasks;
        /// 
        /// public async static void Main()
        /// {
        ///     var index = 0;
        ///     var repository = new MyRepository();
        ///     var cancellationToken = new CancellationToken();
        ///     
        ///     foreach ( var item in await repository.GetAsync( q => q.Where( i => i.FirstName.StartsWith( "Jo" ) ).OrderBy( i => i.LastName ), cancellationToken );
        ///         Console.WriteLine( i => i.ToString() );
        /// }
        /// ]]>
        /// </code></example>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required for generics support.")]
        Task<IEnumerable<T>> GetAsync(Func<System.Linq.IQueryable<T>, System.Linq.IQueryable<T>> queryShaper, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a query result asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The <see cref="Type">type</see> of result to retrieve.</typeparam>
        /// <param name="queryShaper">The <see cref="Func{T,TResult}">function</see> that shapes the <see cref="IQueryable{T}">query</see> to execute.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken">cancellation token</see> that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Task{T}">task</see> containing the <typeparamref name="TResult">result</typeparamref> of the operation.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required for generics support.")]
        Task<TResult> GetAsync<TResult>(Func<System.Linq.IQueryable<T>, TResult> queryShaper, CancellationToken cancellationToken);
    }
}

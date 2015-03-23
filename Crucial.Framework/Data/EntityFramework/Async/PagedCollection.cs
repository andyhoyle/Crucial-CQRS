using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Framework.Data.EntityFramework.Async
{
    /// <summary>
    /// Represents a read-only collection of paged items.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type">item</see> of item returned by the result.</typeparam>
    public class PagedCollection<T> : ReadOnlyObservableCollection<T>
    {
        private long totalCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedCollection{T}"/> class.
        /// </summary>
        /// <param name="sequence">The <see cref="IEnumerable{T}"/> containing the current data page of items.</param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Validated by a code contract.")]
        protected PagedCollection(IEnumerable<T> sequence)
            : this(new ObservableCollection<T>(sequence))
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedCollection{T}"/> class.
        /// </summary>
        /// <param name="collection">The <see cref="ObservableCollection{T}"/> containing the current data page of items.</param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Validated by a code contract.")]
        protected PagedCollection(ObservableCollection<T> collection)
            : this(collection, collection.Count)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedCollection{T}"/> class.
        /// </summary>
        /// <param name="sequence">The <see cref="IEnumerable{T}"/> containing the current data page of items.</param>
        /// <param name="totalCount">The total number of items.</param>
        public PagedCollection(IEnumerable<T> sequence, long totalCount)
            : this(new ObservableCollection<T>(sequence), totalCount)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedCollection{T}"/> class.
        /// </summary>
        /// <param name="collection">The <see cref="ObservableCollection{T}"/> containing the current data page of items.</param>
        /// <param name="totalCount">The total number of items.</param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Validated by a code contract.")]
        public PagedCollection(ObservableCollection<T> collection, long totalCount)
            : base(collection)
        {
            this.totalCount = totalCount;
        }

        /// <summary>
        /// Gets or sets the total count of all items.
        /// </summary>
        /// <value>The total count of all items.</value>
        public long TotalCount
        {
            get
            {
                return this.totalCount;
            }
            protected set
            {
                this.totalCount = value;
            }
        }
    }
}

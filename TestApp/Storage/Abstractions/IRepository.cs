using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Storage.Abstractions
{
    /// <summary>
    /// IRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<T, in TKey> where T : IBaseEntity<TKey>
    {
        /// <summary>
        /// Get items
        /// </summary>
        IQueryable<T> Items { get; }

        /// <summary>
        /// Gets items with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<T> ItemsAsNoTracking { get; }

        /// <summary>
        /// Check any
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Return all elements from db
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Collection</returns>
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Find entity by identifier
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Entity with specified identifier
        /// </returns>
        Task<T> FindAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Entity with passed id
        /// </returns>
        Task<T> GetByIdAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Insert specified item to context
        /// </summary>
        /// <param name="entity">Item to insert</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Insert all specified entities to context
        /// </summary>
        /// <param name="entities">Items to insert</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update specified entity in context
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete specified entity
        /// </summary>
        /// <param name="collection">Entity to update</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task DropCollection(string collection, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete specified entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete entity on context save
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Count of removed items</returns>
        Task<long> DeleteManyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default(CancellationToken));

    }
}

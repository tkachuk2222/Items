using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Abstractions;

namespace Storage
{
    /// <summary>
    /// Abstract repository that works in any context
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public abstract class BaseRepository<T, TKey> : IRepository<T, TKey> where T : class, IBaseEntity<TKey>
    {
        #region Props

        /// <summary>
        ///     Gets or sets the db context.
        /// </summary>
        /// <value>
        ///     The context.
        /// </value>
        protected abstract DbContext Context { get; set; }


        private Dictionary<Type, Action<T, T>> CompiledUpdateExpressions { get; }

        #region Implements IRepository<T>

        public IQueryable<T> Items => Context.Set<T>();

        /// <inheritdoc />
        public IQueryable<T> ItemsAsNoTracking => Context.Set<T>().AsNoTracking();

        /// <inheritdoc />
        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Context.Set<T>().ToListAsync(cancellationToken);
        }
        /// <inheritdoc />
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Context.Set<T>().Where(filter).ToListAsync(cancellationToken);
        }
        /// <inheritdoc />
        public async Task<T> FindAsync(Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Context.Set<T>().FirstOrDefaultAsync(filter, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<T> GetByIdAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Context.Set<T>().FirstOrDefaultAsync(arg => arg.Id.Equals(id), cancellationToken);
        }
        /// <inheritdoc />
        public async Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Context.Set<T>().AddAsync(entity, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
            return entity;
        }
        /// <inheritdoc />
        public async Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var enumerated = entities.ToList();
            await Context.Set<T>().AddRangeAsync(enumerated, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
            return enumerated;
        }
        /// <inheritdoc />
        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entityToUpdate = await Context.Set<T>()
                .SingleAsync(e => e.Id.Equals(entity.Id), cancellationToken: cancellationToken);
            ExpressionUpdate(entityToUpdate, entity);
            await Context.SaveChangesAsync(cancellationToken);
            return entity;
        }
        /// <inheritdoc />
        public Task DropCollection(string collection, CancellationToken cancellationToken = default(CancellationToken))
        {

            throw new NotImplementedException();
        }
        /// <inheritdoc />
        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            Context.Set<T>().RemoveRange(entity);
            await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <inheritdoc />
        public async Task<long> DeleteManyAsync(Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = Context.Set<T>().Where(filter);
            Context.Set<T>().RemoveRange(entities);
            return await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.FromResult(Context.Set<T>().Any(filter));
        }


        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="EfEfRepository{T}" /> class.
        /// </summary>
        protected BaseRepository()
        {
            CompiledUpdateExpressions = new Dictionary<Type, Action<T, T>>();
        }

        #endregion

        #region Public

        #region Implements IRepository<T>

        #endregion

        #endregion

        #region Private

        private void ExpressionUpdate(T old, T updated)
        {
            if (!CompiledUpdateExpressions.ContainsKey(typeof(T)))
                CompiledUpdateExpressions.Add(typeof(T), GetUpdateExpression());
            CompiledUpdateExpressions[typeof(T)]?.Invoke(old, updated);
        }

        private Action<T, T> GetUpdateExpression()
        {
            var oldItem = Expression.Parameter(typeof(T), "currentItem");
            var updatedItem = Expression.Parameter(typeof(T), "updatedItem");
            var properties = typeof(T).GetProperties();

            var resultExpression = new List<Expression>();

            foreach (var property in properties)
                if (property.CanWrite)
                {
                    var notEquals = Expression.NotEqual(Expression.Property(oldItem, property),
                        Expression.Property(updatedItem, property));
                    var ifTrue = Expression.Assign(Expression.Property(oldItem, property),
                        Expression.Property(updatedItem, property));
                    var ifExpr = Expression.IfThen(notEquals, ifTrue);
                    resultExpression.Add(ifExpr);
                }

            var lambda = Expression.Lambda<Action<T, T>>(Expression.Block(resultExpression), oldItem, updatedItem);
            var compiled = lambda.Compile();
            return compiled;
        }
        #endregion
    }
}

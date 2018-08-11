using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Abstractions;

namespace Storage
{
    /// <inheritdoc/>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <seealso cref="IRepository{T,TKey}" />
    public class EfRepository<T, TKey> : BaseRepository<T, TKey> where T : class, IBaseEntity<TKey>
    {
        #region Props

        /// <inheritdoc />
        protected sealed override DbContext Context { get; set; }

        #endregion

        #region Ctor

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        public EfRepository(DbContext context)
        {
            Context = context;
        }

        #endregion
    }
}

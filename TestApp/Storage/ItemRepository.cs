using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Storage.Abstractions;
using Storage.Models;

namespace Storage
{
    /// <summary>
    /// ItemRepository
    /// </summary>
    public class ItemRepository : BaseRepository<ItemEntity, int>, IItemRepository
    {
        #region Props

        /// <summary>
        /// Context
        /// </summary>
        protected sealed override DbContext Context { get; set; }
   
        #endregion

        /// <summary>
        /// the ctor
        /// </summary>
        /// <param name="context"></param>
        public ItemRepository(DbContext context)
        {
            Context = context;
        }
    }
}

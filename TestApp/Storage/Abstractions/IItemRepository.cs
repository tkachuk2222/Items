using System;
using System.Collections.Generic;
using System.Text;
using Storage.Models;

namespace Storage.Abstractions
{
    /// <summary>
    /// IItemRepository
    /// </summary>
    public interface IItemRepository:IRepository<ItemEntity, int>
    {
    }
}

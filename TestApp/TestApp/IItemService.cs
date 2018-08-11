using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Storage.Models;
using TestApp.Models;

namespace TestApp
{
    /// <summary>
    /// IItemService
    /// </summary>
    public interface IItemService
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ItemDto> Create(ItemDto dto);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ItemDto> Update(ItemDto dto);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ItemDto> Delete(int id);
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ItemDto>> GetAll();
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ItemDto> GetById(int id);

        /// <summary>
        /// Get statistic
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<StatisticDto>> GetStatistic();
    }
}
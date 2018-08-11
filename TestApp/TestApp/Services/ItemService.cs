using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Storage.Abstractions;
using Storage.Factories;
using Storage.Models;
using TestApp.Models;

namespace TestApp.Services
{
    /// <summary>
    /// ItemService
    /// </summary>
    public class ItemService : IItemService
    {
        #region fields

        private readonly IItemRepository _repository;

        #endregion

        #region ctor

        /// <summary>
        /// the ctor
        /// </summary>
        /// <param name="repository"></param>
        public ItemService(IItemRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region public 

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ItemDto> Create(ItemDto dto)
        {
            var entity = ItemFactory.CreateEntity(dto);
            var res = await _repository.InsertAsync(entity);
            return ItemFactory.CreateDto(res);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ItemDto> Update(ItemDto dto)
        {
            var itemInDb = await _repository.FindAsync(e => e.Id == dto.Id);
            var updated = new ItemEntity();
            if (itemInDb != null)
            {
                itemInDb.ItemName = dto.ItemName;
                itemInDb.ItemType = dto.ItemType;

                updated = await _repository.UpdateAsync(itemInDb);
            }


            return ItemFactory.CreateDto(updated);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ItemDto> Delete(int id)
        {
            var finded = await _repository.FindAsync(e => e.Id == id);
            var deleted = new ItemEntity();
            if (finded != null)
            {
                await _repository.DeleteAsync(finded);
            }

            return ItemFactory.CreateDto(finded);
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ItemDto>> GetAll()
        {
            var allItems = await _repository.GetAllAsync();

            return ItemFactory.CreateDto(allItems);
        }

        public async Task<ItemDto> GetById(int id)
        {
            var res = await _repository.FindAsync(e => e.Id == id);

            return ItemFactory.CreateDto(res);
        }

        /// <summary>
        /// GetStatistic
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StatisticDto>> GetStatistic()
        {
            var allItems = await _repository.GetAllAsync();
            var statistic = new Dictionary<string, int>();
            foreach (var item in allItems)
            {
                if (statistic.ContainsKey(item.ItemType))
                {
                    statistic[item.ItemType]++;
                }
                else
                {
                    statistic.Add(item.ItemType, 1);
                }
            }

            var statRes = new List<StatisticDto>();
            foreach (var item in statistic)
            {
                statRes.Add(new StatisticDto() {ItemType = item.Key, Count = item.Value});
            }

            return statRes;
        }

        #endregion
    }
}
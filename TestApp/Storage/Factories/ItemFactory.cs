using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Storage.Models;

namespace Storage.Factories
{
    /// <summary>
    /// ItemFactory
    /// </summary>
    public class ItemFactory
    {
        /// <summary>
        /// CreateDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ItemDto CreateDto(ItemEntity entity) => new ItemDto()
        {
            Id = entity.Id,
            ItemName = entity.ItemName,
            ItemType = entity.ItemType
        };

        /// <summary>
        /// CreateDto
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static IEnumerable<ItemDto> CreateDto(IEnumerable<ItemEntity> entities) => entities.Select(CreateDto);

        /// <summary>
        /// CreateEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ItemEntity CreateEntity(ItemDto dto) => new ItemEntity()
        {
            Id = dto.Id,
            ItemName = dto.ItemName,
            ItemType = dto.ItemType
        };

        /// <summary>
        /// CreateEntity
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static IEnumerable<ItemEntity> CreateEntity(IEnumerable<ItemDto> entities) =>
            entities.Select(CreateEntity);
    }
}
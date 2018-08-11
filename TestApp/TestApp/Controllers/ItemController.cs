using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Storage.Factories;
using Storage.Models;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    /// <summary>
    /// ItemController
    /// </summary>
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        #region fields

        private readonly IItemService _service;

        #endregion


        #region ctor

        /// <summary>
        /// the ctor
        /// </summary>
        /// <param name="service"></param>
        public ItemController(IItemService service)
        {
            _service = service;
        }

        #endregion

        #region public

        /// <summary>
        /// CreateNewItem
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateNewItem([FromBody] CreateItemRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.Create(ItemViewFactory.Create(model));
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(ItemFactory.CreateEntity(result));
        }

        /// <summary>
        /// GetAItems
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAItems()
        {
            var res = await _service.GetAll();

            return Ok(res);
        }

        /// <summary>
        /// GetItem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem([FromRoute] int id)
        {
            var res = await _service.GetById(id);

            return Ok(res);
        }

        /// <summary>
        /// DeleteItem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var res = await _service.Delete(id);
            return Ok(res);
        }

        /// <summary>
        /// UpdateItem
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int id, [FromBody] ItemDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.Id = id;
            var result = await _service.Update(model);
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(ItemFactory.CreateEntity(result));
        }

        /// <summary>
        /// GetStatistic
        /// </summary>
        /// <returns></returns>
        [Route("statistic")]
        [HttpGet]
        public async Task<IActionResult> GetStatistic()
        {
            var stat = await _service.GetStatistic();

            return Ok(stat);
        }

        #endregion
    }
}
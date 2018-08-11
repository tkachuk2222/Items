using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Storage.Models;

namespace TestApp.ViewModels
{
    public class ItemViewFactory
    {
        public static ItemDto Create(CreateItemRequest createItemRequest) => new ItemDto()
        {
            ItemName = createItemRequest.ItemName,
            ItemType = createItemRequest.ItemType
        };

    }
}

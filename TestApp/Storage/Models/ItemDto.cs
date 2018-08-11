using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Models
{
    /// <summary>
    /// ItemDto
    /// </summary>
    public class ItemDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Item name
        /// </summary>
        public string ItemName { set; get; }

        /// <summary>
        /// Item type
        /// </summary>
        public string ItemType { set; get; }
    }
}

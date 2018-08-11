using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.ViewModels
{
    /// <summary>
    /// CreateItemRequest
    /// </summary>
    public class CreateItemRequest
    {
        /// <summary>
        /// ItemName
        /// </summary>
        [Required]
        public string ItemName { set; get; }

        /// <summary>
        /// ItemType
        /// </summary>
        [Required]
        public string ItemType { set; get; }
    }
}
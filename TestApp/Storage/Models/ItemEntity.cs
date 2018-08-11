using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Storage.Abstractions;

namespace Storage.Models
{
    /// <summary>
    /// ItemEntity
    /// </summary>
    public class ItemEntity : IBaseEntity<int>
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
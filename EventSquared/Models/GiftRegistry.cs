using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventSquared.Models
{
    public class GiftRegistry
    {
        public int GiftRegistryId { get; set; }

        [MaxLength(200)]
        [Required]
        public string Title { get; set; }

        public ICollection<Gift> Gifts { get; set; }
    }
}
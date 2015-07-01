using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventSquared.Models
{
    public class Gift
    {
        public int GiftId { get; set; }

        [MaxLength(200)]
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Selectable { get; set; }
    }
}
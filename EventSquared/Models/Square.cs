using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventSquared.Models
{
    public class Square
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [DisplayName("Start Time")]
        [DisplayFormat(DataFormatString = "{0:HH:mm tt}",
            ApplyFormatInEditMode = true)]
        [Required]
        public DateTime StartTime { get; set; }

        [DisplayName("End Time")]
        [DisplayFormat(DataFormatString = "{0:HH:mm tt}",
            ApplyFormatInEditMode = true)]
        [Required]
        public DateTime EndTime { get; set; }

        [MaxLength(200)]
        public string Location { get; set; }

        [Description]
        public string Description { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
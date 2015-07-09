using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventSquared.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}",
            ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        public string Description { get; set; }

        public Address Address { get; set; }

        public ICollection<Square> Squares { get; set; }
    }
}
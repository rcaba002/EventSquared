using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventSquared.Models
{
    public class newSquareViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Updated")]
        [DisplayFormat(DataFormatString = "{0:d/M/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime currentTime { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:HH:mm tt}",
            ApplyFormatInEditMode = true)]
        public string Title { get; set; }

        [Required]
        [DisplayName("Start Time")]
        [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Required]
        [DisplayName("Start Time")]
        [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        [MaxLength(200)]
        public string Location { get; set; }

        [Description]
        public string Description { get; set; }

        public int EventId { get; set; }
    }
}
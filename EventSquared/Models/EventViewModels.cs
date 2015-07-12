using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventSquared.Models
{
    public class EventViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        public int? AddressId { get; set; }

        [Required]
        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}",
            ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [MaxLength(100)]
        [Required]
        public string Title { get; set; }

        [Description]
        [Required]
        public string Description { get; set; }

        [MaxLength(100)]
        [Required]
        public string Street { get; set; }

        [MaxLength(50)]
        [Required]
        public string City { get; set; }

        [MaxLength(20)]
        [Required]
        public string State { get; set; }

        [MaxLength(20)]
        [DisplayName("Zip Code")]
        [Required]
        public string ZipCode { get; set; }
    }
}
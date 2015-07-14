using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventSquared.Models
{
        public class newEventViewModel
    {
        [DisplayName("Date")]
        [Required]
        public DateTime StartDate { get; set; }

        [MaxLength(100)]
        [Required]
        public string Title { get; set; }

        [Description]
        public string Description { get; set; }

        public string ApplicationUserId { get; set; }

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
        [Required]
        public string ZipCode { get; set; }

        public int? AddressId { get; set; }

        public int Id { get; set; }
    }

    public class detailsEventViewModel
    {
        public int Id { get; set; }

        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = "{0:M/d/yyyy}", ApplyFormatInEditMode = true)]  
        public DateTime StartDate { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [Description]
        public string Description { get; set; }

        [MaxLength(100)]
        public string Street { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(20)]
        public string State { get; set; }

        [MaxLength(20)]
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }

        public string ApplicationUserId { get; set; }

        public int? AddressId { get; set; }
    }
}
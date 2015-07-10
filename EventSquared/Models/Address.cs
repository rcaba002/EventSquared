using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventSquared.Models
{
    public class Address
    {
        public int Id { get; private set; }

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
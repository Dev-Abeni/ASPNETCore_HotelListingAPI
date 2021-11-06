using System;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Dtos
{
    public class HotelFormDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [Range(1, 5)]
        public double Rating { get; set; }

        //[Required]
        public int CountryId { get; set; }
    }
}

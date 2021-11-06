using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Dtos
{
    public class CountryCreateDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(3)]
        public string ShortName { get; set; }      
    }
}

using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Dtos
{
    public class CreateCountryDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(3)]
        public string ShortName { get; set; }
    }
}

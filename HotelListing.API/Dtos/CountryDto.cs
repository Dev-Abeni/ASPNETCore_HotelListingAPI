using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.API.Dtos
{
    public class CountryDto : CreateCountryDto
    {
        public int Id { get; set; }

        public IList<HotelDto> Hotels { get; set; }
    }
}

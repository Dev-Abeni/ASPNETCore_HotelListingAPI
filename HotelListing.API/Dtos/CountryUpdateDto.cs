using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.API.Dtos
{
    public class CountryUpdateDto : CountryCreateDto
    {
        public IList<HotelFormDto> Hotels { get; set; }
    }
}

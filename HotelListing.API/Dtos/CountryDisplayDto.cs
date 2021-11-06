using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.API.Dtos
{
    public class CountryDisplayDto : CountryCreateDto
    {
        public int Id { get; set; }

        public IList<HotelDisplayDto> Hotels { get; set; }
    }
}

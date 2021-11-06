using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.API.Dtos
{
    public class HotelDisplayDto : HotelFormDto
    {
        public int Id { get; set; }
        public CountryDisplayDto Country { get; set; }
    }
}

using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.API.Configuration.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Hilton Hotel Jamica",
                    Address = "Negril",
                    CountryId = 1,
                    Rating = 4.2
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Hilton Hotel New York",
                    Address = "New York",
                    CountryId = 2,
                    Rating = 4.7
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Hilton Hotel London",
                    Address = "London",
                    CountryId = 3,
                    Rating = 4.5
                }
            );
        }
    }
}

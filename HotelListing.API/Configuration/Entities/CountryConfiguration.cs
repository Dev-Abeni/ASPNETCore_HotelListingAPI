﻿using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.API.Configuration.Entities
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                new Country
                {
                    Id = 1,
                    Name = "Jamica",
                    ShortName = "JM"
                },
                new Country
                {
                    Id = 2,
                    Name = "United States",
                    ShortName = "USA"
                },
                new Country
                {
                    Id = 3,
                    Name = "United Kingdom",
                    ShortName = "UK"
                }
            );
        }
    }
}

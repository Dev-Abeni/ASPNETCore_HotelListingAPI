using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.API.Data
{
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {}


        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        
        // Initial seed data
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Country>().HasData(               
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

            builder.Entity<Hotel>().HasData(
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

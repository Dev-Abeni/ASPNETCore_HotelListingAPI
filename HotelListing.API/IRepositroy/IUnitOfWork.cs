using HotelListing.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.API.IRepositroy
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Country> Countries { get; }
        
        IGenericRepository<Hotel> Hotels { get; }
        
        Task Save();
    }
}

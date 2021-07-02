using EcommerceVT.Data;
using EcommerceVT.Interface;
using EcommerceVT.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EcommerceVT.Services
{
    public class LocationService : ILocation
    {
        private readonly AppDbContext _context;

        public LocationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Location> Get(int locationId)
        {
            return await _context.Location.AsNoTracking().FirstOrDefaultAsync(x => x.Location_Id == locationId);
        }

        public async Task<Location> Post(Location location)
        {
            _context.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task<int> Put(Location location)
        {
            _context.Entry(location).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}

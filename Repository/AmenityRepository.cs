using CozyHaven.Interfaces;
using CozyHaven.Models;
using CozyHaven.Context;
using Microsoft.EntityFrameworkCore;
using CozyHaven.Exceptions;

namespace CozyHaven.Repository
{
    public class AmenityRepository : IRepository<int, Amenity>
    {
        readonly RequestTrackerContext _context;
        private readonly ILogger<AmenityRepository> _logger;

        public AmenityRepository(RequestTrackerContext context, ILogger<AmenityRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Amenity> Add(Amenity item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Amenity record added successfully");
            return item;
        }

        public async Task<Amenity> Delete(int key)
        {
            var amenity = await Get(key);
            _context.Amenities.Remove(amenity);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Amenity record removed successfully");
            return amenity;
        }

        public async Task<Amenity> Get(int key)
        {
            var amenity = await _context.Amenities.FindAsync(key);
            if (amenity != null)
            {
                return amenity;
            }
            throw new AmenityNotFoundException();
        }

        public async Task<List<Amenity>> GetAll()
        {
            return await _context.Amenities.ToListAsync();
        }

        public async Task<Amenity> Update(Amenity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Amenity record updated successfully");
            return item;
        }
    }
}

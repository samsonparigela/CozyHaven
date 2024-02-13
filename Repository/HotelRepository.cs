using CozyHaven.Context;
using CozyHaven.Interfaces;
using CozyHaven.Models;
using CozyHaven.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CozyHaven.Repository
{
    public class HotelRepository : IRepository<int, Hotel>
    {
        readonly RequestTrackerContext _context;
        private readonly ILogger<HotelRepository> _logger;

        public HotelRepository(RequestTrackerContext context, ILogger<HotelRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Hotel> Add(Hotel item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Hotel record added successfully");
            return item;
        }

        public async Task<Hotel> Delete(int key)
        {
            var hotel = await Get(key);
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Hotel record removed successfully");
            return hotel;
        }

        public async Task<Hotel> Get(int key)
        {
            var hotel = await _context.Hotels.FindAsync(key);
            if (hotel != null)
            {
                return hotel;
            }
            throw new HotelNotFoundException();
        }

        public async Task<List<Hotel>> GetAll()
        {
            return await _context.Hotels.Include(h => h.Rooms).ToListAsync();
        }

        public async Task<Hotel> Update(Hotel item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Hotel record updated successfully");
            return item;
        }
    }
}

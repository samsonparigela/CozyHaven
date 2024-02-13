using CozyHaven.Context;
using CozyHaven.Exceptions;
using CozyHaven.Interfaces;
using CozyHaven.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CozyHaven.Repository
{
    public class RoomRepository : IRepository<int, Room>
    {
        readonly RequestTrackerContext _context;
        private readonly ILogger<RoomRepository> _logger;

        public RoomRepository(RequestTrackerContext context, ILogger<RoomRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Room> Add(Room item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Room record added successfully");
            return item;
        }

        public async Task<Room> Delete(int key)
        {
            var room = await Get(key);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Room record removed successfully");
            return room;
        }

        public async Task<Room> Get(int key)
        {
            var room = await _context.Rooms.FindAsync(key);
            if (room != null)
            {
                return room;
            }
            throw new RoomNotFoundException();
        }

        public async Task<List<Room>> GetAll()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> Update(Room item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Room record updated successfully");
            return item;
        }
    }
}

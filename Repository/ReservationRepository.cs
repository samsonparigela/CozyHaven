using CozyHaven.Context;
using CozyHaven.Exceptions;
using CozyHaven.Interfaces;
using CozyHaven.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHaven.Repository
{
    public class ReservationRepository : IRepository<int, Reservation>
    {
        readonly RequestTrackerContext _context;
        private readonly ILogger<ReservationRepository> _logger;

        public ReservationRepository(RequestTrackerContext context, ILogger<ReservationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Reservation> Add(Reservation item)
        {
            _context.Reservations.Add(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Reservation record added successfully");
            return item;
        }

        public async Task<Reservation> Delete(int key)
        {
            var reservation = await Get(key);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Reservation record removed successfully");
            return reservation;
        }

        public async Task<Reservation> Get(int key)
        {
            var reservation = await _context.Reservations.FindAsync(key);
            if (reservation != null)
            {
                return reservation;
            }
            throw new ReservationNotFoundException();
        }

        public async Task<List<Reservation>> GetAll()
        {
            return await _context.Reservations.Include(r => r.User).Include(r => r.Room).ToListAsync();
        }

        public async Task<Reservation> Update(Reservation item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Reservation record updated successfully");
            return item;
        }
    }
}

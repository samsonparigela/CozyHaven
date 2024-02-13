using CozyHaven.Interfaces;
using CozyHaven.Models;
using CozyHaven.Context;
using Microsoft.EntityFrameworkCore;

namespace CozyHaven.Repository
{
    public class HotelOwnerRepository : IRepository<int, HotelOwner>
    {
        private readonly RequestTrackerContext _context;

        public HotelOwnerRepository(RequestTrackerContext context)
        {
            _context = context;
        }

        public async Task<HotelOwner> Add(HotelOwner entity)
        {
            _context.HotelOwners.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<HotelOwner> Delete(int id)
        {
            var owner = await _context.HotelOwners.FindAsync(id);
            if (owner == null)
            {
                throw new InvalidOperationException("Hotel owner not found");
            }

            _context.HotelOwners.Remove(owner);
            await _context.SaveChangesAsync();
            return owner;
        }

        public async Task<HotelOwner> Get(int id)
        {
            return await _context.HotelOwners.FindAsync(id);
        }

        public async Task<List<HotelOwner>> GetAll()
        {
            return await _context.HotelOwners.ToListAsync();
        }

        public async Task<HotelOwner> Update(HotelOwner entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

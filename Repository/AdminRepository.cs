using CozyHaven.Context;
using CozyHaven.Interfaces;
using CozyHaven.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHaven.Repository
{
    public class AdminRepository : IRepository<int, Admin>
    {
        private readonly RequestTrackerContext _context;

        public AdminRepository(RequestTrackerContext context)
        {
            _context = context;
        }

        public async Task<Admin> Add(Admin entity)
        {
            _context.Admins.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Admin> Delete(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                throw new InvalidOperationException("Admin not found");
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<Admin> Get(int id)
        {
            return await _context.Admins.FindAsync(id);
        }

        public async Task<List<Admin>> GetAll()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task<Admin> Update(Admin entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

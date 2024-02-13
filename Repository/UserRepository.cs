using CozyHaven.Interfaces;
using CozyHaven.Models;
using CozyHaven.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CozyHaven.Repository
{
    public class UserRepository : IRepository<string, User>
    {
        private readonly RequestTrackerContext _context;

        public UserRepository(RequestTrackerContext context)
        {
            _context = context;
        }
        public async Task<User> Add(User item)
        {
            _context.Add(item);
            _context.SaveChanges();
            return item;
        }

        public async Task<User> Delete(string key)
        {
            var user = await Get(key);
            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();
                return user;
            }
            return null;
        }

        public async Task<User> Get(string id)
        {
            var user = _context.ApplicationUsers.SingleOrDefault(u => u.Username == id);
            return user;
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _context.ApplicationUsers.ToListAsync();
            return users;
        }

        public async Task<User> Update(User item)
        {
            var user = await Get(item.Username);
            if (user != null)
            {
                _context.Entry<User>(item).State = EntityState.Modified;
                _context.SaveChanges();
                return item;
            }
            return null;
        }
    }
}

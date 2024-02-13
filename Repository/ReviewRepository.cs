using CozyHaven.Interfaces;
using CozyHaven.Context;
using CozyHaven.Exceptions;
using CozyHaven.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CozyHaven.Repository
{
    public class ReviewRepository : IRepository<int, Review>
    {
        readonly RequestTrackerContext _context;
        private readonly ILogger<ReviewRepository> _logger;

        public ReviewRepository(RequestTrackerContext context, ILogger<ReviewRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Review> Add(Review item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Review record added successfully");
            return item;
        }

        public async Task<Review> Delete(int key)
        {
            var review = await Get(key);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Review record removed successfully");
            return review;
        }

        public async Task<Review> Get(int key)
        {
            var review = await _context.Reviews.FindAsync(key);
            if (review != null)
            {
                return review;
            }
            throw new ReviewNotFoundException();
        }

        public async Task<List<Review>> GetAll()
        {
            return await _context.Reviews.Include(r => r.Hotel).ToListAsync();
        }

        public async Task<Review> Update(Review item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Review record updated successfully");
            return item;
        }
    }
}

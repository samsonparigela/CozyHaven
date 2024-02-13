using CozyHaven.Context;
using CozyHaven.Exceptions;
using CozyHaven.Interfaces;
using CozyHaven.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHaven.Repository
{
    public class PaymentRepository : IRepository<int, Payment>
    {
        private readonly RequestTrackerContext _context;
        private readonly ILogger<PaymentRepository> _logger;

        public PaymentRepository(RequestTrackerContext context, ILogger<PaymentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Payment> Add(Payment item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Payment record added successfully");
            return item;
        }

        public async Task<Payment> Delete(int key)
        {
            var payment = await Get(key);
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Payment record removed successfully");
            return payment;
        }

        public async Task<Payment> Get(int key)
        {
            var payment = await _context.Payments.FindAsync(key);
            if (payment != null)
            {
                return payment;
            }
            throw new PaymentNotFoundException();
        }

        public async Task<List<Payment>> GetAll()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment> Update(Payment item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Payment record updated successfully");
            return item;
        }
    }
}

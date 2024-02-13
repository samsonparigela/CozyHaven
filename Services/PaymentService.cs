using CozyHaven.Interfaces;
using CozyHaven.Models;

namespace CozyHaven.Services
{
    public class PaymentService : IPaymentAdminService, IPaymentCustomerService, IPaymentOwnerService
    {
        private readonly IRepository<int, Payment> _repo;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IRepository<int, Payment> repo, ILogger<PaymentService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Payment> AddPayment(Payment payment)
        {
            payment = await _repo.Add(payment);
            return payment;
        }

        public async Task<Payment> DeletePayment(int paymentId)
        {
            var payment = await _repo.Delete(paymentId);
            return payment;
        }

        public async Task<Payment> GetPayment(int paymentId)
        {
            var payment = await _repo.Get(paymentId);
            return payment;
        }

        public async Task<List<Payment>> GetAllPayments()
        {
            var payments = await _repo.GetAll();
            return payments;
        }

        public async Task<Payment> UpdatePayment(Payment payment)
        {
            payment = await _repo.Update(payment);
            return payment;
        }

        //public async Task<List<Payment>> GetAllPaymentsByOwner(int ownerId)
        //{
        //    var payments = await _repo.GetAll();
        //    return payments.Where(p => p.Reservation.User.HotelOwner.HotelOwnerID == ownerId).ToList();
        //}
    }
}

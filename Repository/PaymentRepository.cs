using HandyMan.Data;
using HandyMan.Interfaces;
using HandyMan.Models;
using Microsoft.EntityFrameworkCore;

namespace HandyMan.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly Handyman_DBContext _context;
        public PaymentRepository(Handyman_DBContext context)
        {
            _context = context;
        }

        public async void CreatePayment(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
        }

        public void DeletePaymentById(int id)
        {
            _context.Payments.Remove(_context.Payments.Where(a => a.Payment_ID == id).FirstOrDefault());
        }

        public void DeletePaymentByRequestId(int id)
        {
            _context.Payments.Remove(_context.Payments.Where(a=>a.Request_ID==id).FirstOrDefault());
        }

        public void EditPayment(Payment payment)
        {
            _context.Entry(payment).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Payment>> GetPaymentAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await _context.Payments.Where(a => a.Payment_ID == id).FirstOrDefaultAsync();
        }

        public async Task<Payment> GetPaymentByRequestIdAsync(int id)
        {
            return await _context.Payments.Where(a=>a.Request_ID==id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

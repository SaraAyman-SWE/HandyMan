using HandyMan.Models;

namespace HandyMan.Interfaces
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetPaymentAsync();
        Task<Payment> GetPaymentByIdAsync(int id);
        Task<Payment> GetPaymentByRequestIdAsync(int id);
        void CreatePayment(Payment payment);
        void EditPayment(Payment payment);
        void DeletePaymentById(int id);
        void DeletePaymentByRequestId(int id);
        Task<bool> SaveAllAsync();
    }
}

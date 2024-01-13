using ExpensePaymentSystem.Data.Entity;

namespace ExpensePaymentSystem.Business.Interfaces;

public interface IPaymentService
{
    Task<Payment> CreatePaymentAsync(Payment payment);
    Task<Payment> GetPaymentByIdAsync(int paymentId);
    Task<IEnumerable<Payment>> GetAllPaymentsAsync();
    Task<Payment> UpdatePaymentAsync(int userId, Payment payment);
    Task<bool> DeletePaymentAsync(int paymentId);
}

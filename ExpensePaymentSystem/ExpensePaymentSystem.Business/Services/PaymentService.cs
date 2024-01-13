using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Services;

public class PaymentService : IPaymentService
{
    private readonly ExpensePaymentSystemDbContext _context;

    public PaymentService(ExpensePaymentSystemDbContext context)
    {
        _context = context;
    }

    public async Task<Payment> CreatePaymentAsync(Payment payment)
    {
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
        return payment;
    }

    public async Task<Payment> GetPaymentByIdAsync(int paymentId)
    {
        return await _context.Payments.FindAsync(paymentId);
    }

    public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
    {
        return await _context.Payments.ToListAsync();
    }

    public async Task<Payment> UpdatePaymentAsync(int userId, Payment payment)
    {
        _context.Payments.Update(payment);
        await _context.SaveChangesAsync();
        return payment;
    }

    public async Task<bool> DeletePaymentAsync(int paymentId)
    {
        var payment = await _context.Payments.FindAsync(paymentId);
        if (payment == null)
        {
            return false;
        }
        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();
        return true;
    }
}

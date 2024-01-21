using ExpensePaymentSystem.Business.Interfaces;

namespace ExpensePaymentSystem.Business.Services;

public class FakePaymentSimulator : IPaymentSimulator
{
    public async Task<bool> SimulatePaymentAsync(decimal amount, int recipientId)
    {
        await Task.Delay(1000);
        
        Console.WriteLine("Payment transaction completed successfully.");
        
        return true;
    }
}